using System;
using System.Linq;
using System.Threading.Tasks;
using RokuDeviceLib;

namespace RokuRemote
{
    public class Program
    {
        // https://developer.roku.com/en-ot/docs/developer-program/dev-tools/external-control-api.md
        // https://developer.roku.com/en-ot/docs/developer-program/dev-tools/external-control-api.md#keypress-key-values
        // UPnP Simple Service Discovery Protocol
        private const string SSDP_IP = "239.255.255.250";
        private const int SDP_Port = 1900;

        public static async Task<int> Main(string[] args)
        {
            await MainAsync();
            return 0;
        }

        private static async Task MainAsync()
        {
            Console.WriteLine("Discoverying Roku Devices Please Wait.....");
            Console.WriteLine();
            var discoveredDevices = await RokuClient.Discover(SSDP_IP, SDP_Port, waitSeconds: 3);
            Console.WriteLine();
            if (discoveredDevices.Length > 0)
            {
                Console.WriteLine("-========== Summary ==========-");
                foreach (var item in discoveredDevices.Select((device, index) => new { Device = device, Index = index }))
                {
                    Console.WriteLine($"Device : {item.Index + 1}");
                    Console.WriteLine($"Model: {item.Device.ModelName}");
                    Console.WriteLine($"Serial Numer: {item.Device.SerialNumber}");
                    Console.WriteLine($"Endpoint: {item.Device.Endpoint}");
                    Console.WriteLine("-====================-");
                    Console.WriteLine();
                }

                await Menu.RequestDeviceSelection(discoveredDevices);
            }
            else
            {
                Console.WriteLine("No devices found...");
            }

        }
    }
}

