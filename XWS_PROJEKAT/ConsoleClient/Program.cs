using FirmaService;
using Shared.Model.XSD;
using System;
using System.Collections.Generic;
using System.ServiceModel;


namespace ConsoleClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            string name = "";
           
            var wsHttpBinding = new WSHttpBinding(SecurityMode.None);
            wsHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            wsHttpBinding.Security.Message.EstablishSecurityContext = false;


            var address = new EndpointAddress("http://localhost:8080/Firme?wsdl");
            var client = new FirmaClient(wsHttpBinding, address);


            while (true)
            {
				if(name == ""){
					Console.WriteLine("Your name:");
					name = Console.ReadLine();
				}

				if (name == "") continue;
				else
				{
					Console.WriteLine("1.) Check bills to pay.");
					Console.WriteLine("2.) Sign out.");

					string command = Console.ReadLine();
					if(command == "1")
					{
						ListFactures(name, client);		
					}else
					if(command == "2")
					{
						name = "";
					}else
					{
						Console.WriteLine("Unrecognized command!");
					}
				}
			}
        }

		public static void ListFactures(string name, FirmaClient client)
		{
			List<Faktura> allFaktsForFirm = client.GetFakturaByFirmName(name);
			foreach (var f in allFaktsForFirm)
			{
				Console.WriteLine(f);
				Console.WriteLine("[ENTER] for next, [Q] to end.");
				string comm = Console.ReadLine();
				if (comm == "Q" || comm == "q") break;
			}
		}
    }
}
