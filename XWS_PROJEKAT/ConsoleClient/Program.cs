using FirmaService;
using Shared.Model.XSD;
using System;
using System.Collections.Generic;
using System.ServiceModel;


namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = "";
           
            var wsHttpBinding = new WSHttpBinding(SecurityMode.None);
            wsHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            wsHttpBinding.Security.Message.EstablishSecurityContext = false;


            var address = new EndpointAddress("http://localhost:8080/FirmaA?wsdl");
            var client = new FirmaClient(wsHttpBinding, address);


            while (true)
            {
				Console.WriteLine("1.Read message from bank.");
				Console.WriteLine("2.Insert Facture.");
				Console.WriteLine("3.Get One Fakutra.");
				string command = Console.ReadLine();
				if(command == "1"){
					Console.WriteLine(client.ReturnMessageFromBank());
				}else if(command == "2"){
					Console.WriteLine("Inserting into database!");
					client.InsertIntoFaktura();
					Console.WriteLine("Check your database ;) ...");
				}else if(command == "3"){
					Console.WriteLine("ID Fakture : ");
					string idfakt = Console.ReadLine();
					Console.WriteLine(client.GetOneFaktura(idfakt));
				}else if(command == "4"){
					List<Faktura> fakture = client.GetAllFaktura();
					foreach(var f in fakture){
						Console.WriteLine(f);
					}
				}
            }
        }
    }
}
