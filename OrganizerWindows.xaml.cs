using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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
using YP03.Entity.Model;

namespace YP03
{
    /// <summary>
    /// Логика взаимодействия для OrganizerWindows.xaml
    /// </summary>
    public partial class OrganizerWindows : Window
    {
        public OrganizerWindows(Organizer organizer)
        {
            InitializeComponent();
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + organizer.Photo);
            image.EndInit();
            Photo.Source = image;

            DateTime dateTime = DateTime.Now;
            if(dateTime.Hour >= 9 && (dateTime.Hour < 11 || (dateTime.Hour <= 11 && dateTime.Minute == 0)))
            {
                Greeting.Text = $"Доброе утро!\n{organizer.Name} {organizer.Patronymic}";
            }else if((dateTime.Hour >= 11 && dateTime.Minute == 1) && (dateTime.Hour < 18 || (dateTime.Hour <= 18 && dateTime.Minute == 0)))
            {
                Greeting.Text = $"Добрый день!\n{organizer.Name} {organizer.Patronymic}";
            }
            else
            {
                Greeting.Text = $"Добрый вечер!\n{organizer.Name} {organizer.Patronymic}";
            }
        }

    }
}
