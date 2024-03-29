﻿using System.Drawing;

namespace neuron_with_oop
{
    public class PictureConverter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>")]

        public int Boundary { get; set; } = 128;
        public int Height { get; set; }
        public int Width { get; set; }


        public double[] Convert(Bitmap img)
        {
            var result = new List<double>();

            var image = new Bitmap(img);
            var resizeImage = new Bitmap(image, new Size(15, 15));


            Height = resizeImage.Height;
            Width = resizeImage.Width;

            for (int y = 0; y < resizeImage.Height; y++)
            {
                for (int x = 0; x < resizeImage.Width; x++)
                {
                    var pixel = resizeImage.GetPixel(x, y);
                    var value = Brightness(pixel);
                    result.Add(value);

                }
            }

            return result.ToArray();
        }

        private int Brightness(Color pixel)
        {
            var result = 0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B;

            return result < Boundary ? 0 : 1;

        }

        public void Save(string path, double[] pixels)
        {
            var image = new Bitmap(Width, Height);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    var color = pixels[y * Width + x] == 1 ? Color.White : Color.Black;
                    image.SetPixel(x, y, color);

                }
            }
            image.Save(path);
        }
    }
}
