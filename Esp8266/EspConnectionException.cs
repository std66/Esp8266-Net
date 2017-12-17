using System;
using System.IO.Ports;

namespace Esp8266 {
	public class EspConnectionException : Exception {
		public SerialPort SerialPortConfiguration { get; }

		internal EspConnectionException(SerialPort Port)
			: this($"Failed to connect to the ESP8266 device on serial port '{Port.PortName}' with a baud rate of {Port.BaudRate}.", Port) {

		}

		internal EspConnectionException(string Message, SerialPort Port)
			: base(Message) {
			this.SerialPortConfiguration = Port;
		}
	}
}
