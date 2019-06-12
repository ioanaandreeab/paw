using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Smartphones
{
    public class Tablet : Device
    {
        private int nrusb;

        public Tablet()
        {

        }

        public Tablet(int cod, string denumire, string dimensiuni, float frecvproc, int nrcore, string sistemop, float pret, bool activ, int nrusb) 
            : base (cod,denumire,dimensiuni,frecvproc,nrcore,sistemop,pret,activ)
        {
            this.nrusb = nrusb;
        }

        public int Nrusb { get => nrusb; set => nrusb = value; }

        public override ListViewItem Afisare()
        {
            ListViewItem rand = new ListViewItem();
            rand.Text = this.Cod.ToString();
            rand.SubItems.Add(this.Denumire);
            rand.SubItems.Add(this.Dimensiuni);
            rand.SubItems.Add(this.Frecvproc.ToString());
            rand.SubItems.Add(this.Nrcore.ToString());
            rand.SubItems.Add(this.Sistemop.ToString());
            rand.SubItems.Add(this.Pret.ToString());
            rand.SubItems.Add(this.Activ.ToString());
            rand.SubItems.Add(this.Nrusb.ToString());
            rand.Tag = this;
            return rand;
        }

        public override string ToString()
        {
            return string.Format("Tablet - {0}, nume - {1}, dimensiuni - {2}, pret - {3}", this.Cod, this.Denumire, this.Dimensiuni, this.Pret);
        }
    }
}
