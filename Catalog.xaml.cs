
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using YP03.Entity;
using YP03.Entity.Model;

namespace YP03
{
    /// <summary>
    /// Логика взаимодействия для Catalog.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        public static ObservableCollection<catalogViewModel> models {  get; set; }
        public static ObservableCollection<catalogViewModel> modelsCurrent { get; set; }
        private static TextChangedEventArgs _contextSearchEvent { get; set; }
        private static object _contextSortDirection { get; set; }
        private static SelectionChangedEventArgs _contextSortDirectionEvent { get; set; }
        private static object _contextSearch { get; set; }
        public Catalog()
        {
            InitializeComponent();
            models = loadCatalogItems();
            catalogList.ItemsSource = models;
            modelsCurrent = models;
            List<string> courses = new List<string> { "Аналитика", "Биг Дата", "Дизайн", "Информационная безопасность", "ИТ" };
            Direction.ItemsSource = courses;
            Direction.SelectedItem = "Аналитика";
            Direction.SelectionChanged += DirectionChange;

            Search.TextChanged += Search_TextChanged;
        }
        public void DirectionChange(object sender, SelectionChangedEventArgs e)
        {
            string Mode = Direction.SelectedItem as string;
            switch (Mode)
            {
                case "Аналитика":
                    modelsCurrent = models;
                    var list = modelsCurrent.Where(m => m.Course == "Аналитика");
                    modelsCurrent = new ObservableCollection<catalogViewModel>(list);
                    break;
                case "Биг Дата":
                    modelsCurrent = models;
                    var list1 = modelsCurrent.Where(m => m.Course == "Биг Дата");
                    modelsCurrent = new ObservableCollection<catalogViewModel>(list1);
                    break;
                case "Дизайн":
                    modelsCurrent = models;
                    var list2 = modelsCurrent.Where(m => m.Course == "Дизайн");
                    modelsCurrent = new ObservableCollection<catalogViewModel>(list2);
                    break;
                case "Информационная безопасность":
                    modelsCurrent = models;
                    var list3 = modelsCurrent.Where(m => m.Course == "Информационная безопасность");
                    modelsCurrent = new ObservableCollection<catalogViewModel>(list3);
                    break;
                case "ИТ":
                    modelsCurrent = models;
                    var list4 =modelsCurrent.Where(m => m.Course == "ИТ").ToList();
                    modelsCurrent = new ObservableCollection<catalogViewModel>(list4);
                    break;
                default:
                    break;
            }
            catalogList.ItemsSource = modelsCurrent;
            _contextSortDirectionEvent = e;
            _contextSortDirection = sender;
            Search_TextChanged(_contextSearch, _contextSearchEvent);
            
            
        }
        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string Condition = Search.Text;
                catalogList.ItemsSource = modelsCurrent.Where(x => x.Name.Contains(Condition));
                _contextSearch = sender;
                _contextSearchEvent = e;
            }
            catch (Exception)
            {

               
            }
           
        }
        public ObservableCollection<catalogViewModel> loadCatalogItems()
        {
            Random random = new Random();
            List<string> images = new List<string> { "/p1.png", "/p2.png", "/17.jpg" };
            List<string> courses = new List<string> { "Аналитика", "Биг Дата", "Дизайн", "Информационная безопасность", "ИТ" };
            var events = Constants.Context.events.ToList();
            ObservableCollection<catalogViewModel> catalogs = new ObservableCollection<catalogViewModel>();
            foreach (var even in events)
            {
                catalogs.Add(
                    new catalogViewModel
                    {
                        Image = images[random.Next(0, 3)],
                        Name = even.NameEvent,
                        Course = courses[random.Next(0, 5)],
                        Date = even.StartDate.Date
                    }
                    );
            }
            return catalogs;

        }
        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow logIn = new MainWindow();
            logIn.Show();
            this.Close();
        }
        private static object _contextSortDate { get; set; }
        private static SelectionChangedEventArgs _contextSortDateEvent { get; set; }
        private void Date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var date = Date.SelectedDate;
            var list = modelsCurrent.Where(x => x.Date.Date == date).ToList();
            modelsCurrent = new ObservableCollection<catalogViewModel>(list);
            catalogList.ItemsSource = modelsCurrent;
            _contextSortDate = sender;
            _contextSortDateEvent = e;
            DirectionChange(_contextSortDirection, _contextSortDirectionEvent);
        }
    }
}
