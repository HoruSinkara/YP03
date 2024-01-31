using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;

using YP03.Entity;
using Azure.Identity;

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
            GenerateCaptcha();
            username = Settings.Default.usernameId;
            password = Settings.Default.password;
            role = Settings.Default.role;
            
            switch (role)
            {
                case "Участник":
                    AccountPartipicant account = new AccountPartipicant();
                    account.Show();
                    this.Close();
                    break;
                case "Организатор":
                    var org = Constants.Context.organizers.Where(p => p.Id.Equals(username) && p.Password == password).FirstOrDefault();
                    if (org!=null)
                    {
                        OrganizerWindows account1 = new OrganizerWindows(org);
                        account1.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Возникла проблема с сохраненной учётной записью");
                    }
                    
                    break;
                case "Жюри":
                    AccountJury account2 = new AccountJury();
                    account2.Show();
                    this.Close();
                    break;
                case "Модератор":
                    AccountJury account3 = new AccountJury();
                    account3.Show();
                    this.Close();
                    break;
            }

        }
        private int username;
        private string password;
        private string role;
        private int loginAttempts = 0;
        private string? correctCaptcha;
        private void GenerateCaptcha()
        {
            // Генерация случайной строки для CAPTCHA
            correctCaptcha = CaptchaHelper.GenerateRandomCaptcha(4);
            byte[] imageBytes = CaptchaHelper.GenerateCaptchaImage(correctCaptcha); 

            // Отображение CAPTCHA на форме
            CaptchaImage.Source = ByteArrayToBitmapImage(imageBytes);

        }
        private BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                BitmapImage imageSource = new BitmapImage();
                imageSource.BeginInit();
                imageSource.StreamSource = stream;
                imageSource.CacheOption = BitmapCacheOption.OnLoad;
                imageSource.EndInit();
                return imageSource;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try {
                int id;
                if(!int.TryParse(IdNumber.Text, out id)){
                    MessageBox.Show("Введён неверный номер");
                    return;
                }
                var password = Password.Password;
                string introducedCaptcha = IntroducedCaptcha.Text;
                if (correctCaptcha != introducedCaptcha)
                {
                    loginAttempts++;
                    if (loginAttempts >= 3)
                    {
                        MessageBox.Show("Неправильная CAPTCHA. Попробуйте еще раз. Блокировка на 10 секунд");
                        BlockSystemForTenSeconds();
                        loginAttempts = 0;
                    }
                    else
                    {
                        MessageBox.Show("Неправильная CAPTCHA. Попробуйте еще раз.");

                    }
                    GenerateCaptcha();
                }
                else
                {
                    if (!id.Equals(null) && password != null)
                    {
                        var roleString = Role.Text;
                        switch (roleString)
                        {
                            case "Участник":
                                var particant = Constants.Context.participants.Where(p => p.Id.Equals(id) && p.Password == password).FirstOrDefault();
                                if (particant != null)
                                {
                                    Settings.Default.usernameId = particant.Id;
                                    Settings.Default.password = particant.Password;
                                    Settings.Default.role = "Участник";
                                    AccountPartipicant account = new AccountPartipicant();
                                    account.Show();
                                    this.Close();
                                }
                                else
                                {

                                }
                                break;
                            case "Организатор":
                                var organizer = Constants.Context.organizers.Where(p => p.Id.Equals(id) && p.Password == password).FirstOrDefault();
                                if (organizer != null)
                                {
                                    Settings.Default.usernameId = organizer.Id;
                                    Settings.Default.password = organizer.Password;
                                    Settings.Default.role = "Организатор";
                                    OrganizerWindows account = new OrganizerWindows(organizer);
                                    account.Show();
                                    this.Close();
                                }
                                break;
                            case "Жюри":
                                var jury = Constants.Context.juries.Where(p => p.Id.Equals(id) && p.Password == password).FirstOrDefault();
                                if (jury != null)
                                {
                                    Settings.Default.usernameId = jury.Id;
                                    Settings.Default.password = jury.Password;
                                    Settings.Default.role = "Жюри";
                                    AccountJury account = new AccountJury();
                                    account.Show();
                                    this.Close();
                                }
                                break;
                            case "Модератор":
                                var moderator = Constants.Context.moderators.Where(m => m.Id.Equals(id) && m.Password == password).FirstOrDefault();
                                if (moderator != null)
                                {
                                    Settings.Default.usernameId = moderator.Id;
                                    Settings.Default.password = moderator.Password;
                                    Settings.Default.role = "Модератор";
                                    AccountJury account = new AccountJury();
                                    account.Show();
                                    this.Close();
                                }
                                break;
                            default:
                                loginAttempts++;
                                if (loginAttempts >= 3)
                                {
                                    BlockSystemForTenSeconds();
                                    loginAttempts = 0;
                                    MessageBox.Show("Неверные логин или пароль. Блокировка на 10 секунд");
                                }
                                else
                                {
                                    MessageBox.Show("Неверные логин или пароль");

                                }
                                GenerateCaptcha();
                                break;
                        }
                    }
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("не удалось считать ваш номер");
            }
            catch {
                MessageBox.Show("Произошла непредвиденная ошибка");
            }
            
            
        }
        private void BlockSystemForTenSeconds()
        {
            // Блокировка системы на 10 секунд
            DisableLoginFormControls();
            Task.Delay(10000).ContinueWith(_ => EnableLoginFormControls(), TaskScheduler.FromCurrentSynchronizationContext());
        }
        private void DisableLoginFormControls()
        {
            // Отключение элементов управления для входа в систему
            SignInButton.IsEnabled = false;
            IntroducedCaptcha.IsEnabled = false;
            
        }

        private void EnableLoginFormControls()
        {
            // Включение элементов управления для входа в систему
            SignInButton.IsEnabled = true;
            IntroducedCaptcha.IsEnabled = true;
          
        }

        private void Password_LostFocus(object sender, RoutedEventArgs e)
        {
            string password = Password.Password;

            /*пароль(с повтором), соответствующий требованиям:
            • не менее 6 символов;
            • заглавные и строчные буквы;
            • не менее одного спецсимвола;
            • не менее одной цифры*/
            //Только для английских букв ^(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W])(?=.*\\d).{6,}$
            if (!Regex.IsMatch(password, "^(?=.*[а-яА-Яa-zA-Z])(?=.*[a-zа-я])(?=.*[A-ZА-Я])(?=.*[\\W])(?=.*\\d).{6,}$"))
            {
                MessageBox.Show("Пароль не соответствует требованиям.");
            }
        }

    }
}
