using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Smartphones
{
    public abstract class Device
    {
        private int cod;
        private string denumire;
        private string dimensiuni;
        private float frecvproc;
        private int nrcore;
        private string sistemop;
        private float pret;
        private bool activ;

        public Device ()
        {

        }
        public Device(int cod, string denumire, string dimensiuni, float frecvproc, int nrcore, string sistemop, float pret, bool activ)
        {
            this.cod = cod;
            this.denumire = denumire;
            this.dimensiuni = dimensiuni;
            this.frecvproc = frecvproc;
            this.nrcore = nrcore;
            this.sistemop = sistemop;
            this.pret = pret;
            this.activ = activ;
        }

        public abstract ListViewItem Afisare();

        public int Cod { get => cod; set => cod = value; }
        public string Denumire { get => denumire; set => denumire = value; }
        public string Dimensiuni { get => dimensiuni; set => dimensiuni = value; }
        public float Frecvproc { get => frecvproc; set => frecvproc = value; }
        public int Nrcore { get => nrcore; set => nrcore = value; }
        public string Sistemop { get => sistemop; set => sistemop = value; }
        public float Pret { get => pret; set => pret = value; }
        public bool Activ { get => activ; set => activ = value; }
    }
}
