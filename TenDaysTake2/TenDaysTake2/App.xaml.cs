using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TenDaysTake2.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TenDaysTake2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage( new ExperiencesPage() ); // new MainPage();
        }

        public static string DatabasePath;

        public App(string databasePath)
        {
            InitializeComponent();

            DatabasePath = databasePath;

            MainPage = new NavigationPage( new ExperiencesPage() ); //new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
