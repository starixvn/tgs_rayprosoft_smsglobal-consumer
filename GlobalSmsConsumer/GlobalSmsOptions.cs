namespace TGS.Consumers.GlobalSms
{
	public class GlobalSmsOptions
	{
		public string ApiKey { get; set; }

		public string ApiSecret { get; set; }

		public string Origin { get; set; } = "SMS";
	}
}
