using FirmaService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using XWS_Svc.Shared.Model.InterfejsiServisa;

namespace BankaService
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BankaService" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select BankaService.svc or BankaService.svc.cs at the Solution Explorer and start debugging.
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class BankaService : IBankaService
	{
		private string bankName;

		public BankaService(string bankName)
		{
			this.bankName = bankName;
		}

		public string ProveriBanku()
		{
			return bankName;
		}

		public void sendMessageToFirm(string firmName, string message)
		{
			IFirmaService fs = GetIFirmaServiceChannel("http://localhost:8080/" + firmName);  
			fs.AcceptMessageFromBank(message);
		}

		public static IFirmaService GetIFirmaServiceChannel(string fullPathToService)
		{
			ChannelFactory<IFirmaService> channelFactory = new ChannelFactory<IFirmaService>(new WSHttpBinding(SecurityMode.None));
			IFirmaService fs = channelFactory.CreateChannel(new EndpointAddress(fullPathToService));
			return fs;
		}
	}
}
