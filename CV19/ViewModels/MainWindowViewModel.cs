﻿using CV19.Infrastructure.Commands;
using CV19.ViewModels.Base;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using OxyPlot;
using System;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Тестовый набор данных для визуализации графиков
        private IEnumerable<DataPoint> testDataPoints;
        public IEnumerable<DataPoint> TestDataPoints
        {
            get { return testDataPoints; }
            set { Set(ref testDataPoints, value); }
        } 
        #endregion



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

            var dataPoints = new List<DataPoint>((int) (360 / 0.1));
            for (var x = 0d; x <= 360; x++)
            {
                const double toRad = Math.PI / 180;
                var y = Math.Sin(x * toRad);
                dataPoints.Add(new DataPoint (x ,y));
            }
            TestDataPoints = dataPoints;
        }
    }
}
