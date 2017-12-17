using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Esp8266.Communication {
	internal class DeviceResponse {
		internal IEnumerable<string> Data { get; }
		internal bool RequestSucceeded { get; }
		internal string ExecutedCommand { get; }

		private DeviceResponse(string ExecutedCommand, bool RequestSucceeded, IEnumerable<string> Data) {
			this.ExecutedCommand = ExecutedCommand;
			this.RequestSucceeded = RequestSucceeded;
			this.Data = Data != null ? Data.ToArray() : new string[0];
		}

		internal static DeviceResponse ReadResponse(string ExecutedCommand, StreamReader Reader) {
			if (Reader == null)
				throw new ArgumentNullException(nameof(Reader));
			
			bool RequestSucceeded = false;
			List<string> Data = new List<string>();

			string CurrentLine = String.Empty;
			do {
				CurrentLine = Reader.ReadLine();

				if (CurrentLine == "OK" || CurrentLine == "READY") {
					RequestSucceeded = true;
					break;
				}
				else {
					Data.Add(CurrentLine);
				}
			} while (CurrentLine != "OK" || CurrentLine != "ERROR" || CurrentLine != "READY");
			
			Data.RemoveAll(x => x.Length == 0 || x == ExecutedCommand);

			return new DeviceResponse(ExecutedCommand, RequestSucceeded, Data);
		}

		internal async static Task<DeviceResponse> ReadResponseAsync(string ExecutedCommand, StreamReader Reader) {
			if (Reader == null)
				throw new ArgumentNullException(nameof(Reader));

			bool RequestSucceeded = false;
			List<string> Data = new List<string>();

			string CurrentLine = String.Empty;
			do {
				CurrentLine = await Reader.ReadLineAsync();

				if (CurrentLine == "OK" || CurrentLine == "READY") {
					RequestSucceeded = true;
					break;
				}
				else {
					Data.Add(CurrentLine);
				}
			} while (CurrentLine != "OK" || CurrentLine != "ERROR" || CurrentLine != "READY");
			
			Data.RemoveAll(x => x.Length == 0 || x == ExecutedCommand);

			return new DeviceResponse(ExecutedCommand, RequestSucceeded, Data);
		}
	}
}
