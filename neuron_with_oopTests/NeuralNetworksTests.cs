using Microsoft.VisualStudio.TestTools.UnitTesting;
using neuron_with_oop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace neuron_with_oop.Tests
{
    [TestClass()]
    public class NeuralNetworksTests
    {
        [TestMethod()]
        public void FeedForwardTest()
        {
            var outputs = new double[] { 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1 };
            var inputs = new double[,]
            {
                // Результат - Пациент болен - 1
                //             Пациент Здоров - 0

                // Неправильная температура T
                // Хороший возраст A
                // Курит S
                // Правильно питается F
                // T  A  S  F
                 { 0, 0, 0, 0 },
                 { 0, 0, 0, 1 },
                 { 0, 0, 1, 0 },
                 { 0, 0, 1, 1 },
                 { 0, 1, 0, 0 },
                 { 0, 1, 0, 1 },
                 { 0, 1, 1, 0 },
                 { 0, 1, 1, 1 },
                 { 1, 0, 0, 0 },
                 { 1, 0, 0, 1 },
                 { 1, 0, 1, 0 },
                 { 1, 0, 1, 1 },
                 { 1, 1, 0, 0 },
                 { 1, 1, 0, 1 },
                 { 1, 1, 1, 0 },
                 { 1, 1, 1, 1 }
            };


            var topology = new Topology(4, 1,0.1, 3);
            var neuralNetwork = new NeuralNetworks(topology);           
            var difference = neuralNetwork.Learn(outputs,inputs, 300000);

            var results = new List<double>();
            for (int i = 0; i < outputs.Length; i++)
            {
                var row = NeuralNetworks.GetRow(inputs, i);
                var res = neuralNetwork.Predict(row).Output;
                results.Add(res);   
            }

            for (int i = 0; i < results.Count; i++)
            {
                var expected = Math.Round(outputs[i], 2);
                var actual = Math.Round(results[i], 2);

                Assert.AreEqual(expected, actual);
            }
            

        }

        [TestMethod()]
        public void DataSetTest()
        {
            var outputs = new List<double>();
            var inputs = new List<double[]>();    
            using (var sr = new StreamReader(@"C:\Users\sempaku\Desktop\обучение по нейронке\neuron_with_oopTests\hearth.csv"))
            {
                var header = sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    var row = sr.ReadLine();
                    var values = row.Split(',').Select(v => Convert.ToDouble(v.Replace(".",","))).ToList();
                    var output = values.Last();
                    var input = values.Take(values.Count - 1 ).ToArray();

                    outputs.Add(output);
                    inputs.Add(input);
                }

            }

            var inputSignals = new double[inputs.Count, inputs[0].Length];
            for(int i = 0; i < inputSignals.GetLength(0); i++)
            {
                for (var j = 0; j < inputSignals.GetLength(1); j++)
                {
                    inputSignals[i, j] = inputs[i][j];
                }
            }

            var topology = new Topology(outputs.Count, 1, 0.1, outputs.Count/2);
            var neuralNetwork = new NeuralNetworks(topology);
            var difference = neuralNetwork.Learn(outputs.ToArray(), inputSignals, 100);

            var results = new List<double>();
            for (int i = 0; i < outputs.Count; i++)
            {
                
                var res = neuralNetwork.Predict(inputs[i]).Output;
                results.Add(res);
            }

            for (int i = 0; i < results.Count; i++)
            {
                var exprcted = Math.Round(outputs[i], 2);
                var actual = Math.Round(results[i], 2);

                Assert.AreEqual(exprcted, actual);
            }


        }

        [TestMethod()]
        public void RecognaizeImage()
        {
            var size = 1000;

            var parasitizedPath = @"C:\Users\sempaku\Desktop\cell_images\Parasitized\";
            var unparasitizedPath = @"C:\Users\sempaku\Desktop\cell_images\Uninfected\";

            var converter = new PictureConverter();
            var testParasitizedImageInput = converter.Convert(@"C:\Users\sempaku\Desktop\Neural Med SYs\NeuralNetwoek\neuron_with_oopTests\malaria\paraz\paraz.png");
            var testUnparasitizedImageInput = converter.Convert(@"C:\Users\sempaku\Desktop\Neural Med SYs\NeuralNetwoek\neuron_with_oopTests\malaria\unparaz\unparaz.png");

            var topology = new Topology(testParasitizedImageInput.Length, 2, 0.1, testParasitizedImageInput.Length * 2);
            var neuralNetwork = new NeuralNetworks(topology);


            double[,] unparasitizedInputs = GetData(unparasitizedPath, converter, testParasitizedImageInput,size);
            neuralNetwork.Learn(new double[] { 0 }, unparasitizedInputs, 100);

            double[,] parasitizedInputs = GetData(parasitizedPath, converter, testParasitizedImageInput, size);
            neuralNetwork.Learn(new double[] { 1 }, parasitizedInputs, 100);

            var par = neuralNetwork.Predict(testParasitizedImageInput.Select(t => (double)t).ToArray());
            var unpar = neuralNetwork.Predict(testUnparasitizedImageInput.Select(t => (double)t).ToArray());

            Assert.AreEqual(1, Math.Round(par.Output, 2));
            Assert.AreEqual(0, Math.Round(unpar.Output, 2));
        }

        private static double[,] GetData(string parasitizedPath, PictureConverter converter, double[] testImageInput,int size )
        {
            var images = Directory.GetFiles(parasitizedPath);
            var result = new double[size, testImageInput.Length];
            for (int i = 0; i < size; i++)// i -> кол-во изображений
            {
                var image = converter.Convert(images[i]);
                for (int j = 0; j < image.Length; j++)
                {
                    result[i, j] = image[j];
                }
            }

            return result;
        }

        [TestMethod]
        public void XORTest()
        {
            var outputs = new double[] { 0, 1, 1, 0 };
            var inputs = new double[,]
            {
                {0,0 },
                {0,1 },
                {1,0 },
                {1,1 }
            };

            var topology = new Topology(2, 1, 0.1, 3);
            var neuralNetwork = new NeuralNetworks(topology);
            neuralNetwork.Learn(outputs, inputs, 1000000);

            var results = new List<double>();

            for (int i = 0; i < outputs.Length; i++)
            {
                var row = NeuralNetworks.GetRow(inputs, i);
                var res = neuralNetwork.Predict(row).Output;
                results.Add(res);
            }

            for (int i = 0; i < results.Count; i++)
            {
                var expected = Math.Round(outputs[i], 2);
                var actual = Math.Round(results[i], 2);

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void SearchNumber() 
        {
            var size = 1000;

            var edinicaPath = @"C:\Users\sempaku\Desktop\Neural Med SYs\NeuralNetwoek\neuron_with_oopTests\Numbers\NumsForLearn\1numbers";
            var nullPath = @"C:\Users\sempaku\Desktop\Neural Med SYs\NeuralNetwoek\neuron_with_oopTests\Numbers\NumsForLearn\0numbers";


            var converter = new PictureConverter();
            var testedinica = converter.Convert(@"C:\Users\sempaku\Desktop\Neural Med SYs\NeuralNetwoek\neuron_with_oopTests\Numbers\Num_Tests\1numtest\test1.png");
            var testnull = converter.Convert(@"C:\Users\sempaku\Desktop\Neural Med SYs\NeuralNetwoek\neuron_with_oopTests\Numbers\Num_Tests\0numtest\0test.png");

            var topology = new Topology(edinicaPath.Length, 1, 0.1, 32);
            var neuralNetwork = new NeuralNetworks(topology);

            double[,] edInputs = GetData(edinicaPath, converter, testedinica, size);
            double[,] nlInputs = GetData(nullPath, converter, testnull, size);

            
            neuralNetwork.Learn(new double[] { 1 }, edInputs, 3);
            neuralNetwork.Learn(new double[] { 0 }, nlInputs, 3);
            

            var ed = neuralNetwork.Predict(testedinica.Select(t => (double)t).ToArray());
            var nl = neuralNetwork.Predict(testnull.Select(t => (double)t).ToArray());

            Assert.AreEqual(0, Math.Round(nl.Output, 2));
            Assert.AreEqual(1, Math.Round(ed.Output, 2));
            


        }
    }

    
}