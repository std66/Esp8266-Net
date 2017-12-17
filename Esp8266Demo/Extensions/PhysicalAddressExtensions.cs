using System.Net.NetworkInformation;
using System.Text;

namespace Esp8266Demo.Extensions {
	internal static class PhysicalAddressExtensions {
		internal static string AsString(this PhysicalAddress Address) {
			StringBuilder sb = new StringBuilder();

			foreach (var CurrentByte in Address.GetAddressBytes()) {
				sb.Append(CurrentByte.ToString("X2"));
				sb.Append(":");
			}
			
			return sb.ToString(0, sb.Length - 1);
		}
	}
}
