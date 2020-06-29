using System;
using Newtonsoft.Json;
using RestSharp;

namespace ServerStatusClient {
    public class LocalConfig {
        public class ConfigBuilder {
            public int overrideCpuCount { get; set; }
            public bool degreesTemperatureScale { get; set; }
        }

        public LocalConfig(bool configMode, string json = "") {
            var options = JsonConvert.DeserializeObject<ConfigBuilder>(json);
            SendConfig(setConfig(options));
        }

        public ConfigBuilder setConfig(ConfigBuilder oldConfig) {
            ConfigBuilder newConfig = new ConfigBuilder();
            if (oldConfig != null) {
                Console.WriteLine($"Override physical CPU count? Current config is set to {oldConfig.overrideCpuCount}. Enter 0 to auto-detect the CPU count or enter a number.");
            } else {
                Console.WriteLine("Override physical CPU count? Enter 0 to auto-detect the CPU count or enter a number.");
            }
            int cpuCount;
            while (true) {
                try {
                    cpuCount = int.Parse(Console.ReadLine());
                    break;
                } catch (FormatException) {
                    Console.WriteLine("Please enter a number.");
                } catch (NullReferenceException) {
                    Console.WriteLine("Please enter a number.");
                }
            }
            
            if (oldConfig != null) {
                Console.WriteLine(
                    $"Celsius or Fahrenheit? Current config is set to {oldConfig.degreesTemperatureScale}. Enter true for celsius or false for fahrenheit.");
            } else {
                Console.WriteLine("Celsius or Fahrenheit? Enter true for celsius or false for fahrenheit.");
            }
            bool degreesTemperatureScale;
            while (true) {
                try {
                    degreesTemperatureScale = bool.Parse(Console.ReadLine());
                    break;
                } catch (FormatException) {
                    Console.WriteLine("Please enter either true for celsius or false for fahrenheit.");
                } catch (NullReferenceException) {
                    Console.WriteLine("Please enter either true for celsius or false for fahrenheit.");
                }
            }
            newConfig.overrideCpuCount = cpuCount;
            newConfig.degreesTemperatureScale = degreesTemperatureScale;
            return newConfig;
        }

        private static void SendConfig(ConfigBuilder newConfig) {
            var client = new RestClient("http://localhost:1234/config");
            var request = new RestRequest();
            request.Method = Method.POST;
            request.AddJsonBody(newConfig);
            var response = client.Execute(request).Content;
            Console.WriteLine(response);
        }
    }
}