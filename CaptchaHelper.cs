using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace YP03
{
    public static class CaptchaHelper
    {
        public static string GenerateRandomCaptcha(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static byte[] GenerateCaptchaImage(string captchaText)
        {
            // Создание изображения для CAPTCHA
            
            Bitmap bitmap = new Bitmap(200, 50);
            Graphics graphics = Graphics.FromImage(bitmap);
            Random random = new Random();

            // Задание фона и шума на изображении
            graphics.Clear(Color.White);
            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(bitmap.Width);
                int y = random.Next(bitmap.Height);
                bitmap.SetPixel(x, y, Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)));
            }

            // Нанесение текста CAPTCHA на изображение
            Font font = new Font("Arial", 20, FontStyle.Bold, GraphicsUnit.Pixel);
            graphics.DrawString(captchaText, font, Brushes.Black, new Point(10, 10));

            // Преобразование изображения в массив байтов
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, ImageFormat.Png);
                return memoryStream.ToArray();
            }
        }
    }
}
