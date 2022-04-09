using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace neuron_with_oop
{
    public class DelBlack
    {
        public int Boundary { get; set; } = 128;
        public int Height { get; set; }
        public int Width { get; set; }


        public Bitmap Delete(Bitmap img)
        {
            var result = new List<double>();

            var image = new Bitmap(img);
            var resizeImage = new Bitmap(image, new Size(50, 50));


            Height = resizeImage.Height;
            Width = resizeImage.Width;

            for (int y = 0; y < resizeImage.Height; y++)
            {
                for (int x = 0; x < resizeImage.Width; x++)
                {
                    var pixel = resizeImage.GetPixel(x, y);
                    var value = Blacker(pixel);
                    resizeImage.SetPixel(x, y, value);

                }
            }
            
            return resizeImage;
        }

        private Color Blacker(Color pixel)
        {
            var Y = 0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B;
            int x = 140;
            int y = 14;
            int z = 110;
            if (pixel.R <= x | pixel.G <= y | pixel.B <= z)
            {
                return Color.White;
            }
            else
            {
                return pixel;
            }

        }

      /*  public void Grayer(string path)
        {
            
            var image = new Bitmap(path);
            var resize = new Bitmap(image, new Size(50, 50));

            Height = resize.Height;
            Width = resize.Width;

            for (int y = 0; y < resize.Height; y++)
            {
                for (int x = 0; x < resize.Width; x++)
                {
                    var pixel = resize.GetPixel(x, y);
                    double YY = 0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B;
                    int Y = Convert.ToInt32(YY);

                    resize.SetPixel(x, y, Color.FromArgb(Y, Y, Y));

                }

            }
            for (int y = 0; y < resize.Height; y++)
            {
                for (int x = 0; x < resize.Width; x++)
                {
                    var pixel = resize.GetPixel(x, y);
                    
                    if (pixel.R < 70)
                        resize.SetPixel(x, y, Color.White);

                }

            }
            resize.Save(@"C:\Users\sempaku\Desktop\Neural Med SYs\NeuralNetwoek\neuron_with_oopTests\DelBlack\resuGrayer.png");

        }*/

        public Bitmap Violetted(string path)
        {
            var image = new Bitmap(path);
            var resize = new Bitmap(image, new Size(15, 15));

            Height = resize.Height;
            Width = resize.Width;
            var flag = false;
            for (int y = 0; y < resize.Height; y++)
            {
                for (int x = 0; x < resize.Width; x++)
                {
                    var pixel = resize.GetPixel(x, y);

                    if ((141 <= pixel.R & pixel.R <= 161) & (15 <= pixel.G & pixel.G <= 33) & (110 <= pixel.B & pixel.B <= 116))
                    {
                        flag = true;
                        var whiteImage = Delete(image);
                        resize = Convert(whiteImage);
                        return resize;
                        
                    }
                    else
                    {
                        resize.SetPixel(x, y, Color.White);
                    }
                    

                }

            }
            
            return resize;
            

        }

        public Bitmap Convert(Bitmap img)
        {
            var result = new List<double>();

            var image = new Bitmap(img);
            var resizeImage = new Bitmap(image, new Size(50, 50));


            Height = resizeImage.Height;
            Width = resizeImage.Width;

            for (int y = 0; y < resizeImage.Height; y++)
            {
                for (int x = 0; x < resizeImage.Width; x++)
                {
                    var pixel = resizeImage.GetPixel(x, y);
                    var value = Brightness(pixel);
                    if (value == 0)
                    {
                        resizeImage.SetPixel(x,y,Color.Black);
                    }
                    else
                    {
                        resizeImage.SetPixel(x, y, Color.White);
                    }

                }
            }

            return resizeImage;
        }

        private int Brightness(Color pixel)
        {
            var result = 0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B;

            return result < Boundary ? 0 : 1;

        }


    }
}
