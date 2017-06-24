using FirmaService;
using Shared.Model.XSD;
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


            while (true)
            {
                //Console.WriteLine("1.Read message from bank.");
                //string command = Console.ReadLine();
                //if(command == "1"){
                //	Console.WriteLine(client.ReturnMessageFromBank());
                //}
                Console.WriteLine("Unesi naziv firme sa kojom hoces da radis. ");               

                string nazivFirmeA = Console.ReadLine();

                var address = new EndpointAddress("http://localhost:8080/" + nazivFirmeA + "?wsdl");
                var client = new FirmaClient(wsHttpBinding, address);

                Console.WriteLine("***************************************");
                Console.WriteLine(nazivFirmeA);
                Console.WriteLine("***************************************");

                Console.WriteLine("Opcije: \n");

                Console.WriteLine("1. Slanje fakture. ");
                Console.WriteLine("2. Slanje naloga za placanje. ");
                Console.WriteLine("3. Prikazi fakture. ");



                string command = Console.ReadLine();

                if (command == "1")
                {
                    Console.WriteLine("Unesi naziv firme kojoj se salje faktura: ");
                    string nazivFirmeB = Console.ReadLine();


                    Faktura faktura = new Faktura();
                    faktura.IDFakture = 1;
                    faktura.IDPoruke = "1 IDPoruke";
                    faktura.IznosZaUplatu = 120000.50;
                    faktura.NazivDobavljaca = "1 Dobavljac naziv";
                    faktura.NazivKupca = "1 Kupac naziv";
                    faktura.OznakaValute = "RSD";
                    faktura.PIBDobavljaca = "1 PIB dobavljaca";
                    faktura.PIBKupca = "1 PIB kupca";
                    faktura.UkupanPorez = 1000;
                    faktura.UkupanRabat = 5000;
                    faktura.UkupnoRobaIUsluge = 2000000;
                    faktura.UplataNaRacun = "123-456789";
                    faktura.VrednostRobe = 1000000;
                    faktura.VrednostUsluga = 500000;
                    ListaStavkiFakture stavke = new ListaStavkiFakture();
                    StavkaFakture stavka = new StavkaFakture();
                    stavka.IDFakture = 1;
                    stavka.IDStavke = 1;
                    stavka.IznosRabata = 1000;
                    stavka.JedinicaMere = "kg";
                    stavka.JedinicnaCena = 12;
                    stavka.Kolicina = 100;
                    stavka.NazivRobeIliUsluge = "1 Naziv robe ili usluge";
                    stavka.ProcenatRabata = 0.5;
                    stavka.RedniBr = 1;
                    stavka.UkupanPorez = 5000;
                    stavka.UmanjenoZaRabat = 3000;
                    stavka.Vrednost = 2200;

                    stavke.Add(stavka);

                    faktura.StavkeFakture = stavke;

                    client.SlanjeFakture(faktura, nazivFirmeB);
                }
                else if (command == "3") {
                    Console.WriteLine("PRIKAYI FAKTURE!!!");
                    client.PrikaziFakture(nazivFirmeA);
                    Console.WriteLine("KRAJ PRIKAYI FAKTURE!!!");

                }

            }
        }
    }
}
