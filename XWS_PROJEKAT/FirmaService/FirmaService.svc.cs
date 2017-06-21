using System;
using System.ServiceModel;

namespace FirmaService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class FirmaService : IFirmaService
    {
        private string nazivFirme;

        public FirmaService(string nazivFirme)
        {
            this.nazivFirme = nazivFirme;
        }

        public string GetData(int value)
        {
            return string.Format("Odgovor od firme:" + nazivFirme + "   Vrednost je: ", value);
        }

        public FakturaResponse SlanjeFakture(Faktura faktura)
        {
            Console.WriteLine("Odgovor od firme:" + nazivFirme + "  Faktura sa id-jem: " + faktura.Idfakture);
            FakturaResponse fakturaResponse = new FakturaResponse();
            fakturaResponse.Success = true;

            return fakturaResponse;
        }
    }
}
