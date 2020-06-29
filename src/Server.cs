using System;
using System.Collections.Generic;

namespace ServerStatusClient {
    public class Root {
        public Config Config { get; set; } 
            public Cpu Cpu { get; set; } 
            public Memory Memory { get; set; } 
            public int CpuPhysicalCount { get; set; }
    }
}