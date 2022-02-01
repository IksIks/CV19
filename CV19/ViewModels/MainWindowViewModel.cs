using System;
using System.Collections.Generic;
using System.Text;
using CV19.ViewModels.Base;

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



    }
}
