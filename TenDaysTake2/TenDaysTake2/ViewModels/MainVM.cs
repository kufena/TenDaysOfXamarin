using Plugin.Permissions.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using TenDaysTake2.Models;
using TenDaysTake2.ViewModels.Helpers;
using Xamarin.Forms;

namespace TenDaysTake2.ViewModels
{
    class MainVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        LocationHelper locationHelper;
        public ObservableCollection<Venue> Venues { get; set; }

        public MainVM()
        {
            CancelCommand = new Command(CancelAction);
            SaveCommand = new Command<bool>(SaveAction, CanExecuteSave);
            locationHelper = new LocationHelper();
            Venues = new ObservableCollection<Venue>();
        }

        void CancelAction(object obj)
        {
            App.Current.MainPage.Navigation.PopAsync();
        }

        bool CanExecuteSave(bool arg)
        {
            return arg;
        }

        void SaveAction(bool obj)
        {
            Experience newExperience = new Experience()
            {
                Title = Title,
                Content = Content,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                VenueName = SelectedVenue.name,
                VenueCategory = SelectedVenue.MainCategory,
                VenueLat = float.Parse(SelectedVenue.location.Coordinates.Split(',')[0]),
                VenueLng = float.Parse(SelectedVenue.location.Coordinates.Split(',')[1])
            };

            int insertedItems = 0;
            // added using SQLite;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.CreateTable<Experience>();
                insertedItems = conn.Insert(newExperience);
            }
            // here the conn has been disposed of, hence closed
            if (insertedItems > 0)
            {
                Title = string.Empty;
                Content = string.Empty;
                SelectedVenue = null;
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Error", "There was an error inserting the Experience, please try again", "Ok");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool CanSave
        {
            get { return !string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(Content); }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
                OnPropertyChanged("CanSave");
            }
        }

        private string query;
        public string Query
        {
            get { return query; }
            set
            {
                query = value;
                if (!string.IsNullOrWhiteSpace(query))
                {
                    GetVenues();
                    ShowVenues = true;
                }
                else
                {
                    ShowVenues = false;
                }
                OnPropertyChanged("Query");
            }
        }

        public bool ShowSelectedVenue
        {
            get { return SelectedVenue != null; }
        }

        private bool showVenues;
        public bool ShowVenues
        {
            get { return showVenues; }
            set
            {
                showVenues = value;
                OnPropertyChanged("ShowVenues");
            }
        }

        private Venue selectedVenue; // added using TenDaysOfXamarin.Model;
        public Venue SelectedVenue
        {
            get { return selectedVenue; }
            set
            {
                selectedVenue = value;
                if (selectedVenue != null)
                {
                    ShowVenues = false;
                    Query = string.Empty;
                }
                OnPropertyChanged("SelectedVenue");
                OnPropertyChanged("ShowSelectedVenue");
            }
        }

        private string content;
        public string Content
        {
            get { return content; }
            set
            {
                content = value;
                OnPropertyChanged("Content");
                OnPropertyChanged("CanSave");
            }
        }

        public async void GetLocationPermission()
        {
            // added using TenDaysOfXamarin.ViewModels.Helpers;
            // added using Plugin.Permissions.Abstractions;
            var status = await PermissionsHelper.GetPermission(Permission.LocationWhenInUse);

            // Already granted (maybe), go on
            if (status == PermissionStatus.Granted)
            {
                // Granted! Get the location
                await locationHelper.GetLocation(TimeSpan.FromMinutes(30), 500);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Access to location denied", "We don't have access to your location", "Ok");
            }
        }

        public void StopListeningLocationUpdates()
        {
            locationHelper.StopListening();
        }

        public async void GetVenues()
        {
            var response = await Search.SearchRequest(locationHelper.position.Latitude, locationHelper.position.Longitude, 500, Query);
            
            Venues.Clear();
            foreach (var venue in response.venues)
            {
                Venues.Add(venue);
            }
        }
    }
}
