using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet_medical
{
    public class Medic
    {
        private int idMedic;
        private string specializare;
        private float tarif;

        public Medic(int idMedic, string specializare, float tarif)
        {
            this.idMedic = idMedic;
            this.specializare = specializare;
            this.tarif = tarif;
        }

        public int IdMedic { get => idMedic; set => idMedic = value; }
        public string Specializare { get => specializare; set => specializare = value; }
        public float Tarif { get => tarif; set => tarif = value; }

        public static bool operator < (Medic m1, Medic m2)
        {
            if (m1.tarif < m2.tarif)
                return true;
            else return false;
        }

        public static bool operator > (Medic m1, Medic m2)
        {
            if (m1.tarif > m2.tarif)
                return true;
            else return false;
        }
    }
}
