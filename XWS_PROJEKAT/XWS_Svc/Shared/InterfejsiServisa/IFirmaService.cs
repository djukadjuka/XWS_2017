
using Shared.Model.XSD;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using XWS.Shared.Model;

namespace XWS.Shared.Model.InterfejsiServisa
{
    [ServiceContract]
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

        [OperationContract]
        void NapraviNalogZaPrenos(Faktura faktura);

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
