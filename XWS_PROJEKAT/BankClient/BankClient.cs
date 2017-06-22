using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using XWS_Svc.Shared.Model.InterfejsiServisa;

namespace BankClient
{
	public class BankClient : ClientBase<IBankaService>, IBankaService
	{
		public BankClient(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress) { }

		public string ProveriBanku()
		{
			return Channel.ProveriBanku();
		}

		public void sendMessageToFirm(string firmName, string message)
		{
			Channel.sendMessageToFirm(firmName, message);
		}
	}
}
