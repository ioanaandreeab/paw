using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcare_auto
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            
            InitializeComponent();
            Parcare parcare = new Parcare();
            parcare[2] = true;
            parcare[3] = true;
            PictureBox[] pictureBoxes = new PictureBox[5];
            for(int i = 0; i < pictureBoxes.Length; i++)
            {
                pictureBoxes[i] = new PictureBox();
                if (parcare[i] == false)
                {
                    pictureBoxes[i].BackColor = Color.Green;
                    pictureBoxes[i].Location = new Point((i * 60) + 10, 10);
                    pictureBoxes[i].Size = new Size(50, 100);
                    this.Controls.Add(pictureBoxes[i]);

                }
                else
                {
                    pictureBoxes[i].BackColor = Color.Red;
                    pictureBoxes[i].Location = new Point((i * 60) + 10, 10);
                    pictureBoxes[i].Size = new Size(50, 100);
                    this.Controls.Add(pictureBoxes[i]);
                }
            }
        }
    }
}
