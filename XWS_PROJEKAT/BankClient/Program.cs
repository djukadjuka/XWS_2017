using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BankClient
{
	public class Program
	{
		static void Main(string[] args)
		{
			string command = "";

			var wsHttpBinding = new WSHttpBinding(SecurityMode.None);
			wsHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
			wsHttpBinding.Security.Message.EstablishSecurityContext = false;


			var address = new EndpointAddress("http://localhost:8080/BankaA?wsdl");
			var client = new BankClient(wsHttpBinding, address);


			while (true)
			{
				string message;
				string firmName;
				Console.WriteLine("1:Send message.");
				command = Console.ReadLine();
				if (command == "1"){
					Console.WriteLine("Enter Message : ");
					message = Console.ReadLine();
					Console.WriteLine("Enter firm name : ");
					firmName = Console.ReadLine();
					client.sendMessageToFirm(firmName, message);
				}
			}
		}
	}
}
