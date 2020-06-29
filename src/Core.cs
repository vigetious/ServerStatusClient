using System.Collections.Generic;

namespace ServerStatusClient {
    public class Cpucore    {
        public double Coretemp { get; set; } 
        public int Corenumber { get; set; } 
        public List<double> Coreutil { get; set; } 

    }
}