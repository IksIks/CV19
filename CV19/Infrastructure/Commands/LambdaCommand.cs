using CV19.Infrastructure.Commands.Base;
using System;


namespace CV19.Infrastructure.Commands
{
    internal class LambdaCommand : Command
    {
        protected readonly Action<object> execute;
        protected readonly Func<object, bool> canExecute;
        public LambdaCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;

        }
        public override bool CanExecute(object parameter) => canExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) => execute(parameter);
    }
}
