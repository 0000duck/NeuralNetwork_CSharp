namespace neuron_with_oop
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }


    public class NeuralNetworks
    {
        public Topology Topology { get; }
        public List<Layer> Layers { get; }

        public NeuralNetworks(Topology topology)
        {
            Topology = topology;

            Layers = new List<Layer>();

            CreateInputLayer();
            CreateHiddenLayers();
            CreateOutputLayer();

        }

        public Neuron Predict(params double[] inputSignals)
        {


            SendSignalsToInputNeurons(inputSignals);
            FeedForwardAllLayersAfterInput();

            if (Topology.OutputCount == 1)
            {
                return Layers.Last().Neurons[0];
            }
            else
            {
                return Layers.Last().Neurons.OrderByDescending(n => n.Output).First(); // упорядочиваем и берем больший
            }
        }
        public double Learn(double[] expected, double[,] inputs, int epoch)
        {
            //var signals = Normalization(inputs);

            var error = 0.0;
            for (int i = 0; i < epoch; i++)
            {
                for (int j = 0; j < expected.Length; j++)
                {
                    var output = expected[j];
                    var input = GetRow(inputs, j);

                    error += Backpropagation(output, input);

                }
            }

            var result = error / epoch;
            return result;
        }

        public static double[] GetRow(double[,] matrix, int row)
        {
            var columns = matrix.GetLength(1);
            var array = new double[columns];
            for (int i = 0; i < columns; ++i)
            {
                array[i] = matrix[row, i];
            }
            return array;
        }


        private double[,] Scalling(double[,] inputs) // Маштабирование
        {
            var result = new double[inputs.GetLength(0), inputs.GetLength(1)];

            for (int coloumn = 0; coloumn < inputs.GetLength(1); coloumn++)
            {
                var min = inputs[0, coloumn];
                var max = inputs[0, coloumn];
                for (int row = 1; row < inputs.GetLength(0); row++)
                {
                    var item = inputs[row, coloumn];
                    if (item < min)
                        min = item;


                    if (item > max)
                        max = item;

                }
                var div = max - min;
                for (int row = 1; row < inputs.GetLength(0); row++)
                {
                    result[row, coloumn] = (inputs[row, coloumn] - min) / div;
                }
            }
            return result;

        }

        private double[,] Normalization(double[,] inputs) //Нормализация
        {
            var result = new double[inputs.GetLength(0), inputs.GetLength(1)];

            for (int coloumn = 0; coloumn < inputs.GetLength(1); coloumn++)
            {
                var sum = 0.0;
                for (int row = 0; row < inputs.GetLength(0); row++)//Вычисляем среднее значение сигнала нейрона
                {
                    sum = sum + inputs[row, coloumn];

                }
                var average = sum / inputs.GetLength(0);

                var error = 0.0;
                for (int row = 0; row < inputs.GetLength(0); row++)//Стандартное квадратичное отклонение нецрона
                {
                    error = Math.Pow((error + inputs[row, coloumn] - average), 2);
                }
                var standartError = Math.Sqrt(error / inputs.GetLength(0));


                for (int row = 0; row < inputs.GetLength(0); row++)// Новое значение нейрона
                {
                    result[row, coloumn] = (inputs[row, coloumn] - average) / standartError;
                }
            }

            return result;
        }

        private double Backpropagation(double expected, params double[] inputs)//обратное распространение ошибки
        {
            var actual = Predict(inputs).Output;

            var difference = actual - expected;

            foreach (var neuron in Layers.Last().Neurons)
            {
                neuron.Learn(difference, Topology.LearningRate);
            }

            for (int j = Layers.Count - 2; j >= 0; j--)// -2 т.к. нумерация с 0 и еще один слой мы уже обучили
            {
                var layer = Layers[j];
                var prevLayer = Layers[j + 1];

                for (int i = 0; i < layer.NeuronCount; i++)
                {
                    var neuron = layer.Neurons[i];

                    for (int k = 0; k < prevLayer.NeuronCount; k++)
                    {
                        var prevNeuron = prevLayer.Neurons[k];
                        var error = prevNeuron.Weights[i] * prevNeuron.Delta;

                        neuron.Learn(error, Topology.LearningRate);
                    }
                }

            }
            var result = difference * difference;
            return result;
        }

        private void FeedForwardAllLayersAfterInput()
        {
            for (int i = 1; i < Layers.Count; i++)// с 1 т.к. на нулевой слой мы уже отправили данные
            {
                var layer = Layers[i];
                var previousLayerSignals = Layers[i - 1].GetSignals();

                foreach (var neuron in layer.Neurons)
                {
                    neuron.FeedForward(previousLayerSignals);
                }


            }
        }

        private void SendSignalsToInputNeurons(params double[] inputSignals)
        {
            for (int i = 0; i < inputSignals.Length; i++)
            {
                var signal = new List<double> { inputSignals[i] };
                var neuron = Layers[0].Neurons[i];

                neuron.FeedForward(signal);

            }
        }

        private void CreateOutputLayer()
        {
            var outputNeurons = new List<Neuron>();
            var lastLayer = Layers.Last();

            for (int i = 0; i < Topology.OutputCount; i++)
            {
                var neuron = new Neuron(lastLayer.NeuronCount, NeuronType.Output);
                outputNeurons.Add(neuron);
            }
            var outputLayer = new Layer(outputNeurons, NeuronType.Output);
            Layers.Add(outputLayer);
        }

        private void CreateHiddenLayers()
        {
            for (int j = 0; j < Topology.HiddenLayers.Count; j++)
            {
                var hiddenNeurons = new List<Neuron>();
                var lastLayer = Layers.Last();
                for (int i = 0; i < Topology.HiddenLayers[j]; i++)
                {
                    var neuron = new Neuron(lastLayer.NeuronCount);
                    hiddenNeurons.Add(neuron);
                }
                var hiddenLayer = new Layer(hiddenNeurons, NeuronType.Output);
                Layers.Add(hiddenLayer);
            }
        }

        private void CreateInputLayer()
        {
            var inputNeurons = new List<Neuron>();
            for (int i = 0; i < Topology.InputCount; i++)
            {
                var neuron = new Neuron(1, NeuronType.Input);
                inputNeurons.Add(neuron);
            }
            var inputLayer = new Layer(inputNeurons, NeuronType.Input);
            Layers.Add(inputLayer);
        }
    }
}
