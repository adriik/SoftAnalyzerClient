using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftAnalyzerClient
{
    class Podmiot
    {
        public int LiczbaAtrybutowProperty;
        public int LiczbaMetodProperty;
        public LinkedList<ServiceReference1.atrybutyPlikow> LiczbaAtrybutowWKlasach;
        public List<string> ZbiorBibliotekProperty;
        public int LiczbaPlikowProperty;
        public int LiczbaLiniiKoduProperty;
        public LinkedList<ServiceReference1.rozmiaryPlikow> RozmiaryPlikowKodowZrodlowychProperty;
        public string JezykProgramowaniaProperty;
        public int LiczbaZnakowProperty;
        public List<string> ListaNazwPlikowIKatalogow;
        public LinkedList<ServiceReference1.rozszerzeniaPlikow> LiczbaPlikowODanymRozszerzeniuProperty;
        public LinkedList<ServiceReference1.typyPlikow> LiczbaPlikowDanegoTypuProperty;
        public bool MozliwoscWczytywaniaPlikowProperty;
        public int LiczbaDanychWejsciowychProperty;
        public string JezykInterfejsuProperty;
        public LinkedList<ServiceReference1.typyZmiennych> LiczbaZmiennychDanegoTypuProperty;
        public LinkedList<ServiceReference1.hashePlikow> SkrotyPlikowProperty;
        public string ParadygmatProperty;
        public bool MechanizmWielowatkowosciProperty;
    }
}
