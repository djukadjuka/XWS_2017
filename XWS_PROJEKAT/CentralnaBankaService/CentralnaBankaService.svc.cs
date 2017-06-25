using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using XWS.Shared.Model.InterfejsiServisa;

namespace CentralnaBankaService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CentralnaBankaService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CentralnaBankaService.svc or CentralnaBankaService.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CentralnaBankaService : ICentralnaBankaService
    {
        public void doSomething()
        {
            throw new NotImplementedException();
        }
    }
//{
//	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CentralnaBankaService" in code, svc and config file together.
//	// NOTE: In order to launch WCF Test Client for testing this service, please select CentralnaBankaService.svc or CentralnaBankaService.svc.cs at the Solution Explorer and start debugging.
//	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
//	public class CentralnaBankaService : ICentralnaBankaService
//	{
//		public void doStuff()
//		{
//			throw new NotImplementedException();
//		}
//	}
}
