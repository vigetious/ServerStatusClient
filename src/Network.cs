using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace ServerStatusClient {
    public class Network {
        public List<string> InterfaceNames { get; set; } 
        public List<Speed> Speeds { get; set; } 
    }
}