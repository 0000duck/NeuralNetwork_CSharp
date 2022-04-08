using neuron_with_oop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Med_System
{
    class SystemController
    {
        public NeuralNetworks DataNetwork { get; }
        public NeuralNetworks ImageNetwork { get; }

        public SystemController()
        {
            Topology dataTopology = new Topology(14, 1, 0.1, 7);
            DataNetwork = new NeuralNetworks(dataTopology);

            Topology imageTopology = new Topology(400, 1, 0.1, 200);
            ImageNetwork = new NeuralNetworks(imageTopology);
        }
    }
}
