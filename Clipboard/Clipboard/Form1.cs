using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clipboard
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = @"C:\Users\ioana\Desktop\stickers\19.PNG";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = System.Windows.Forms.Clipboard.GetImage();
        }
    }
}
