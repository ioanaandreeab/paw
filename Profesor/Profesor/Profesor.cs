using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profesor
{
    public class Profesor : IPremiere
    {
        private int marca;
        private string nume;
        private double salariu;

        public Profesor(int marca, string nume, double salariu)
        {
            this.marca = marca;
            this.nume = nume;
            this.salariu = salariu;
        }

        public int Marca { get => marca; set => marca = value; }
        public string Nume { get => nume; set => nume = value; }
        public double Salariu { get => salariu; set => salariu = value; }

        public void Premiaza()
        {
            this.salariu = salariu * 1.3;
        }

        public static double operator+(double salariu, Profesor p)
        {
            return salariu + p.Salariu;
        }

        public override string ToString()
        {
            return string.Format("Profesorul - {0} - {1} are salariul de {2} lei.",this.marca, this.nume, this.salariu);
        }
    }
}
