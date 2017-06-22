
using Shared.Model.XSD;
using System.Collections.Generic;
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
		string ReturnMessageFromBank();

		[OperationContract]
		void InsertIntoFaktura();

		[OperationContract]
		void AcceptMessageFromBank(string message);

		[OperationContract]
		string GetOneFaktura(string id);

		[OperationContract]
		List<Faktura> GetAllFaktura();
    }
}
