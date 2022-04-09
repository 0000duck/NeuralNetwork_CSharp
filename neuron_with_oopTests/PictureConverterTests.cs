using Microsoft.VisualStudio.TestTools.UnitTesting;
using neuron_with_oop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuron_with_oop.Tests
{
    [TestClass()]
    public class PictureConverterTests
    {
        [TestMethod()]
        public void ConvertTest()
        {

            //var colorChanged = new ColorChanged();
            //var image = colorChanged.Change(@"C:\Users\sempaku\Desktop\Neural Med SYs\NeuralNetwoek\neuron_with_oopTests\ColorTest\paraz.png", 25, 25);
            //var image = colorChanged.Change(@"C:\Users\sempaku\Desktop\Neural Med SYs\NeuralNetwoek\neuron_with_oopTests\malaria\unparaz\unparaz.png", 25, 25);

        

            /*var converter = new PictureConverter();
            var inputs = converter.Convert(image);
            converter.Save(@"C:\Users\sempaku\Desktop\Neural Med SYs\NeuralNetwoek\neuron_with_oopTests\ColorTest\result.png", inputs);*/
        }

        [TestMethod()]
        public void DeleteBlackColor()
        {
            var deleter = new DelBlack();
            var converter = new PictureConverter();

            //deleter.Delete(@"C:\Users\sempaku\Desktop\Neural Med SYs\NeuralNetwoek\neuron_with_oopTests\ColorTest\paraz.png");
            //deleter.Violetted(@"C:\Users\sempaku\Desktop\Neural Med SYs\NeuralNetwoek\neuron_with_oopTests\DelBlack\paraz.png");
            //deleter.Violetted(@"C:\Users\sempaku\Desktop\Neural Med SYs\NeuralNetwoek\neuron_with_oopTests\DelBlack\unparaz.png");

            var preinput = deleter.Violetted(@"C:\Users\sempaku\Desktop\Neural Med SYs\NeuralNetwoek\neuron_with_oopTests\DelBlack\paraz.png");
            var inputs = converter.Convert(preinput);
            converter.Save(@"C:\Users\sempaku\Desktop\Neural Med SYs\NeuralNetwoek\neuron_with_oopTests\DelBlack\parazAfterConverter.png", inputs);
        }

        
    }
}