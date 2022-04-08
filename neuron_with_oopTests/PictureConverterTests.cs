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

            var converter = new PictureConverter();
            var inputs = converter.Convert(@"C:\Users\sempaku\Desktop\Neural Med SYs\NeuralNetwoek\neuron_with_oopTests\Numbers\Num_Tests\0numtest\0test.png");
            converter.Save(@"C:\Users\sempaku\Desktop\Neural Med SYs\NeuralNetwoek\neuron_with_oopTests\Numbers\result.png", inputs);
        }
    }
}