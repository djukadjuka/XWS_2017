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

		public void AcceptMessageFromBank(string message)
		{
			Channel.AcceptMessageFromBank(message);
		}

		public List<Faktura> GetAllFaktura()
		{
			return Channel.GetAllFaktura();
		}

		public string GetData(int value)
        {
           return Channel.GetData(value);
        }

		public string GetOneFaktura(string id)
		{
			return this.Channel.GetOneFaktura(id);
		}

		public void InsertIntoFaktura()
		{
			this.Channel.InsertIntoFaktura();
		}

		public string ReturnMessageFromBank()
		{
			return Channel.ReturnMessageFromBank();
		}
		
    }
}
