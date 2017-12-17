using Esp8266.Communication;
using Esp8266.Devices;
using System;
using System.IO;
using System.IO.Ports;

namespace Esp8266 {
	public class Esp8266
    {
		public static IEsp8266Device GetDevice(SerialPort Port) {
			#region Error checking
			if (Port == null)
				throw new ArgumentNullException(nameof(Port));

			if (!Port.IsOpen) {
				try {
					Port.Open();
				}
				catch (UnauthorizedAccessException e) {
					throw new EspConnectionException($"Failed to open port '{Port.PortName}' because: {e.Message}", Port);
				}
			}
			#endregion

			StreamWriter sw = new StreamWriter(Port.BaseStream) { AutoFlush = true };
			StreamReader sr = new StreamReader(Port.BaseStream);

			CommandInvoker c = new CommandInvoker(sr, sw);
			DeviceResponse Response = c.Send("AT");

			if (Response.RequestSucceeded)
				return new BasicEspDevice(c);

			throw new EspConnectionException(Port);
		}

		public static bool TryGetDevice(SerialPort Port, out IEsp8266Device Device) {
			Device = null;

			#region Error checking
			if (Port == null)
				return false;

			if (!Port.IsOpen) {
				try {
					Port.Open();
				}
				catch (UnauthorizedAccessException e) {
					return false;
				}
			}
			#endregion

			StreamWriter sw = new StreamWriter(Port.BaseStream) { AutoFlush = true };
			StreamReader sr = new StreamReader(Port.BaseStream);

			CommandInvoker c = new CommandInvoker(sr, sw);
			DeviceResponse Response = c.Send("AT");

			if (Response.RequestSucceeded)
				Device = new BasicEspDevice(c);

			return Response.RequestSucceeded;
		}
	}
}
