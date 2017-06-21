using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;

namespace FirmaService
{
    public class FirmaMain
    {
        static void Main(string[] args)
        {
            try { 
            
                var wsHttpBinding = new WSHttpBinding(SecurityMode.None);
                wsHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
                wsHttpBinding.Security.Message.EstablishSecurityContext = false;

                var httpLocation = "http://localhost:8080/FirmaService";
                FirmaService service = new FirmaService("FirmaA");
                ServiceHost svh = new ServiceHost(service, new Uri(httpLocation));

                svh.AddServiceEndpoint(typeof(IFirmaService), wsHttpBinding, httpLocation);


                ServiceMetadataBehavior smb = svh.Description.Behaviors.Find<ServiceMetadataBehavior>();

                if (smb == null)
                    smb = new ServiceMetadataBehavior();

                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                smb.HttpGetEnabled = true;
                svh.Description.Behaviors.Add(smb);

                svh.Open();
           
                var re = new ManualResetEvent(false);
                re.WaitOne();
                svh.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception");
                Console.WriteLine(e);
                var re2 = new ManualResetEvent(false);
                re2.WaitOne();      
            }
        }
    }
}