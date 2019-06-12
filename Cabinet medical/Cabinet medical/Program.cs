using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cabinet_medical
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            List<Medic> medici = new List<Medic>();
            Medic m1 = new Medic(1, "ortopedie", 2500);
            Medic m2 = new Medic(2, "ORL", 4500);
            Medic m3 = new Medic(3, "cardiologie", 6500);
            Medic m4 = new Medic(4, "imunologie", 1500);
            medici.Add(m1);
            medici.Add(m2);
            medici.Add(m3);
            medici.Add(m4);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(medici));
        }

            
    }
}
