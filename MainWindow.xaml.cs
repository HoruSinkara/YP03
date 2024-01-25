using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using YP03.Entity;

namespace YP03
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var a = Constants.Context.activities.ToList();
            if (true)
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var id = Convert.ToInt32(IdNumber.Text);
            var password = Password.Password;

            if(!id.Equals(null) && password!=null)
            {
                switch (Role.SelectedItem)
                {
                    case "Участник":
                        var particant = Constants.Context.participants.Where(p => p.Id.Equals(id) && p.Password == password).FirstOrDefault();
                        if (particant != null)
                        {
                            AccountPartipicant account = new AccountPartipicant();
                            account.Show();
                            this.Close();
                        }
                        break;
                    case "Организатор":
                        var organizer = Constants.Context.organizers.Where(p => p.Id.Equals(id) && p.Password == password).FirstOrDefault();
                        if (organizer != null)
                        {
                            OrganizerWindows account = new OrganizerWindows();
                            account.Show();
                            this.Close();
                        }
                        break;
                    case "Жюри":
                        var jury = Constants.Context.juries.Where(p => p.Id.Equals(id) && p.Password == password).FirstOrDefault();
                        if (jury != null)
                        {
                            AccountJury account = new AccountJury();
                               
                            account.Show();
                            this.Close();
                        }
                        break;
                    case "Модератор":
                        var moderator= Constants.Context.moderators.Where(m => m.Id.Equals(id) && m.Password == password).FirstOrDefault();
                        if (moderator != null)
                        {
                            AccountJury account= new AccountJury();
                            
                            account.Show();
                            this.Close();
                        }
                        break;
                }
            }
        }
    }
}
