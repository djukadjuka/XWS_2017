
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
        FakturaResponse SlanjeFakture(Faktura faktura);

		[OperationContract]
		string ReturnMessageFromBank();

		[OperationContract]
		void AcceptMessageFromBank(string message);
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

    [DataContract]
    public class Faktura
    {
      
        long idfakture;
        string idporuke;

        [DataMember]
        public long Idfakture
        {
            get { return idfakture; }
            set { idfakture = value; }
        }


        [DataMember]
        public string Idporuke
        {
            get { return idporuke; }
            set { idporuke = value; }
        }


    }
}
