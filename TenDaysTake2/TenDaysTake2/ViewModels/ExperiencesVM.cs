using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using TenDaysTake2.Models;
using TenDaysTake2.ViewModels.Commands;
using TenDaysTake2.Views;

namespace TenDaysTake2.ViewModels
{
    class ExperiencesVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        // added using TenDaysOfXamarin.ViewModels.Commands;
        public NewExperienceCommand NewExperienceCommand { get; set; }
        public ObservableCollection<Experience> Experiences { get; set; }

        public ExperiencesVM()
        {
            NewExperienceCommand = new NewExperienceCommand(this);
            Experiences = new ObservableCollection<Experience>();
            ReadExperiences();
        }
        
        public void ReadExperiences()
        {
            // added using TenDaysOfXamarin.Model;
            var experiences = Experience.GetExperiences();
            Experiences.Clear();
            foreach (var experience in experiences)
            {
                Experiences.Add(experience);
            }
        }

        public void NewExperience()
        {
            App.Current.MainPage.Navigation.PushAsync(new MainPage());
        }
    }
}
