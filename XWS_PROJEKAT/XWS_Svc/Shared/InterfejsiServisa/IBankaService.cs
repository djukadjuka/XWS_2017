using Shared.Model.XSD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace XWS.Shared.Model.InterfejsiServisa
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBankaService" in both code and config file together.
	[ServiceContract]
    [XmlSerializerFormat(Style = OperationFormatStyle.Document, Use = OperationFormatUse.Literal)]
    public interface IBankaService
	{
		[OperationContract]
		string ProveriBanku();

		[OperationContract]
		void sendMessageToFirm(string firmName, string message);

        [OperationContract]
        void NapraviNalogZaPrenos(NalogZaPlacanje nzp);

        [OperationContract]
        void NapraviNalogZaGrupnoPlacanje();
        
        [OperationContract]
		void PrimiPorukuOOdobrenjuIRTGS(PorukaOOdobrenju odobrenje, RTGSNalog nalog);

        [OperationContract]
        void PrimiPorukuOOdobrenjuINalogZaGrupnoPlacanje(PorukaOOdobrenju odobrenje, NalogZaGrupnoPlacanje nzgp);

        [OperationContract]
		void PrimiPorukuOZaduzenju(PorukaOZaduzenju zaduzenje);
    }
}
