using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartphones_producatori
{
    [Serializable]
    public class Producator
    {
        private int id;
        private string denumire;

        public Producator()
        {
            this.id = 0;
            this.denumire = string.Empty;
        }

        public Producator(int id, string denumire)
        {
            this.id = id;
            this.denumire = denumire;
        }

        public int Id { get => id; set => id = value; }
        public string Denumire { get => denumire; set => denumire = value; }
    }
}
