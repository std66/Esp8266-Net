using System;
using System.Collections.Generic;

namespace Esp8266.Communication {
	public class EspCommunicationException : Exception {
		private readonly DeviceResponse Response;

		public IEnumerable<string> DeviceResponse {
			get {
				return this.Response.Data;
			}
		}

		internal EspCommunicationException(DeviceResponse Response) 
			: base($"An unknown error occured during the execution of the following command: {Response.ExecutedCommand}") {
			this.Response = Response;
		}
	}
}
