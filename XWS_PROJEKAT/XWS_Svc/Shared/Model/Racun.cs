using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWS_Svc.Shared.Model
{
    class Racun
    {
        private int idRacun;
        private string racun;
        private double stanje;

        public Racun()
        {

        }

        public Racun(int idRacun, string racun, double stanje)
        {
            this.idRacun = idRacun;
            this.racun = racun;
            this.stanje = stanje;
        }

        public int IdRacun
        {
            get { return idRacun; }
            set
            {
                idRacun = value;
            }
        }

        public string BrojRacun
        {
            get { return racun; }
            set
            {
                racun = value;
            }
        }

        public double Stanje
        {
            get { return stanje; }
            set
            {
                stanje = value;
            }
        }
    }
}
