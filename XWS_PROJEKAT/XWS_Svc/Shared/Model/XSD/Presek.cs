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
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="ListaStavkiPreseka", Namespace="http://schemas.datacontract.org/2004/07/Shared.Model.XSD", ItemName="Stavke")]
    public class ListaStavkiPreseka : System.Collections.Generic.List<Shared.Model.XSD.StavkaPreseka>
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="StavkaPreseka", Namespace="http://schemas.datacontract.org/2004/07/Shared.Model.XSD")]
    public partial class StavkaPreseka : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int IDStavkePresekaField;
        
        private string DuznikField;
        
        private string SvrhaPlacanjaField;
        
        private string PrimalacField;
        
        private System.DateTime DatumNalogaField;
        
        private System.DateTime DatumValuteField;
        
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
        public int IDStavkePreseka
        {
            get
            {
                return this.IDStavkePresekaField;
            }
            set
            {
                if ((this.IDStavkePresekaField.Equals(value) != true))
                {
                    this.IDStavkePresekaField = value;
                    this.RaisePropertyChanged("IDStavkePreseka");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Duznik
        {
            get
            {
                return this.DuznikField;
            }
            set
            {
                if ((object.ReferenceEquals(this.DuznikField, value) != true))
                {
                    this.DuznikField = value;
                    this.RaisePropertyChanged("Duznik");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string SvrhaPlacanja
        {
            get
            {
                return this.SvrhaPlacanjaField;
            }
            set
            {
                if ((object.ReferenceEquals(this.SvrhaPlacanjaField, value) != true))
                {
                    this.SvrhaPlacanjaField = value;
                    this.RaisePropertyChanged("SvrhaPlacanja");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string Primalac
        {
            get
            {
                return this.PrimalacField;
            }
            set
            {
                if ((object.ReferenceEquals(this.PrimalacField, value) != true))
                {
                    this.PrimalacField = value;
                    this.RaisePropertyChanged("Primalac");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public System.DateTime DatumNaloga
        {
            get
            {
                return this.DatumNalogaField;
            }
            set
            {
                if ((this.DatumNalogaField.Equals(value) != true))
                {
                    this.DatumNalogaField = value;
                    this.RaisePropertyChanged("DatumNaloga");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
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
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Presek", Namespace="http://schemas.datacontract.org/2004/07/Shared.Model.XSD")]
    public partial class Presek : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private Shared.Model.XSD.ListaStavkiPreseka StavkePresekaField;
        
        private int IDPresekaField;
        
        private string BrRacunaField;
        
        private System.DateTime DatumNalogaField;
        
        private double BrPresekaField;
        
        private double PrethodnoStanjeField;
        
        private double BrPromenaUKoristField;
        
        private double UkupnoUKoristField;
        
        private double BrPromenaNaTeretField;
        
        private double UkupnoNaTeretField;
        
        private double NovoStanjeField;
        
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
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public Shared.Model.XSD.ListaStavkiPreseka StavkePreseka
        {
            get
            {
                return this.StavkePresekaField;
            }
            set
            {
                if ((object.ReferenceEquals(this.StavkePresekaField, value) != true))
                {
                    this.StavkePresekaField = value;
                    this.RaisePropertyChanged("StavkePreseka");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int IDPreseka
        {
            get
            {
                return this.IDPresekaField;
            }
            set
            {
                if ((this.IDPresekaField.Equals(value) != true))
                {
                    this.IDPresekaField = value;
                    this.RaisePropertyChanged("IDPreseka");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string BrRacuna
        {
            get
            {
                return this.BrRacunaField;
            }
            set
            {
                if ((object.ReferenceEquals(this.BrRacunaField, value) != true))
                {
                    this.BrRacunaField = value;
                    this.RaisePropertyChanged("BrRacuna");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public System.DateTime DatumNaloga
        {
            get
            {
                return this.DatumNalogaField;
            }
            set
            {
                if ((this.DatumNalogaField.Equals(value) != true))
                {
                    this.DatumNalogaField = value;
                    this.RaisePropertyChanged("DatumNaloga");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public double BrPreseka
        {
            get
            {
                return this.BrPresekaField;
            }
            set
            {
                if ((this.BrPresekaField.Equals(value) != true))
                {
                    this.BrPresekaField = value;
                    this.RaisePropertyChanged("BrPreseka");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public double PrethodnoStanje
        {
            get
            {
                return this.PrethodnoStanjeField;
            }
            set
            {
                if ((this.PrethodnoStanjeField.Equals(value) != true))
                {
                    this.PrethodnoStanjeField = value;
                    this.RaisePropertyChanged("PrethodnoStanje");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=6)]
        public double BrPromenaUKorist
        {
            get
            {
                return this.BrPromenaUKoristField;
            }
            set
            {
                if ((this.BrPromenaUKoristField.Equals(value) != true))
                {
                    this.BrPromenaUKoristField = value;
                    this.RaisePropertyChanged("BrPromenaUKorist");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=7)]
        public double UkupnoUKorist
        {
            get
            {
                return this.UkupnoUKoristField;
            }
            set
            {
                if ((this.UkupnoUKoristField.Equals(value) != true))
                {
                    this.UkupnoUKoristField = value;
                    this.RaisePropertyChanged("UkupnoUKorist");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=8)]
        public double BrPromenaNaTeret
        {
            get
            {
                return this.BrPromenaNaTeretField;
            }
            set
            {
                if ((this.BrPromenaNaTeretField.Equals(value) != true))
                {
                    this.BrPromenaNaTeretField = value;
                    this.RaisePropertyChanged("BrPromenaNaTeret");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=9)]
        public double UkupnoNaTeret
        {
            get
            {
                return this.UkupnoNaTeretField;
            }
            set
            {
                if ((this.UkupnoNaTeretField.Equals(value) != true))
                {
                    this.UkupnoNaTeretField = value;
                    this.RaisePropertyChanged("UkupnoNaTeret");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=10)]
        public double NovoStanje
        {
            get
            {
                return this.NovoStanjeField;
            }
            set
            {
                if ((this.NovoStanjeField.Equals(value) != true))
                {
                    this.NovoStanjeField = value;
                    this.RaisePropertyChanged("NovoStanje");
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
    }
}