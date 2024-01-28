using System;
using System.Collections.Generic;
using System.IO;
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

namespace YP03
{
    /// <summary>
    /// Логика взаимодействия для CaptchaView.xaml
    /// </summary>
    public partial class CaptchaView : Window
    {
        public CaptchaView()
        {
            InitializeComponent();
            GenerateCaptcha();
        }
        private string correctCaptcha;
        private void GenerateCaptcha()
        {
            // Генерация случайной строки для CAPTCHA
            correctCaptcha = CaptchaHelper.GenerateRandomCaptcha(4);
            // Предположим, что у вас есть массив байтов imageBytes, который содержит изображение
            byte[] imageBytes = CaptchaHelper.GenerateCaptchaImage(correctCaptcha); // Получение массива байтов изображения

            // Отображение CAPTCHA на форме
            captchaImage.Source = ByteArrayToBitmapImage(imageBytes);
          
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

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            string introducedCaptcha = IntroducedCaptcha.Text;
            if (introducedCaptcha == correctCaptcha)
            {

            }
        }
    }
}
