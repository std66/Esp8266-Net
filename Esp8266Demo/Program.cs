using Esp8266;
using Esp8266Demo.Extensions;
using System;
using System.IO.Ports;
using System.Linq;

namespace Esp8266Demo {
	class Program {
		private const string SerialPort = "COM5";
		private const int BaudRate = 9600;

		static void Main(string[] args) {
			Console.WriteLine("ESP8266 WiFi module demo");
			Console.WriteLine("========================");
			Console.WriteLine();

			using (SerialPort Port = new SerialPort(SerialPort, BaudRate)) {
				IEsp8266Device Device;

				Console.WriteLine($"Connecting to the device on '{SerialPort}'...");
				if (Esp8266.Esp8266.TryGetDevice(Port, out Device)) {
					Console.WriteLine("Successfully connected.");
				}
				else {
					Console.Error.WriteLine("Failed to connect.");
					Environment.Exit(1);
				}

				GetFirmwareVersionExample(Device);
				GetAvailableNetworksExample(Device);
			}
		}

		private static void GetFirmwareVersionExample(IEsp8266Device Device) {
			Console.WriteLine();
			Console.WriteLine($"Firmware version: {Device.Version}");
			Console.WriteLine();
		}

		private static void GetAvailableNetworksExample(IEsp8266Device Device) {
			var AvailableNetworks = Device.AvailableNetworks;

			Console.WriteLine($"Available networks ({AvailableNetworks.Count()}):");
			foreach (var item in AvailableNetworks) {
				Console.WriteLine($"   SSID:            {item.Name}");
				Console.WriteLine($"   Encrypted:       {(item.IsEncrypted ? "Yes, using " + item.Encryption : "No")}");
				Console.WriteLine($"   AP MAC address:  {item.MacAddress.AsString()}");
				Console.WriteLine($"   Signal strength: {item.SignalStrength}");
				Console.WriteLine();
			}
		}
	}
}