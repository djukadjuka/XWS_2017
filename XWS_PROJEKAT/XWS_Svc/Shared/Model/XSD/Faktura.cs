﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shared.Model.XSD
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="ListaStavkiFakture", Namespace="http://schemas.datacontract.org/2004/07/Shared.Model.XSD", ItemName="Stavke")]
    public class ListaStavkiFakture : System.Collections.Generic.List<Shared.Model.XSD.StavkaFakture>
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="StavkaFakture", Namespace="http://schemas.datacontract.org/2004/07/Shared.Model.XSD")]
    public partial class StavkaFakture : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int IDStavkeField;
        
        private double RedniBrField;
        
        private string NazivRobeIliUslugeField;
        
        private double KolicinaField;
        
        private string JedinicaMereField;
        
        private double JedinicnaCenaField;
        
        private double VrednostField;
        
        private double ProcenatRabataField;
        
        private double IznosRabataField;
        
        private double UmanjenoZaRabatField;
        
        private double UkupanPorezField;
        
        private int IDFaktureField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IDStavke
        {
            get
            {
                return this.IDStavkeField;
            }
            set
            {
                if ((this.IDStavkeField.Equals(value) != true))
                {
                    this.IDStavkeField = value;
                    this.RaisePropertyChanged("IDStavke");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double RedniBr
        {
            get
            {
                return this.RedniBrField;
            }
            set
            {
                if ((this.RedniBrField.Equals(value) != true))
                {
                    this.RedniBrField = value;
                    this.RaisePropertyChanged("RedniBr");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string NazivRobeIliUsluge
        {
            get
            {
                return this.NazivRobeIliUslugeField;
            }
            set
            {
                if ((object.ReferenceEquals(this.NazivRobeIliUslugeField, value) != true))
                {
                    this.NazivRobeIliUslugeField = value;
                    this.RaisePropertyChanged("NazivRobeIliUsluge");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public double Kolicina
        {
            get
            {
                return this.KolicinaField;
            }
            set
            {
                if ((this.KolicinaField.Equals(value) != true))
                {
                    this.KolicinaField = value;
                    this.RaisePropertyChanged("Kolicina");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string JedinicaMere
        {
            get
            {
                return this.JedinicaMereField;
            }
            set
            {
                if ((object.ReferenceEquals(this.JedinicaMereField, value) != true))
                {
                    this.JedinicaMereField = value;
                    this.RaisePropertyChanged("JedinicaMere");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public double JedinicnaCena
        {
            get
            {
                return this.JedinicnaCenaField;
            }
            set
            {
                if ((this.JedinicnaCenaField.Equals(value) != true))
                {
                    this.JedinicnaCenaField = value;
                    this.RaisePropertyChanged("JedinicnaCena");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=6)]
        public double Vrednost
        {
            get
            {
                return this.VrednostField;
            }
            set
            {
                if ((this.VrednostField.Equals(value) != true))
                {
                    this.VrednostField = value;
                    this.RaisePropertyChanged("Vrednost");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=7)]
        public double ProcenatRabata
        {
            get
            {
                return this.ProcenatRabataField;
            }
            set
            {
                if ((this.ProcenatRabataField.Equals(value) != true))
                {
                    this.ProcenatRabataField = value;
                    this.RaisePropertyChanged("ProcenatRabata");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=8)]
        public double IznosRabata
        {
            get
            {
                return this.IznosRabataField;
            }
            set
            {
                if ((this.IznosRabataField.Equals(value) != true))
                {
                    this.IznosRabataField = value;
                    this.RaisePropertyChanged("IznosRabata");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=9)]
        public double UmanjenoZaRabat
        {
            get
            {
                return this.UmanjenoZaRabatField;
            }
            set
            {
                if ((this.UmanjenoZaRabatField.Equals(value) != true))
                {
                    this.UmanjenoZaRabatField = value;
                    this.RaisePropertyChanged("UmanjenoZaRabat");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=10)]
        public double UkupanPorez
        {
            get
            {
                return this.UkupanPorezField;
            }
            set
            {
                if ((this.UkupanPorezField.Equals(value) != true))
                {
                    this.UkupanPorezField = value;
                    this.RaisePropertyChanged("UkupanPorez");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=11)]
        public int IDFakture
        {
            get
            {
                return this.IDFaktureField;
            }
            set
            {
                if ((this.IDFaktureField.Equals(value) != true))
                {
                    this.IDFaktureField = value;
                    this.RaisePropertyChanged("IDFakture");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

		public override string ToString()
		{
			string str = "";

			str += "\t------------Stavka : [" + this.IDStavke + "]\n";
			str += "\tID-FAKTURE : [" + this.IDFakture + "]\n";
			str += "\tIZNOS-RABATA : [" + this.IznosRabata + "]\n";
			str += "\tJEDINICA-MERE : [" + this.JedinicaMere + "]\n";
			str += "\tJEDINICNA-CENA : [" + this.JedinicnaCena + "]\n";
			str += "\tKOLICINA : [" + this.Kolicina + "]\n";
			str += "\tNAZIV-ROBE-ILI-USLUGE : [" + this.NazivRobeIliUsluge + "]\n";
			str += "\tPROCENA-RABATA : [" + this.ProcenatRabata + "]\n";
			str += "\tREDNI-BR : [" + this.RedniBr + "]\n";
			str += "\tUKUPAN-POREZ : [" + this.UkupanPorez + "]\n";
			str += "\tUMANJENO-ZA-RABAT : [" + this.UmanjenoZaRabat + "]\n";
			str += "\tVREDNOST : [" + this.Vrednost + "]\n";

			return str;
		}
	}
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Faktura", Namespace="http://schemas.datacontract.org/2004/07/Shared.Model.XSD")]
    public partial class Faktura : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int IDFaktureField;
        
        private string IDPorukeField;
        
        private string NazivDobavljacaField;
        
        private string AdresaDobavljacaField;
        
        private string PIBDobavljacaField;
        
        private string NazivKupcaField;
        
        private string AdresaKupcaField;
        
        private string PIBKupcaField;
        
        private double BrRacunaField;
        
        private System.DateTime DatumRacunaField;
        
        private double VrednostRobeField;
        
        private double VrednostUslugaField;
        
        private double UkupnoRobaIUslugeField;
        
        private double UkupanRabatField;
        
        private double UkupanPorezField;
        
        private string OznakaValuteField;
        
        private double IznosZaUplatuField;
        
        private string UplataNaRacunField;
        
        private System.DateTime DatumValuteField;
        
        private Shared.Model.XSD.ListaStavkiFakture StavkeFaktureField;
        
        private string StatusField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IDFakture
        {
            get
            {
                return this.IDFaktureField;
            }
            set
            {
                if ((this.IDFaktureField.Equals(value) != true))
                {
                    this.IDFaktureField = value;
                    this.RaisePropertyChanged("IDFakture");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string IDPoruke
        {
            get
            {
                return this.IDPorukeField;
            }
            set
            {
                if ((object.ReferenceEquals(this.IDPorukeField, value) != true))
                {
                    this.IDPorukeField = value;
                    this.RaisePropertyChanged("IDPoruke");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string NazivDobavljaca
        {
            get
            {
                return this.NazivDobavljacaField;
            }
            set
            {
                if ((object.ReferenceEquals(this.NazivDobavljacaField, value) != true))
                {
                    this.NazivDobavljacaField = value;
                    this.RaisePropertyChanged("NazivDobavljaca");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string AdresaDobavljaca
        {
            get
            {
                return this.AdresaDobavljacaField;
            }
            set
            {
                if ((object.ReferenceEquals(this.AdresaDobavljacaField, value) != true))
                {
                    this.AdresaDobavljacaField = value;
                    this.RaisePropertyChanged("AdresaDobavljaca");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string PIBDobavljaca
        {
            get
            {
                return this.PIBDobavljacaField;
            }
            set
            {
                if ((object.ReferenceEquals(this.PIBDobavljacaField, value) != true))
                {
                    this.PIBDobavljacaField = value;
                    this.RaisePropertyChanged("PIBDobavljaca");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string NazivKupca
        {
            get
            {
                return this.NazivKupcaField;
            }
            set
            {
                if ((object.ReferenceEquals(this.NazivKupcaField, value) != true))
                {
                    this.NazivKupcaField = value;
                    this.RaisePropertyChanged("NazivKupca");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string AdresaKupca
        {
            get
            {
                return this.AdresaKupcaField;
            }
            set
            {
                if ((object.ReferenceEquals(this.AdresaKupcaField, value) != true))
                {
                    this.AdresaKupcaField = value;
                    this.RaisePropertyChanged("AdresaKupca");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public string PIBKupca
        {
            get
            {
                return this.PIBKupcaField;
            }
            set
            {
                if ((object.ReferenceEquals(this.PIBKupcaField, value) != true))
                {
                    this.PIBKupcaField = value;
                    this.RaisePropertyChanged("PIBKupca");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=8)]
        public double BrRacuna
        {
            get
            {
                return this.BrRacunaField;
            }
            set
            {
                if ((this.BrRacunaField.Equals(value) != true))
                {
                    this.BrRacunaField = value;
                    this.RaisePropertyChanged("BrRacuna");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=9)]
        public System.DateTime DatumRacuna
        {
            get
            {
                return this.DatumRacunaField;
            }
            set
            {
                if ((this.DatumRacunaField.Equals(value) != true))
                {
                    this.DatumRacunaField = value;
                    this.RaisePropertyChanged("DatumRacuna");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=10)]
        public double VrednostRobe
        {
            get
            {
                return this.VrednostRobeField;
            }
            set
            {
                if ((this.VrednostRobeField.Equals(value) != true))
                {
                    this.VrednostRobeField = value;
                    this.RaisePropertyChanged("VrednostRobe");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=11)]
        public double VrednostUsluga
        {
            get
            {
                return this.VrednostUslugaField;
            }
            set
            {
                if ((this.VrednostUslugaField.Equals(value) != true))
                {
                    this.VrednostUslugaField = value;
                    this.RaisePropertyChanged("VrednostUsluga");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=12)]
        public double UkupnoRobaIUsluge
        {
            get
            {
                return this.UkupnoRobaIUslugeField;
            }
            set
            {
                if ((this.UkupnoRobaIUslugeField.Equals(value) != true))
                {
                    this.UkupnoRobaIUslugeField = value;
                    this.RaisePropertyChanged("UkupnoRobaIUsluge");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=13)]
        public double UkupanRabat
        {
            get
            {
                return this.UkupanRabatField;
            }
            set
            {
                if ((this.UkupanRabatField.Equals(value) != true))
                {
                    this.UkupanRabatField = value;
                    this.RaisePropertyChanged("UkupanRabat");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=14)]
        public double UkupanPorez
        {
            get
            {
                return this.UkupanPorezField;
            }
            set
            {
                if ((this.UkupanPorezField.Equals(value) != true))
                {
                    this.UkupanPorezField = value;
                    this.RaisePropertyChanged("UkupanPorez");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=15)]
        public string OznakaValute
        {
            get
            {
                return this.OznakaValuteField;
            }
            set
            {
                if ((object.ReferenceEquals(this.OznakaValuteField, value) != true))
                {
                    this.OznakaValuteField = value;
                    this.RaisePropertyChanged("OznakaValute");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=16)]
        public double IznosZaUplatu
        {
            get
            {
                return this.IznosZaUplatuField;
            }
            set
            {
                if ((this.IznosZaUplatuField.Equals(value) != true))
                {
                    this.IznosZaUplatuField = value;
                    this.RaisePropertyChanged("IznosZaUplatu");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=17)]
        public string UplataNaRacun
        {
            get
            {
                return this.UplataNaRacunField;
            }
            set
            {
                if ((object.ReferenceEquals(this.UplataNaRacunField, value) != true))
                {
                    this.UplataNaRacunField = value;
                    this.RaisePropertyChanged("UplataNaRacun");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=18)]
        public System.DateTime DatumValute
        {
            get
            {
                return this.DatumValuteField;
            }
            set
            {
                if ((this.DatumValuteField.Equals(value) != true))
                {
                    this.DatumValuteField = value;
                    this.RaisePropertyChanged("DatumValute");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=19)]
        public Shared.Model.XSD.ListaStavkiFakture StavkeFakture
        {
            get
            {
                return this.StavkeFaktureField;
            }
            set
            {
                if ((object.ReferenceEquals(this.StavkeFaktureField, value) != true))
                {
                    this.StavkeFaktureField = value;
                    this.RaisePropertyChanged("StavkeFakture");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=20)]
        public string Status
        {
            get
            {
                return this.StatusField;
            }
            set
            {
                if ((object.ReferenceEquals(this.StatusField, value) != true))
                {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

		public override string ToString()
		{
			string str = "";

			str += "---------------FAKTURA : ["+this.IDFakture+"]\n";
			str += "ADRESA-DOBAVLJACA : [" + this.AdresaDobavljaca+ "]\n";
			str += "ADRESA-KUPCA : [" + this.AdresaKupca+ "]\n";
			str += "BROJ-RACUNA : [" + this.BrRacuna+ "]\n";
			str += "DATUM-RACUNA : [" + this.DatumRacuna+ "]\n";
			str += "DATUM-VALUTE : [" + this.DatumValute+ "]\n";
			str += "ID-PORUKE : [" + this.IDPoruke+ "]\n";
			str += "IZNOS-ZA-UPLATU : [" + this.IznosZaUplatu+ "]\n";
			str += "NAZIV-DOBAVLJACA : [" + this.NazivDobavljaca+ "]\n";
			str += "NAZIV-KUPCA : [" + this.NazivKupca+ "]\n";
			str += "OZNAKA-VALUTE : [" + this.OznakaValute+ "]\n";
			str += "PIB-DOBAVLJACA : [" + this.PIBDobavljaca+ "]\n";
			str += "PIB-KUPCA : [" + this.PIBKupca+ "]\n";
			str += "STATUS : [" + (this.Status=="0"?"Kreirana":(this.Status=="1")?"Poslata":"Placeno") + "]\n";
			str += "UKUPAN-POREZ : [" + this.UkupanPorez+ "]\n";
			str += "UKUPAN-RABAT : [" + this.UkupanRabat+ "]\n";
			str += "UKUPNO-ROBA-I-USLUGE : [" + this.UkupnoRobaIUsluge+ "]\n";
			str += "UPLATA-NA-RACUN : [" + this.UplataNaRacun+ "]\n";
			str += "VREDNOST-ROBE : [" + this.VrednostRobe+ "]\n";
			str += "VREDNOST-USLUGE : [" + this.VrednostUsluga+ "]\n";
			str += "STAVKE : [\n" + this.StavkeFakture + "]\n";

			str += "STAVKE : [\n";
			foreach (var s in this.StavkeFakture)
			{
				str += s.ToString();
			}
			str += "]";
			return str;
		}
	}
}
