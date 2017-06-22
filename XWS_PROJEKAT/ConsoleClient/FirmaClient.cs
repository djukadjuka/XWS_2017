using FirmaService;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System;

namespace ConsoleClient
{
    public class FirmaClient : ClientBase<IFirmaService>, IFirmaService
    {
        public FirmaClient(Binding binding, EndpointAddress address) : base (binding, address)
		{
        }

		public void AcceptMessageFromBank(string message)
		{
			Channel.AcceptMessageFromBank(message);
		}

		public string GetData(int value)
        {
           return Channel.GetData(value);
        }

		public string ReturnMessageFromBank()
		{
			return Channel.ReturnMessageFromBank();
		}

		public FakturaResponse SlanjeFakture(Faktura faktura)
        {
            return Channel.SlanjeFakture(faktura);
        }
    }
}
