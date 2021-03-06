﻿
using Shared.Model.XSD;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web.Services;
using XWS.Shared.Model;

namespace XWS.Shared.Model.InterfejsiServisa
{
    [ServiceContract]
	[XmlSerializerFormat(Style = OperationFormatStyle.Document,
Use = OperationFormatUse.Literal)]
	public interface IFirmaService
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        FakturaResponse SlanjeFakture(Faktura faktura, string nazivFirme);

		[OperationContract]
		string ReturnMessageFromBank();

		[OperationContract]
		void AcceptMessageFromBank(string message);

        [OperationContract]
        void SaveCreatedInvoice(Faktura faktura);

        [OperationContract]
        void PrikaziFakture(string nazivFirme);

		[OperationContract]
		void SendCreatedInvoice(int idFakture);

		[OperationContract]
		void SendInvoiceProfile(Faktura sourceInvoice);

		[OperationContract]
		List<Faktura> GetForCompanyAndStatus(Firma firma, string status);
		
		[WebMethod]
        [OperationContract]
        void NapraviNalogZaPrenosK(NalogZaPlacanje nzp);

		[WebMethod]
		[OperationContract]
        void NapraviNalogZaGrupnoPlacanjeK();


        [OperationContract]
        void PromeniStatusFakture(int idFakture, string status);

		[OperationContract]
		string ZahtevZaPresek(ZahtevZaDobijanjeIzvoda zahtev);
    }

    [DataContract]
    public class FakturaResponse
    {

        bool success;

        [DataMember]
        public bool Success
        {
            get { return success; }
            set { success = value; }
        }


    }
}
