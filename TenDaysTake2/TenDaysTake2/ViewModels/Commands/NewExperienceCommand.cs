using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TenDaysTake2.ViewModels.Commands
{
    class NewExperienceCommand : ICommand
    {

        private ExperiencesVM viewModel;
        public NewExperienceCommand(ExperiencesVM viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.NewExperience();
        }
    }
}
