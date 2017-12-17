using System;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace Esp8266.NetworkManagement {
	/// <summary>
	/// Represents a wireless network
	/// </summary>
	public class Network {
		/// <summary>
		/// Gets the SSID of the access point.
		/// </summary>
		public string Name {
			get;
		}

		/// <summary>
		/// Gets the physical address of the access point.
		/// </summary>
		public PhysicalAddress MacAddress {
			get;
		}

		/// <summary>
		/// Gets the strength of the detected signal.
		/// </summary>
		public int SignalStrength {
			get;
		}

		/// <summary>
		/// Gets the encryption used on the network.
		/// </summary>
		public WirelessEncryption Encryption {
			get;
		}

		/// <summary>
		/// Gets if the network uses an encryption.
		/// </summary>
		public bool IsEncrypted {
			get {
				return this.Encryption != WirelessEncryption.NotEncrypted;
			}
		}

		private Network(string Name, PhysicalAddress MacAddress, int SignalStrength, WirelessEncryption Encryption) {
			this.Name = Name;
			this.MacAddress = MacAddress;
			this.SignalStrength = SignalStrength;
			this.Encryption = Encryption;
		}

		internal static Network Parse(string Input) {
			const string Pattern = "\\+CWLAP:\\((?<Encryption>\\d+),\"(?<SSID>[^\"]+)\",(?<strength>-?\\d+),\"(?<MAC>[^\"]+)\",(?<freq_offset>\\d+)\\)";
			Regex r = new Regex(Pattern);

			Match m = r.Match(Input);

			if (!m.Success)
				return null;

			return new Network(
				Name: m.Groups["SSID"].Value,
				MacAddress: PhysicalAddress.Parse(m.Groups["MAC"].Value.ToUpper().Replace(':', '-')),
				SignalStrength: Convert.ToInt32(m.Groups["strength"].Value),
				Encryption: (WirelessEncryption)Convert.ToInt32(m.Groups["Encryption"].Value)
			);
		}
	}
}
