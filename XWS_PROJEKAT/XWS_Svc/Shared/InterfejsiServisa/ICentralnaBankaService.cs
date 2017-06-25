using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace XWS.Shared.Model.InterfejsiServisa
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICentralnaBankaService" in both code and config file together.
	[ServiceContract]
    [XmlSerializerFormat(Style = OperationFormatStyle.Document, Use = OperationFormatUse.Literal)]
    public interface ICentralnaBankaService
	{
		[OperationContract]
		void doSomething();
	}
}
