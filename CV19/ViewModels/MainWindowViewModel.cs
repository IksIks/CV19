using CV19.Infrastructure.Commands;
using CV19.ViewModels.Base;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using OxyPlot;
using System;
using System.Collections.ObjectModel;
using CV19.Models.Decanat;
using System.Linq;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private object selectedCompositeValue;

        public object SelectedCompositeValue
        {
            get { return selectedCompositeValue; }
            set { Set(ref selectedCompositeValue, value); }
        }

        public ObservableCollection<Group> Groups { get; }
        public object[] CompositeCollection { get; }


        #region Selected Group: Group - выбранная группа
        /// <summary> Выбранная группа </summary>
        private Group selectedGroup;
        /// <summary> Выбранная группа </summary>
        public Group SelectedGroup
        {
            get { return selectedGroup; }
            set { Set(ref selectedGroup, value); }
        }

        #endregion

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

        /*---------------------------------------------------------------------------------------------------------------*/

        #region Команды

        #region CloseAplicationCommand
        /// <summary>Закрытие приложения</summary>
        public ICommand CloseAplicationCommand { get; }

        private void OnCloseAplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        private bool CanCloseAplicationCommandExecute(object p) => true;
        #endregion

        #region Add Group
        /// <summary>Добавление группы</summary>
        public ICommand CreateCroupCommand { get; }
        private bool CanCreateCroupCommandExecute(object p) => true;
        private void OnCreateCroupCommandExecuted(object p)
        {
            var groupMaxIndex = Groups.Count + 1;
            var newGroup = new Group
            {
                Name = $"Группа {groupMaxIndex}",
                Students = new ObservableCollection<Student>()
            };
            Groups.Add(newGroup);
        }
        #endregion

        #region DeleteGroup
        /// <summary>
        /// Удаление группы
        /// </summary>
        public ICommand DeleteCroupCommand { get; }
        private bool CanDeleteCroupCommandCommandExecute(object p) => p is Group group && Groups.Contains(group);
        private void OnDeleteCroupCommandExecuted(object p)
        {
            if (!(p is Group group)) return;
            var groupIndex = Groups.IndexOf(group);
            Groups.Remove(group);
            if (groupIndex < Groups.Count)
                SelectedGroup = Groups[groupIndex];
        } 
        #endregion

        #endregion

        /*---------------------------------------------------------------------------------------------------------------*/

        public MainWindowViewModel()
        {
            CloseAplicationCommand = new LambdaCommand(OnCloseAplicationCommandExecuted, CanCloseAplicationCommandExecute);
            CreateCroupCommand = new LambdaCommand(OnCreateCroupCommandExecuted, CanCreateCroupCommandExecute);
            DeleteCroupCommand = new LambdaCommand(OnDeleteCroupCommandExecuted, CanDeleteCroupCommandCommandExecute);


            var dataPoints = new List<DataPoint>((int) (360 / 0.1));
            for (var x = 0d; x <= 360; x++)
            {
                const double toRad = Math.PI / 180;
                var y = Math.Sin(x * toRad);
                dataPoints.Add(new DataPoint (x ,y));
            }
            TestDataPoints = dataPoints;
            int studentIndex = 1;
            var students = Enumerable.Range(1, 10).Select(i => new Student
            {
                Name = $"Name {studentIndex}",
                Surname = $"Surname {studentIndex}",
                Patronymic = $"Patronymic {studentIndex++}",
                Birthday = DateTime.Now,
                Rating =0
            });
            var groups = Enumerable.Range(1, 20).Select(i => new Group
            {
                Name = $"Группа {i}",
                Students = new ObservableCollection<Student>(students)
            }) ;

            Groups = new ObservableCollection<Group>(groups);

            var dataList = new List<object>();
            var group = Groups[1];
            dataList.Add(group);
            dataList.Add(group.Students[0]);
            dataList.Add("Hello word");
            dataList.Add(42);
            CompositeCollection = dataList.ToArray();


        }
    }
}
