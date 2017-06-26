﻿using Shared.Model.XSD;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using XWS.Shared;
using XWS.Shared.BP;
using XWS.Shared.Model;
using XWS.Shared.Model.InterfejsiServisa;

namespace ConsoleClient
{
    public class Program
    {

        static void Main(string[] args)
        {
            var wsHttpBinding = new WSHttpBinding(SecurityMode.None);
            wsHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            wsHttpBinding.Security.Message.EstablishSecurityContext = false;

			var address = new EndpointAddress(XWS.Shared.GlobalConst.HOST_ADDRESS + XWS.Shared.GlobalConst.FIRME_SERVICE_NAME + "?wsdl");
			var client = new FirmaClient(wsHttpBinding, address);

			while (true)
            {
				LinijaUkras();
				Console.Write("Unesite Naziv Firme: ");
				string firmName = Console.ReadLine();

				if (firmName == "") continue;

				Firma firma = FirmaDB.GetFirmaByName(firmName);
				if (firma != null)
				{
					LinijaUkras();
					Console.WriteLine(firma);
					LinijaUkras();

					StampajMeni();

					string izbor = Console.ReadLine();

					IzabranMeni(firma,izbor, client);
				}
			}
        }

		public static void LinijaUkras()
		{
			Console.WriteLine("------------------------------\n");
		}

		public static void StampajMeni()
		{
			Console.WriteLine("1.) Napravi Fakturu\n");
			Console.WriteLine("2.) Prikazi Sve Kreirane Fakture\n");
			Console.WriteLine("3.) Prikazi Sve Narucene Fakture\n");
			Console.WriteLine("4.) Posalji Fakturu Kupcu\n");
            Console.WriteLine("5.) Napravi nalog za prenos\n");
            Console.WriteLine("6.) Banka neka uradi CLEARING & SETTLEMENT\n");
		}

		public static bool IzabranMeni(Firma sourceFirma,string izbor, FirmaClient client)
		{
			switch(izbor)
			{
				case "1":
				{
					NapraviNovuFakturu(sourceFirma, client);
					break;
				}
				case "2":
				{
					//sve kreirane fakture trazis sa 0 - one koji si TI napravio (generalno sve ikad)
					PrikaziSveFakture(sourceFirma, client, GlobalConst.STATUS_FAKTURE_KREIRANA);
					break;
				}
				case "3":
				{
					//sve narucene fakture trazis sa 1 - one koje su TEBI posalte (ti si kupac, status je 1)
					PrikaziSveFakture(sourceFirma, client, GlobalConst.STATUS_FAKTURE_POSLATA);
					break;
				}
				case "4":
				{
					PosaljiFakturuKupcu(sourceFirma, client, GlobalConst.STATUS_FAKTURE_KREIRANA);
					break;
				}
                case "5":
                {
                     NapraviNalogZaPrenos(sourceFirma, client);
                     break;
                }
                case "6":
                {
                    OdradiClearingAndSettlement();
                    break;
                }
                default:
				{
					Console.WriteLine("Izbor NE POSTOJI!\n");
					return false;
				}
			}
			return true;
		}

        private static void PosaljiFakturuKupcu(Firma sourceFirma, FirmaClient client, string v)
		{
			PrikaziSveFakture(sourceFirma, client, v);

			while(true)
			{
				Console.WriteLine("Unesite broj fakture koju zelite da posaljete kupcu.");
				string id_string = Console.ReadLine();
				try
				{
					int id = Int32.Parse(id_string);
					Faktura fakt = FakturaDB.GetFaktura(id);
					if (fakt != null)
					{
                        //client.SendCreatedInvoice(id);
                        client.PromeniStatusFakture(fakt.IDFakture, GlobalConst.STATUS_FAKTURE_POSLATA);
						break;
					}
				}catch(Exception e)
				{
					//pass
				}
			}
		}

		public static void NapraviNovuFakturu(Firma sourceFirma, FirmaClient client)
		{
			Faktura fakt = new Faktura();
			
			LinijaUkras();
			Console.WriteLine("UNOS NOVE FAKTURE:");
			LinijaUkras();

			fakt.NazivKupca = "";
			
			Firma kupac;
			while (true)
			{
				Console.Write("Unesite [NAZIV KUPCA](Q za izlaz) : ");
				string nazivKupca = Console.ReadLine();
				if(nazivKupca == "Q" || nazivKupca == "q")
				{
					return;
				}
				kupac = FirmaDB.GetFirmaByName(nazivKupca);
				if (kupac != null && kupac.IDFirme != sourceFirma.IDFirme) break;
			}

			fakt.IDPoruke = "1234";

			fakt.NazivDobavljaca = sourceFirma.NazivFirme;
			fakt.AdresaDobavljaca = sourceFirma.AdresaFirme;
			fakt.PIBDobavljaca = sourceFirma.PIB;

			fakt.NazivKupca = kupac.NazivFirme;
			fakt.AdresaKupca = kupac.AdresaFirme;
			fakt.PIBKupca = kupac.PIB;

            fakt.BrRacuna = 1;//kupac.Racun;
			fakt.UplataNaRacun = sourceFirma.Racun.ToString();

			fakt.DatumRacuna = DateTime.Now;
			fakt.DatumValute = DateTime.Now;

			fakt.VrednostRobe = 1000;
			fakt.VrednostUsluga = 1000;
			fakt.UkupnoRobaIUsluge = fakt.VrednostRobe + fakt.VrednostUsluga;

			fakt.UkupanRabat = 10;
			fakt.UkupanPorez = 5;

			fakt.IznosZaUplatu = fakt.UkupnoRobaIUsluge + 
								fakt.UkupnoRobaIUsluge * (fakt.UkupanPorez / 100) - 
								fakt.UkupnoRobaIUsluge * (fakt.UkupanRabat / 100);

			fakt.OznakaValute = "RSD";
			fakt.Status = GlobalConst.STATUS_FAKTURE_KREIRANA;

			ListaStavkiFakture listaStavki = new ListaStavkiFakture();
			StavkaFakture stavka1 = new StavkaFakture();
			StavkaFakture stavka2 = new StavkaFakture();

			stavka1.RedniBr = 1;
			stavka1.NazivRobeIliUsluge = "Televizor";
			stavka1.Kolicina = 2;
			stavka1.JedinicaMere = "Komad";
			stavka1.JedinicnaCena = 100;
			stavka1.Vrednost = 200;
			stavka1.ProcenatRabata = 5;
			stavka1.IznosRabata = 50;
			stavka1.UmanjenoZaRabat = 55;
			stavka1.UkupanPorez = 1234;

			stavka2.RedniBr = 2;
			stavka2.NazivRobeIliUsluge = "Rad";
			stavka2.Kolicina = 1;
			stavka2.JedinicaMere = "Puno";
			stavka2.JedinicnaCena = 500;
			stavka2.Vrednost = 10;
			stavka2.ProcenatRabata = 123;
			stavka2.IznosRabata = 5;
			stavka2.UmanjenoZaRabat = 4;
			stavka2.UkupanPorez = 17;

			listaStavki.Add(stavka1);
			listaStavki.Add(stavka2);

			fakt.StavkeFakture = listaStavki;

			client.SaveCreatedInvoice(fakt);
		}

		public static void PrikaziSveFakture(Firma sourceFirma, FirmaClient client, String kreirane_narucene)
		{
			LinijaUkras();
			Console.WriteLine("Sve " + kreirane_narucene + " Fakture : ");
			List<Faktura> fakture = client.GetForCompanyAndStatus(sourceFirma,kreirane_narucene);
			foreach(var f in fakture)
			{
				Console.WriteLine(f);
			}
		}

        public static void NapraviNalogZaPrenos(Firma sourceFirma, FirmaClient client)
        {
            PrikaziSveFakture(sourceFirma, client, GlobalConst.STATUS_FAKTURE_POSLATA);

            while (true)
            {
                Console.WriteLine("Izaberi fakturu od koje hoces da napravis nalog za prenos(Q za izlaz):");
                string id_string = Console.ReadLine();
                if (id_string == "Q" || id_string == "q")
                {
                    return;
                }
                try
                {
                    int id = Int32.Parse(id_string);
                    Faktura fakt = FakturaDB.GetFaktura(id);
                    if (fakt != null)
                    {
                        while (true)
                        {
                            Console.WriteLine("Da li je hitno? (y/n):");
                            string odgovor = Console.ReadLine();
                            if (odgovor == "Q" || odgovor == "q")
                            {
                                return;
                            }
                            if(odgovor == "y" || odgovor=="Y" || odgovor=="n" || odgovor == "N")
                            {
                                bool hitno;
                                if (odgovor == "y" || odgovor == "Y")
                                    hitno = true;
                                 else
                                    hitno = false;
                                NalogZaPlacanje nzp = generisiNZP(sourceFirma, hitno, fakt);
                                
                                client.NapraviNalogZaPrenos(nzp);

								// TODO:Da li treba ovde da se promeni na placena, ili tek kada stigne poruka o odobrenju?
                                client.PromeniStatusFakture(fakt.IDFakture, GlobalConst.STATUS_FAKTURE_PLACENA);
                                break;
                            }
                        }
                            
                        break;
                    }
                }
                catch (Exception e)
                {
                    //pass
                }
            }
        }

        public static NalogZaPlacanje generisiNZP(Firma sourceFirma, bool hitno, Faktura faktura)
        {
            NalogZaPlacanje nzp = new NalogZaPlacanje();
            nzp.Hitno = hitno;
            nzp.DatumNaloga = DateTime.Now;
            nzp.DatumValute = DateTime.Now;
            nzp.Duznik = faktura.NazivKupca;
            nzp.IDPoruke = faktura.IDPoruke;
            nzp.Iznos = faktura.IznosZaUplatu;
            nzp.ModelOdobrenja = 97;
            nzp.ModelZaduzenja = 97;
            nzp.OznakaValute = faktura.OznakaValute;
            nzp.PozivNaBrOdobrenja = 123;
            nzp.PozivNaBrZaduzenja = "123";
            nzp.Primalac = faktura.NazivDobavljaca;
            nzp.RacunPoverioca = faktura.UplataNaRacun;
            nzp.RacunDuznika = sourceFirma.Racun.ToString();
            nzp.Status = GlobalConst.STATUS_NALOGA_ZA_PLACANJE_KREIRAN;
            nzp.SvrhaPlacanja = "dug";
            /*if (nzp.Iznos > 250000)
                nzp.Status = "1";
            else
                nzp.Status = ""*/

            Console.WriteLine(nzp);
            return nzp;
        }


        private static void OdradiClearingAndSettlement()
        {
            //ovde ide logika clearing and settlementa, prosledi jos neke parametre ako treba
            ChannelFactory<IFirmaService> channelFactory = new ChannelFactory<IFirmaService>(new WSHttpBinding(SecurityMode.None));
            IFirmaService fs = channelFactory.CreateChannel(new EndpointAddress(GlobalConst.HOST_ADDRESS + GlobalConst.CENTRALNA_BANKA_NAME));
            fs.NapraviNalogZaGrupnoPlacanje(); 
        }
    }  
}
