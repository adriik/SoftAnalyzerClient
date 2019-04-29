using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftAnalyzerClient
{
    class KontenerCech
    {
        private LinkedList<Cecha> listaCech;

        public KontenerCech()
        {
            listaCech = new LinkedList<Cecha>();
        }

        public void DodajCeche(String nazwa, int waga)
        {
            listaCech.AddLast(new Cecha(nazwa, waga));
        }

        public int getWaga(String nazwa)
        {
            foreach (var item in listaCech)
            {
                if (item.nazwa.Equals(nazwa))
                {
                    return item.waga;
                }
            }
            return 0;
        }

    }
}
