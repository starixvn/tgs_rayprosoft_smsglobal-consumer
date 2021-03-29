using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SMSGlobal.api;
using TGS.SmsReceiver;
using TGS.Consumers.GlobalSms;

namespace GlobalSmsConsumer
{
	internal class GlobalSmsReceiveHandler : ISmsContentHandler
	{
		private readonly Client _client;
		private readonly GlobalSmsOptions _options;

		public GlobalSmsReceiveHandler(Client client, IOptions<GlobalSmsOptions> optionsAccessor)
		{
			_client = client ?? throw new ArgumentNullException(nameof(client));
			_options = optionsAccessor.Value;
		}
		public async ValueTask HandleAsync(SmsContent smsContent)
		{
			var response = await _client.SMS
				.SMSSend(new
				{
					origin = _options.Origin,
					destination = smsContent.Destination,
					message = smsContent.Message
				})
				.ConfigureAwait(false);

			Console.WriteLine(JsonSerializer.Serialize(smsContent));
			Console.WriteLine(JsonSerializer.Serialize(response));
		}
	}
}
