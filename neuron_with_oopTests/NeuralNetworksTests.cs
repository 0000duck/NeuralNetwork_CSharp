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
        /*public void FeedForwardTest()
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
                var res = neuralNetwork.FeedForward(row).Output;
                results.Add(res);   
            }

            for (int i = 0; i < results.Count; i++)
            {
                var exprcted = Math.Round(outputs[i], 2);
                var actual = Math.Round(results[i], 2);

                Assert.AreEqual(exprcted, actual);
            }
            

        }*/

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
                
                var res = neuralNetwork.FeedForward(inputs[i]).Output;
                results.Add(res);
            }

            for (int i = 0; i < results.Count; i++)
            {
                var exprcted = Math.Round(outputs[i], 2);
                var actual = Math.Round(results[i], 2);

                Assert.AreEqual(exprcted, actual);
            }


        }

    }

    
}