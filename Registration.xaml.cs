using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace YP03
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
            try
            {
                Id.Text = generateUniqueId().ToString();
                completion();
            }
            catch (Exception)
            {
            }
            
        }
        private void completion()
        {
            var activities = Constants.Context.courses.ToList();
            var events = Constants.Context.events.ToList();
            List<string> direction = new List<string>();
            foreach ( var activity in activities ) {
                direction.Add(activity.Name);
            }
            Direction.ItemsSource = direction;

            List<string> eventes = new List<string>();
            foreach ( var eve in events ) {
                eventes.Add(eve.NameEvent);
            }
            Event.ItemsSource = eventes;
        }
        private int generateUniqueId()
        {
           /* try
            {
                var juryList = Constants.Context.juries.ToList();
                var moderatorList = Constants.Context.moderators.ToList();
                if (juryList.Count == 0 || moderatorList.Count == 0) { return 1010; }
                if(juryList.Count == 0)
                {
                    var unique = moderatorList.MaxBy(m => m.Id).Id+1;
                    return unique;
                }
                else
                else if(moderatorList.Count == 0)
                {
                    var unique = juryList.MaxBy(m => m.Id).Id + 1;
                    return unique;
                }
                var uniqueId = Math.Max(juryList.MaxBy(j => j.Id).Id, moderatorList.MaxBy(m => m.Id).Id);
                return uniqueId;
            }
            catch (NullReferenceException nullReferenceException)
            {
                return 1010;
            }
            
            catch (Exception)
            {
                return 1010;
            }*/
            int nextJuryId = Constants.Context.juries.Any() ? Constants.Context.juries.Max(e => e.Id) + 1 : 1;
            int nextModeratorId = Constants.Context.moderators.Any() ? Constants.Context.moderators.Max(e => e.Id) + 1 : 1;

            return Math.Max(nextJuryId, nextModeratorId);
        }

        private bool isRightPassword(string password)
        {
            /*пароль(с повтором), соответствующий требованиям:
            • не менее 6 символов;
            • заглавные и строчные буквы;
            • не менее одного спецсимвола;
            • не менее одной цифры*/
            //Только для английских букв ^(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W])(?=.*\\d).{6,}$
            if (!Regex.IsMatch(password, "^(?=.*[а-яА-Яa-zA-Z])(?=.*[a-zа-я])(?=.*[A-ZА-Я])(?=.*[\\W])(?=.*\\d).{6,}$"))
            {
                MessageBox.Show("Пароль не соответствует требованиям.");
                return false;
            }
            return true;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var id = Id.Text;
                var fio = FIO.Text;
                var gender = Genders.Text;
                var role = Role.Text;
                var email = Email.Text;
                var phone = Phone.Text;
                var direction = Direction.Text;
                var events = Event.Text;
                var password = Password.Text;
                var rePassword = RePassword.Text;
                if (!isRightPassword(password)) { return; }
                if (password != rePassword)
                {
                    MessageBox.Show("Пароли не совпадают");
                    return;
                }
                if (fio != "" && gender != "" && role != "" && email != "" && phone != "" && direction != "" && events != "" && password != "" && rePassword != "")
                {
                    var f_i_o = fio.Split(' ').ToList();
                    switch (role)
                    {
                        case "Жюри":
                            Constants.Context.juries.Add(
                                new Entity.Model.Jury
                                {
                                    Id = Int32.Parse(id),
                                    Surname = f_i_o[0],
                                    Name = f_i_o[1],
                                    Patronymic = f_i_o[2],
                                    Gender = gender,
                                    Email = email,
                                    Phone = phone,
                                    Password = password
                                }
                                );
                            Constants.Context.SaveChanges();
                            MessageBox.Show("Запись успешно сохранена");
                            this.Close();
                            break;
                        case "Модератор":
                            Constants.Context.moderators.Add(
                                new Entity.Model.Moderator
                                {
                                    Id = Int32.Parse(id),
                                    Surname = f_i_o[0],
                                    Name = f_i_o[1],
                                    Patronymic = f_i_o[2],
                                    Gender = gender,
                                    Email = email,
                                    Phone = phone,
                                    Password = password
                                }
                                );
                            Constants.Context.SaveChanges();
                            MessageBox.Show("Запись успешно сохранена");
                            this.Close();
                            break;
                        default:
                            break;
                    }

                    return;
                }
                else
                {
                    MessageBox.Show("Остались пустые поля");
                    return;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Произошла ошибка");
            }
            
            
        }
    }
}
