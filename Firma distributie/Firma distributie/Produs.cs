using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma_distributie
{
    public class Produs : IComparable<Produs>
    {
        private int id;
        private string nume;
        private int unitati;
        private decimal pret;
        private int furnizorId;

        public Produs()
        {
            this.id = 0;
            this.nume = null;
            this.unitati = 0;
            this.pret = 0;
            this.furnizorId = 0;
        }

        public Produs(int id, string nume, int unitati, decimal pret, int furnizorId)
        {
            this.id = id;
            this.nume = nume;
            this.unitati = unitati;
            this.pret = pret;
            this.furnizorId = furnizorId;
        }

        public int Id { get => id; set => id = value; }
        public string Nume { get => nume; set => nume = value; }
        public int Unitati { get => unitati; set => unitati = value; }
        public decimal Pret { get => pret; set => pret = value; }
        public int FurnizorId { get => furnizorId; set => furnizorId = value; }

        public int CompareTo(Produs other)
        {
            return this.nume.CompareTo(other.nume);
        }

        public static explicit operator decimal(Produs p)
        {
            return p.pret * p.unitati;
        }
    }
}
