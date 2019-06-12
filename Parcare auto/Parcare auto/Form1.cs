using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcare_auto
{
    public partial class Form1 : Form
    {
        List<Auto> flota = new List<Auto>();
        ListView listView = new ListView();
        public Form1()
        {
            InitializeComponent();
            Auto a1 = new Auto("B 57 VXC", true);
            Auto a2 = new Auto("B 12 VXC", false);
            Auto a3 = new Auto("B 27 VXC", true);
            Auto a4 = new Auto("B 53 VXC", true);
            Auto a5 = new Auto("B 80 VXC", true);
            flota.Add(a1);
            flota.Add(a2);
            flota.Add(a3);
            flota.Add(a4);
            flota.Add(a5);
        }

        private void parkingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 formular = new Form2();
            formular.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream("flota.dat", FileMode.Create, FileAccess.Write);
            bf.Serialize(fs,flota);
            fs.Close();
            MessageBox.Show("Flota a fost serializata cu succes!");

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream("flota.dat",FileMode.Open,FileAccess.Read);
            flota = (List<Auto>)bf.Deserialize(fs);
            fs.Close();
            MessageBox.Show("Flota a fost deserializata cu succes!");
            Afisare();
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializareListView();
            Afisare();
        }

        private void InitializareListView()
        {
            listView.Dock = DockStyle.Bottom;
            listView.Width = 500;
            listView.Height = 430;
            listView.View = View.Details;
            listView.MultiSelect = false;
            listView.FullRowSelect = false;
            listView.LabelEdit = true;
            listView.AfterLabelEdit += ListView_AfterLabelEdit;

            listView.ContextMenuStrip = contextMenuStrip1;

            listView.Columns.Add("Numar inmatriculare", 150);
            listView.Columns.Add("Este in cursa?", 100);
            this.Controls.Add(listView);
        }

        private void ListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            ListViewItem rand = listView.SelectedItems[0]; //elementul pe care il editez
            Auto selectat = (Auto)rand.Tag;
            foreach (Auto auto in flota)
            {
                if (auto.Equals(selectat))
                    auto.NrInmatriculare = e.Label;
            }
        }

        private void Afisare()
        {
            listView.Items.Clear();
            foreach (Auto auto in flota)
            {
                ListViewItem rand = new ListViewItem();
                rand.Text = auto.NrInmatriculare;
                rand.SubItems.Add(auto.InCursa.ToString());
                rand.Tag = auto;
                listView.Items.Add(rand);
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(listView.SelectedItems.Count > 0)
            {
                ListViewItem rand = listView.SelectedItems[0];
                listView.Items.Remove(rand);
                Auto auto = (Auto)rand.Tag;
                flota.Remove(auto);
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Auto auto = new Auto("B 00 XXX",false);
            flota.Add(auto);
            Afisare();
        }
    }
}
