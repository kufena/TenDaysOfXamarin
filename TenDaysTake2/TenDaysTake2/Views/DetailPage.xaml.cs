using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenDaysTake2.Models;
using TenDaysTake2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TenDaysTake2.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailPage : ContentPage
	{

        DetailVM viewModel;

		public DetailPage (Experience exp)
		{
			InitializeComponent ();
            viewModel = Resources["vm"] as DetailVM;
            viewModel.selectedExperience = exp;

            labelTitle.Text = exp.Title;
            labelContent.Text = exp.Content;
        }

    }
}