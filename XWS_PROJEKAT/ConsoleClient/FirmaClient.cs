using System.ServiceModel;
using System.ServiceModel.Channels;
using Shared.Model.XSD;
using System.Collections.Generic;
using XWS.Shared.Model;
using XWS.Shared.Model.InterfejsiServisa;
using System;

namespace ConsoleClient
{
	public class FirmaClient : ClientBase<IFirmaService>, IFirmaService
    {
        public FirmaClient(Binding binding, EndpointAddress address) : base (binding, address)
		{
        }

        public void SaveCreatedInvoice(Faktura faktura)
        {
            Channel.SaveCreatedInvoice(faktura);
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

		public void SendCreatedInvoice(int idFakture)
		{
			Channel.SendCreatedInvoice(idFakture);
		}

		public void SendInvoiceProfile(Faktura sourceInvoice)
		{
			Channel.SendInvoiceProfile(sourceInvoice);
		}

		public List<Faktura> GetForCompanyAndStatus(Firma firma, string status)
		{
			return Channel.GetForCompanyAndStatus(firma, status);
		}

        
        public void NapraviNalogZaPrenos(NalogZaPlacanje nzp)
        {
            Channel.NapraviNalogZaPrenos(nzp);
        }

        public void NapraviNalogZaGrupnoPlacanje()
        {
            Channel.NapraviNalogZaGrupnoPlacanje();
        }

        public void PromeniStatusFakture(int idFakture, string status)
        {
            Channel.PromeniStatusFakture(idFakture, status);
        }

		public string ZahtevZaPresek(ZahtevZaDobijanjeIzvoda zahtev)
		{
			return Channel.ZahtevZaPresek(zahtev);
		}
	}
}
