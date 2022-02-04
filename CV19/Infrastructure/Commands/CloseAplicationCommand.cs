using CV19.Infrastructure.Commands.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace CV19.Infrastructure.Commands
{
    internal class CloseAplicationCommand : Command
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
