using System.IO;
using System.Threading.Tasks;

namespace Esp8266.Communication {
	internal class CommandInvoker {
		private readonly StreamReader Reader;
		private readonly StreamWriter Writer;

		internal CommandInvoker(StreamReader Reader, StreamWriter Writer) {
			this.Reader = Reader;
			this.Writer = Writer;
		}

		internal DeviceResponse Send(string Command) {
			this.Writer.WriteLine(Command);
			return DeviceResponse.ReadResponse(Command, this.Reader);
		}

		internal async Task<DeviceResponse> SendAsync(string Command) {
			await this.Writer.WriteLineAsync(Command);
			return await DeviceResponse.ReadResponseAsync(Command, this.Reader);
		}
	}
}
