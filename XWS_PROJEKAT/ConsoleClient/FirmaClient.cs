using FirmaService;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System;
using Shared.Model.XSD;
using System.Collections.Generic;

namespace ConsoleClient
{
    public class FirmaClient : ClientBase<IFirmaService>, IFirmaService
    {
        public FirmaClient(Binding binding, EndpointAddress address) : base (binding, address)
		{
        }

        public void AcceptFactureFromFirm(Faktura faktura, string nazivFirme)
        {
            Channel.AcceptFactureFromFirm(faktura, nazivFirme);
        }

        public void AcceptMessageFromBank(string message)
		{
			Channel.AcceptMessageFromBank(message);
		}

		public string GetData(int value)
        {
           return Channel.GetData(value);
        }

        public void PrikaziFakture(string nazivFirme)
        {
            Channel.PrikaziFakture(nazivFirme); 
        }

        public string ReturnMessageFromBank()
		{
			return Channel.ReturnMessageFromBank();
		}

		public FakturaResponse SlanjeFakture(Faktura faktura, string nazivFirme)
        {
            return Channel.SlanjeFakture(faktura, nazivFirme);
        }
    }
}
