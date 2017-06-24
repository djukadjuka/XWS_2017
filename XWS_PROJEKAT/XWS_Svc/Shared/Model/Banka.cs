using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWS_Svc.Shared.Model
{
    public class Banka
    {
        private int idBanka;
        private string naziv;
        private string swift;
        private string racun;

        private List<Racun> racuni = new List<Racun>();


        public Banka()
        {

        }

        public Banka(int idBanka, string naziv, string swift, string racun)
        {
            this.idBanka = idBanka;
            this.naziv = naziv;
            this.swift = swift;
            this.racun = racun;
        }

        public int IdBanka
        {
            get { return idBanka; }
            set
            {
                idBanka = value;
                
            }
        }

        public string Naziv
        {
            get { return naziv; }
            set
            {
                naziv = value;

            }
        }

        public string Swift
        {
            get { return swift; }
            set
            {
                swift = value;
               
            }
        }

        public string Racun
        {
            get { return racun; }
            set
            {
                racun = value;
                
            }
        }


    }
}
