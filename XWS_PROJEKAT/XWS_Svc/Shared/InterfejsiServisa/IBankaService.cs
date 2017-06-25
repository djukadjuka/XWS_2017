﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace XWS.Shared.Model.InterfejsiServisa
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBankaService" in both code and config file together.
	[ServiceContract]
	public interface IBankaService
	{
		[OperationContract]
		string ProveriBanku();

		[OperationContract]
		void sendMessageToFirm(string firmName, string message);

        [OperationContract]
        void NapraviNalogZaPrenos(Firma sourceFirma);


    }
}
