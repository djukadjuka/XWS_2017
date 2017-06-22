using FirmaService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using XWS_Svc.Shared.Model.InterfejsiServisa;

namespace ServerStartup
{
	public class ServerUtils
	{
		/// <summary>
		/// -> http://localhost:8080/
		/// </summary>
		public static readonly string HOST_ADDRESS = "http://localhost:8080/";
		/// <summary>
		/// -> CB/
		/// </summary>
		public static readonly string CENTRALNA_BANKA_NAME = "CB/";

		public static ServiceHost CreateFirmaServer(string firmName)
		{
			var wsHttpBinding = new WSHttpBinding(SecurityMode.None);
			wsHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
			wsHttpBinding.Security.Message.EstablishSecurityContext = false;

			string httpLocation = HOST_ADDRESS + firmName;
			FirmaService.FirmaService service = new FirmaService.FirmaService(firmName);
			ServiceHost svh = new ServiceHost(service, new Uri(httpLocation));

			svh.AddServiceEndpoint(typeof(IFirmaService), wsHttpBinding, httpLocation);
			ServiceMetadataBehavior smb = svh.Description.Behaviors.Find<ServiceMetadataBehavior>();

			if (smb == null)
				smb = new ServiceMetadataBehavior();

			smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
			smb.HttpGetEnabled = true;
			svh.Description.Behaviors.Add(smb);

			return svh;
		}

		public static ServiceHost CreateBankServer(string bankName)
		{
			var wsHttpBinding = new WSHttpBinding(SecurityMode.None);
			wsHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
			wsHttpBinding.Security.Message.EstablishSecurityContext = false;

			string httpLocation = HOST_ADDRESS + bankName;
			BankaService.BankaService service = new BankaService.BankaService(bankName);
			ServiceHost svh = new ServiceHost(service, new Uri(httpLocation));

			svh.AddServiceEndpoint(typeof(IBankaService), wsHttpBinding, httpLocation);
			ServiceMetadataBehavior smb = svh.Description.Behaviors.Find<ServiceMetadataBehavior>();

			if (smb == null)
				smb = new ServiceMetadataBehavior();

			smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
			smb.HttpGetEnabled = true;
			svh.Description.Behaviors.Add(smb);

			return svh;
		}

		public static ServiceHost CreateCentralnaBankaServer()
		{
			var wsHttpBinding = new WSHttpBinding(SecurityMode.None);
			wsHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
			wsHttpBinding.Security.Message.EstablishSecurityContext = false;

			string httpLocation = HOST_ADDRESS + CENTRALNA_BANKA_NAME;
			CentralnaBankaService.CentralnaBankaService service = new CentralnaBankaService.CentralnaBankaService();
			ServiceHost svh = new ServiceHost(service, new Uri(httpLocation));

			svh.AddServiceEndpoint(typeof(IBankaService), wsHttpBinding, httpLocation);
			ServiceMetadataBehavior smb = svh.Description.Behaviors.Find<ServiceMetadataBehavior>();

			if (smb == null)
				smb = new ServiceMetadataBehavior();

			smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
			smb.HttpGetEnabled = true;
			svh.Description.Behaviors.Add(smb);

			return svh;
		}
		

	}
}
