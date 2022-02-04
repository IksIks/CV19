using CV19.Infrastructure.Commands.Base;
using System;


namespace CV19.Infrastructure.Commands
{
    internal class LambdaCommand : Command
    {
        protected readonly Action<object> execute;
        protected readonly Func<object, bool> canExecute;
        public LambdaCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
        {
            execute = Execute ?? throw new ArgumentNullException(nameof(execute));
            canExecute = CanExecute;
        }
        public override bool CanExecute(object parameter)
            => canExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) 
            => execute(parameter);
    }
}
