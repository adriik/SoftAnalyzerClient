using System.Windows.Controls;

namespace SoftAnalyzerClient
{
    public class Cecha
    {
        public string identyfikator { get; set; }
        public int waga { get; set; }
        public float podobienstwo { get; set; }
        public DataGrid szczegoloweDane { get; set; }
        public string nazwa { get; set; }

        public string typ { get; set; }

        public Cecha(string identyfikator, int waga, string typ, string nazwa)
        {
            this.identyfikator = identyfikator;
            this.waga = waga;
            this.typ = typ;
            this.nazwa = nazwa;
        }
    }
}