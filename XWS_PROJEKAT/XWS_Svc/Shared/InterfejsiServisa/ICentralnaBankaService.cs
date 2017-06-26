using Shared.Model.XSD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace XWS.Shared.Model.InterfejsiServisa
{
	[ServiceContract]
    [XmlSerializerFormat(Style = OperationFormatStyle.Document, Use = OperationFormatUse.Literal)]
    public interface ICentralnaBankaService
	{
		/// <summary>
		/// Prima RTGS nalog, salje Poruku O Odobrenju banci primaoca, a vraca Poruku o zaduzenju banci koja salje nalog (duzniku)
		/// </summary>
		/// <param name="nalog"></param>
		/// <returns></returns>
		[OperationContract]
		PorukaOZaduzenju AcceptRTGSAndSendMessages(RTGSNalog nalog);

        [OperationContract]
        void NapraviNalogZaGrupnoPlacanje();

    }
}
