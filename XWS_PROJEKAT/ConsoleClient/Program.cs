using FirmaService;
using System;

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
				string command = Console.ReadLine();
				if(command == "1"){
					Console.WriteLine(client.ReturnMessageFromBank());
				}

            }
        }
    }
}
