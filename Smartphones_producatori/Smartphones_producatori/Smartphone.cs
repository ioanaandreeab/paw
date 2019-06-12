using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartphones_producatori
{
    [Serializable]
    public class Smartphone : IComparable<Smartphone>
    {
        private int id;
        private string model;
        private int stocDisp;
        private float pret;
        private string dataAparitie;
        private int idProducator;

        public Smartphone(int id, string model, int stocDisp, float pret, string dataAparitie, int idProducator)
        {
            this.id = id;
            this.model = model;
            this.stocDisp = stocDisp;
            this.pret = pret;
            this.dataAparitie = dataAparitie;
            this.idProducator = idProducator;
        }

        public int Id { get => id; set => id = value; }
        public string Model { get => model; set => model = value; }
        public int StocDisp { get => stocDisp; set => stocDisp = value; }
        public float Pret { get => pret; set => pret = value; }
        public string DataAparitie { get => dataAparitie; set => dataAparitie = value; }
        public int IdProducator { get => idProducator; set => idProducator = value; }

        public int CompareTo(Smartphone other)
        {
            if (this.pret < other.pret && this.stocDisp > other.stocDisp)
                return 1;
            else if (this.pret > other.pret && this.stocDisp < other.stocDisp)
                return -1;
            else return 0;
        }

        public override string ToString()
        {
            return string.Format("Id - {0}, model - {1}, stoc disponibil - {2}, pret - {3}, data aparitie - {4}, idProd - {5}.",
                this.id, this.model, this.stocDisp, this.pret, this.dataAparitie, this.idProducator);
        }

        public static explicit operator int(Smartphone s)
        {
            return s.StocDisp;
        }
    }
}
