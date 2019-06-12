using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unitate_turistica
{
    public class Unitate_turistica 
    {
        private List<Camera> camere;

        public List<Camera> Camere { get => camere; set => camere = value; }

        public Unitate_turistica(List<Camera> camere)
        {
            this.camere = camere;
        }

        public Unitate_turistica (string fileName)
        {
            string[] linii = File.ReadAllLines(fileName);
            this.camere = new List<Camera>();
            for (int i = 0; i < linii.Length; i = i + 4)
            {
                this.camere.Add(new Camera(
                    nrCamera: int.Parse(linii[i]),
                    tipCamera: char.Parse(linii[i + 1]),
                    esteOcupata: bool.Parse(linii[i + 2]),
                    dataCazare: linii[i + 3]));
            }
        }

        public static bool operator >(Unitate_turistica u1, Unitate_turistica u2)
        {
            if (u1.camere.Count > u2.camere.Count)
                return true;
            else return false;
        }

        public static bool operator <(Unitate_turistica u1, Unitate_turistica u2)
        {
            if (u1.camere.Count < u2.camere.Count)
                return true;
            else return false;
        }

       public static Unitate_turistica operator +(Unitate_turistica u, Camera c)
        {
            u.camere.Add(c);
            return u;
        }

        public override string ToString()
        {
            string rezultat = null;
            foreach(Camera camera in camere)
            {
                rezultat += "\n"+camera.ToString();
            }
            return rezultat;
        }

        /*public object Clone()
        {
            Unitate_turistica unitate = new Unitate_turistica(null);
            foreach(Camera camera in this.camere)
            {
                unitate.camere.Add(new Camera(camera.NrCamera,camera.TipCamera,camera.EsteOcupata,camera.DataCazare));
            }

            return unitate;
        }*/
    }
}
