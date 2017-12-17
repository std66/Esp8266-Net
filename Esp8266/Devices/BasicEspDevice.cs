using Esp8266.Communication;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Esp8266.NetworkManagement;

namespace Esp8266.Devices {
	internal class BasicEspDevice : IEsp8266Device {
		private readonly CommandInvoker Command;

		public string Version {
			get {
				DeviceResponse Response = this.Command.Send("AT+GMR");

				if (Response.RequestSucceeded)
					return Response.Data.ElementAt(0);

				throw new EspCommunicationException(Response);
			}
		}

		public virtual IEnumerable<Network> AvailableNetworks {
			get {
				DeviceResponse Response = this.Command.Send("AT+CWLAP");

				if (!Response.RequestSucceeded)
					throw new EspCommunicationException(Response);

				List<Network> Result = new List<Network>();
				foreach (string Item in Response.Data) {
					Network CurrentNetwork = Network.Parse(Item);

					if (CurrentNetwork != null)
						Result.Add(CurrentNetwork);
				}

				return Result;
			}
		}

		internal BasicEspDevice(CommandInvoker Command) {
			this.Command = Command;
		}

		public async Task<string> GetVersionAsync() {
			DeviceResponse Response = await this.Command.SendAsync("AT+GMR");

			if (Response.RequestSucceeded)
				return Response.Data.ElementAt(0);

			throw new EspCommunicationException(Response);
		}

		public bool Reset() {
			return this.Command.Send("AT+RST").RequestSucceeded;
		}

		public async Task<bool> ResetAsync() {
			return (await this.Command.SendAsync("AT+RST")).RequestSucceeded;
		}

		public virtual async Task<IEnumerable<Network>> GetAvailableNetworksAsync() {
			DeviceResponse Response = await this.Command.SendAsync("AT+CWLAP");

			if (!Response.RequestSucceeded)
				throw new EspCommunicationException(Response);

			List<Network> Result = new List<Network>();

			foreach (string Item in Response.Data) {
				Network CurrentNetwork = Network.Parse(Item);

				if (CurrentNetwork != null)
					Result.Add(CurrentNetwork);
			}

			return Result;
		}
	}
}
