using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcare_auto
{
    public class Parcare
    {
        private string nume;
        private int nrLocuri;
        private bool[] LocuriOcupate;

        public Parcare()
        {
            this.nume = "publica";
            this.nrLocuri = 5;
            this.LocuriOcupate = new bool[this.nrLocuri];
            for(int i = 0; i < LocuriOcupate.Length; i++)
            {
                this.LocuriOcupate[i] = false;
            }
        }

        public bool this[int index]
        {
            get
            {
                return LocuriOcupate[index];
            }
            set
            {
                LocuriOcupate[index] = value;
            }
        }

    }
}
