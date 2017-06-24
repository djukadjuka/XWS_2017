
using Shared.Model.XSD;
using System.Runtime.Serialization;
using System.ServiceModel;


namespace FirmaService
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
        void AcceptFactureFromFirm(Faktura faktura, string nazivFirme);

        [OperationContract]
        void PrikaziFakture(string nazivFirme);
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
