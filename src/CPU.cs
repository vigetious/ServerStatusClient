using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ServerStatusClient {
    public class Cpu    {
        public object CpuUtil { get; set; } 
        public object Cputemp { get; set; } 
        public List<Cpucore> Cpucores { get; set; } 

    }
}