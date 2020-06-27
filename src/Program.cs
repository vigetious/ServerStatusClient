using System;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;

namespace ServerStatusClient {
    public class Program {
        public static void Main(string[] args) {
            var retrievedConfig = getConfig();
            if (retrievedConfig != null) {
                if (args.Contains("-c")) {
                    Config json = new Config(true, retrievedConfig);
                }
                
            } else {
                throw new Exception("Contact with server could not be made. Make sure the server is running the service.");
            }
        }

        public static string getConfig() {
            var client = new RestClient("http://localhost:1234/config");
            var request = new RestRequest();
            request.Method = Method.GET;
            var response = client.Execute(request).Content;
            if (response == "success") {
                Console.WriteLine("Successfully got configuration from server.");
                return response;
            }
            Console.WriteLine("Failed to get configuration from server.");
            return null;
            }

        public static string getServerData() {
            var client = new RestClient("http://localhost:1234/config");
        }
    }
}