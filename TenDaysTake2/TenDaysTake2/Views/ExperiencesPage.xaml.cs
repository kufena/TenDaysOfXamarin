using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TenDaysTake2.Models;
using TenDaysTake2.ViewModels;

namespace TenDaysTake2.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExperiencesPage : ContentPage
	{
        ExperiencesVM viewModel;
		public ExperiencesPage ()
		{
			InitializeComponent ();
            viewModel = Resources["vm"] as ExperiencesVM;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.ReadExperiences();
        }

        public void TextCellTap1(object sender, System.EventArgs e)
        {
            var tc = sender as TextCell;
            var obj = tc.BindingContext as Experience;

            Console.WriteLine("what the?" + obj.Id);
            Console.WriteLine("Hell yeah!");

            App.Current.MainPage.Navigation.PushAsync(new DetailPage(obj));

        }
    }
}