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


            var address = new EndpointAddress("http://localhost:8080/FirmaService?wsdl");
            var client = new FirmaClient(wsHttpBinding, address);


            while (true)
            {
                
                name = Console.ReadLine();

                
                Faktura faktura = new Faktura();
                faktura.Idfakture = 99;
                faktura.Idporuke = "IDporuka";
            }
        }
    }
}
