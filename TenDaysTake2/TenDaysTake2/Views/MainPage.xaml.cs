using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TenDaysTake2.Models;
using TenDaysTake2.ViewModels;
using TenDaysTake2.ViewModels.Helpers;
using Xamarin.Forms;

namespace TenDaysTake2.Views
{
    public partial class MainPage : ContentPage
    {
        

        MainVM viewModel;
        LocationHelper locationHelper;

        public MainPage()
        {
            InitializeComponent();

            viewModel = new MainVM();
            BindingContext = viewModel;
            locationHelper = new LocationHelper();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.GetLocationPermission();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.StopListeningLocationUpdates();
        }
    }
}

