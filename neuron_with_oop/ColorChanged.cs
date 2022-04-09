
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace neuron_with_oop
{
    public class ColorChanged
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public Bitmap Change(string path, int size_Height, int size_Width)
        {
            Height = size_Height;
            Width = size_Width;

            var result = new List<double>();

            var image = new Bitmap(path);
            image = new Bitmap(image, new Size(Height, Width));
            var newImage = new Bitmap(Height, Width);

            

            image = WhiteBorder(image);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    var pixel = image.GetPixel(x, y);
                    if (pixel == Color.White)
                        continue;
                    else
                    {
                        if (pixel == Color.FromArgb(0, 0, 0))
                        {
                            newImage.SetPixel(x, y, Color.FromArgb(200,200,200));
                            newImage.SetPixel(x - 1, y, Color.FromArgb(200, 200, 200));
                            newImage.SetPixel(x, y - 1, Color.FromArgb(200, 200, 200));
                            newImage.SetPixel(x + 1, y, Color.FromArgb(200, 200, 200));
                            newImage.SetPixel(x, y + 1, Color.FromArgb(200, 200, 200));
                        }
                        else
                        {
                            newImage.SetPixel(x, y, pixel);
                        }
                    }
                    


                }
            }

            return newImage;
        }

        private Bitmap WhiteBorder(Bitmap image)
        {
            var oldImage = image;
            Bitmap whiteImage = new Bitmap(image.Height, image.Width);
            for(int x = 0; x < image.Height; x++)
                for(int y = 0; y < image.Width; y++)
                {
                    var pixel = oldImage.GetPixel(x, y);
                    if ((x == 0 & (0 <= y - 1 & y <= Width - 1)) | (x == Height -1 & (0 <= y & y <= Width -1)) | (y == 0 & (0 <=x & x <= Width -1)) | (y == Width -1 & (0 <=x & x <=Width -1)) )
                    {
                        var white = Color.White;
                        whiteImage.SetPixel(x,y,white);
                    }
                    else
                    {
                        whiteImage.SetPixel(x,y,pixel);
                    }
                }

            return whiteImage;
        }

    }
}

