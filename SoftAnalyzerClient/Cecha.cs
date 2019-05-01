using System.Windows.Controls;

namespace SoftAnalyzerClient
{
    public class Cecha
    {
        public string nazwa { get; set; }
        public int waga { get; set; }
        public float podobienstwo { get; set; }
        public DataGrid szczegoloweDane { get; set; }

        public string typ { get; set; }

        public Cecha(string nazwa, int waga, string typ)
        {
            this.nazwa = nazwa;
            this.waga = waga;
            this.typ = typ;
        }
    }
}