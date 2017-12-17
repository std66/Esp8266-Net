using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esp8266.NetworkManagement {
	public enum WirelessEncryption {
		NotEncrypted = 0,
		Wep = 1,
		WpaPsk = 2,
		Wpa2Psk = 3,
		Wpa_Wpa2Psk = 4,
		Wpa2Enterprise = 5
	}
}
