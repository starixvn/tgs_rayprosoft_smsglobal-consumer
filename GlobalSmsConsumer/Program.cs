using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SMSGlobal.api;

namespace GlobalSmsConsumer
{
	class Program
	{
		static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureServices(services =>
				{
					services
						.AddOptions<GlobalSmsOptions>()
						.Configure<IConfiguration>((options, config) =>
						{
							config.GetSection("Sms").Bind(options);
						})
						.Services
						.AddSingleton(sp =>
						{
							var options = sp.GetRequiredService<IOptions<GlobalSmsOptions>>().Value;
							var client = new Client(new Credentials(options.ApiKey, options.ApiSecret));

							return client;
						});

					services.AddSmsReceiver()
						.ConfigureOptions((options, sp) =>
						{
							var config = sp.GetRequiredService<IConfiguration>();

							config.GetSection("Sms").Bind(options);
						})
						.RegisterHandler<GlobalSmsReceiveHandler>();
				});

	}
}
