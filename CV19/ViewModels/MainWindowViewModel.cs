using CV19.Infrastructure.Commands;
using CV19.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Заголовок окна

        private string title = "Анализ статистики CV19";
        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }

        #endregion

        #region Статус программы

        /// <summary>Статус программы</summary>
        private string status = "Готов!";
        /// <summary>Статус программы</summary>
        public string Status
        {
            get => status;
            set => Set(ref status, value);
        }

        #endregion

        #region Команды

        #region CloseAplicationCommand
        public ICommand CloseAplicationCommand { get; }

        private void OnCloseAplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        private bool CanCloseAplicationCommandExecute(object p) => true;

        #endregion

        #endregion
        public MainWindowViewModel()
        {
            CloseAplicationCommand = new LambdaCommand(OnCloseAplicationCommandExecuted, CanCloseAplicationCommandExecute);
        }
    }
}
