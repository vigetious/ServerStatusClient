using System;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ServerStatusClient {
    public class Program {
        private class localConfig {
            public string ip;
            public int port;
        }
        
        public static void Main(string[] args) {
            var myLocalConfig = JsonConvert.DeserializeObject<localConfig>(File.ReadAllText("config/config.json"));

            var retrievedConfig = getServerConfig(myLocalConfig.ip, myLocalConfig.port);
            if (retrievedConfig != null) {
                if (args.Contains("-c")) {
                    LocalConfig json = new LocalConfig(true, retrievedConfig);
                }

                var oldNetwork =
                    JsonConvert.DeserializeObject<Network>(getServerData(myLocalConfig.ip, myLocalConfig.port,
                        "network"));
                
                while (true) {
                    var server =
                        JsonConvert.DeserializeObject<Root>(getServerData(myLocalConfig.ip, myLocalConfig.port,
                            "server"));
                    for (int x = 0; x < server.Cpu.Cpucores.Count; x++) {
                        Console.WriteLine(
                            $"Core {server.Cpu.Cpucores[x].Corenumber} temp: {server.Cpu.Cpucores[x].Coretemp}; Util: ");
                        Console.WriteLine(server.Cpu.Cpucores[x].Coreutil[0].ToString("#.0") + "%");
                        Console.WriteLine(server.Cpu.Cpucores[x].Coreutil[1].ToString("#.0") + "%");
                    }

                    Console.WriteLine(
                        $"Memory: Total: {server.Memory.Total}M; Used: {server.Memory.Used}; Free: {server.Memory.Free}");

                    var network =
                        JsonConvert.DeserializeObject<Network>(getServerData(myLocalConfig.ip, myLocalConfig.port,
                            "network"));
                    for (int i = 0; i < network.InterfaceNames.Count; i++) {
                        long newRecieved = network.Speeds[i].Recieved;
                        if (oldNetwork.Speeds[i].Recieved != 0) {
                            Console.WriteLine($"Speed of {network.Speeds[i].Name}: " + ((newRecieved - oldNetwork.Speeds[i].Recieved) / 102400.0).ToString("#.00") + "Mb/s");
                        }
                        oldNetwork.Speeds[i].Recieved = newRecieved;
                    }
                    
                    Console.WriteLine("-----------------------------------------------------------------------------");
                    
                    Thread.Sleep(1000);
                }
            } else {
                throw new Exception("Contact with server could not be made. Make sure the server is running the service.");
            }
        }

        public static string getServerConfig(string ip, int port) {
            var client = new RestClient($"http://{ip}:{port}/config");
            var request = new RestRequest();
            request.Method = Method.GET;
            var response = client.Execute(request).Content;
            if (response.Length > 0) {
                Console.WriteLine("Successfully got configuration from server.");
                return response;
            }
            Console.WriteLine("Failed to get configuration from server.");
            return null;
            }

        public static string getServerData(string ip, int port, string endpoint) {
            var client = new RestClient($"http://{ip}:{port}/{endpoint}");
            var request = new RestRequest();
            request.Method = Method.GET;
            var response = client.Execute(request).Content;
            return response;
        }
    }
}