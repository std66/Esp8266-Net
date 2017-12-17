using Esp8266.NetworkManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Esp8266 {
	public interface IEsp8266Device {
		/// <summary>
		/// Gets the version of the device.
		/// </summary>
		string Version { get; }

		/// <summary>
		/// Gets the available wireless networks.
		/// </summary>
		IEnumerable<Network> AvailableNetworks {
			get;
		}

		/// <summary>
		/// Resets the device.
		/// </summary>
		/// <returns>True if the operation succeeds, false if not.</returns>
		bool Reset();

		/// <summary>
		/// Resets the device asynchronously.
		/// </summary>
		/// <returns>
		///		A <see cref="Task{bool}"/> that represents the asynchronous process.
		///		True if the operation succeeds, false if not.
		/// </returns>
		Task<bool> ResetAsync();

		/// <summary>
		/// Gets the version of the device asynchronously.
		/// </summary>
		/// <returns>
		///		A <see cref="Task{string}"/> that represents the asynchronous process.
		///		A string that represents the version of the module.
		/// </returns>
		Task<string> GetVersionAsync();

		/// <summary>
		/// Gets the available wireless networks asynchronously.
		/// </summary>
		/// <returns>
		///		A <see cref="Task{IEnumerable{Network}}"/> that represents the asynchronous process.
		///		A sequence of <see cref="Network"/> instances that hold informations about
		///		the available networks.
		/// </returns>
		Task<IEnumerable<Network>> GetAvailableNetworksAsync();
	}
}
