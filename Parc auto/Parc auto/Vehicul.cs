using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Parc_auto
{
    [Serializable]
    public class Vehicul : IComparable<Vehicul>
    {
        private int cod;
        private string denumire;
        private float tonaj;

        public Vehicul(int cod, string denumire, float tonaj)
        {
            this.cod = cod;
            this.denumire = denumire;
            this.tonaj = tonaj;
        }

        public float Tonaj { get => tonaj; set => tonaj = value; }
        public string Denumire { get => denumire; set => denumire = value; }
        public int Cod { get => cod; set => cod = value; }

        public static bool operator <(Vehicul v1, Vehicul v2)
        {
            if (v1.tonaj < v2.tonaj) return true;
            else return false;
        }

        public static bool operator >(Vehicul v1, Vehicul v2)
        {
            if (v1.tonaj > v2.tonaj) return true;
            else return false;
        }

        public override string ToString()
        {
            return string.Format("Vehiculul {0}, denumire - {1}, tonaj - {2}", this.cod, this.denumire, this.tonaj);
        }

        //sorteaza crescator
        public int CompareTo(Vehicul other)
        {
            if (this < other) return -1;
            else if (this > other) return 1;
            else return 0;
        }
    }
}
