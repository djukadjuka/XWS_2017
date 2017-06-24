using Shared    .Model.XSD;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using XWS_Svc.Shared.Model.InterfejsiServisa;

namespace FirmaService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class FirmaService : IFirmaService
    {
        private string nazivFirme;
		private string bankMessage;
        Dictionary<string, Dictionary<int, Faktura>> fakture = new Dictionary<string, Dictionary<int, Faktura>>();  


        public FirmaService(string nazivFirme)
        {
            this.nazivFirme = nazivFirme;
        }

		public void AcceptMessageFromBank(string message)
		{
			this.bankMessage = message;
		}

		public string GetData(int value)
        {
            return string.Format("Odgovor od firme:" + nazivFirme + "   Vrednost je: ", value);
        }

		public string ReturnMessageFromBank()
		{
			return "Message from bank: ["+this.bankMessage+"]";
		}

		public FakturaResponse SlanjeFakture(Faktura faktura, string nazivFirme)
        {
            Console.WriteLine("Slanje fakture od " + this.nazivFirme + "  firmi : " + nazivFirme + "   Faktura ID je: " + faktura.IDFakture);


            IFirmaService fs = GetIFirmaServiceChannel("http://localhost:8080/" + nazivFirme);
            fs.AcceptFactureFromFirm(faktura, this.nazivFirme);

            FakturaResponse fakturaResponse = new FakturaResponse();
            fakturaResponse.Success = true;

            return fakturaResponse;
        }

        public static IFirmaService GetIFirmaServiceChannel(string fullPathToService)
        {
            ChannelFactory<IFirmaService> channelFactory = new ChannelFactory<IFirmaService>(new WSHttpBinding(SecurityMode.None));
            IFirmaService fs = channelFactory.CreateChannel(new EndpointAddress(fullPathToService));
            return fs;
        }

        public static IBankaService GetIBankaServiceChannel(string fullPathToService)
		{
			ChannelFactory<IBankaService> channelFactory = new ChannelFactory<IBankaService>(new WSHttpBinding(SecurityMode.None));
			IBankaService bs = channelFactory.CreateChannel(new EndpointAddress(fullPathToService));
			return bs;
		}

        public void AcceptFactureFromFirm(Faktura faktura, string nazivFirme)
        {
            Dictionary<int, Faktura> fakturaa = new Dictionary<int, Faktura>();
            fakturaa.Add(faktura.IDFakture, faktura);
            this.fakture.Add(nazivFirme, fakturaa);
        }

        public void PrikaziFakture(string nazivFirme)
        {
            int brojFaktura = fakture.Count;

            Console.WriteLine("Broj faktura: " + brojFaktura);
            fakture.TryGetValue(nazivFirme, out Dictionary<int, Faktura> faktureFirme);
            //if (faktureFirme != null)
            //{
            //    Console.WriteLine("Fakture: ");

            //    foreach (Faktura f in faktureFirme.Values)
            //    {
            //        Console.WriteLine("ID fakture je: " + f.IDFakture);
            //    }
            //}
            //else {

            //    Console.WriteLine("Nema fakture firma.");
            //}
            
        }
    }
}
