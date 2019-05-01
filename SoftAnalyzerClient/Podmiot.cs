using System.Collections.Generic;
using System.Linq;

namespace SoftAnalyzerClient
{
    public class Podmiot
    {
        public string NazwaPodmiotu { get; set; }
        public int LiczbaAtrybutowProperty { get; set; }
        public int LiczbaMetodProperty { get; set; }
        public LinkedList<ServiceReference1.atrybutyPlikow> LiczbaAtrybutowWKlasach { get; set; }
        public List<string> ZbiorBibliotekProperty { get; set; }
        public int LiczbaPlikowProperty { get; set; }
        public LinkedList<ServiceReference1.liczbaLiniiKodu> LiczbaLiniiKoduProperty { get; set; }
        public LinkedList<ServiceReference1.rozmiaryPlikow> RozmiaryPlikowKodowZrodlowychProperty { get; set; }
        public string JezykProgramowaniaProperty { get; set; }
        public int LiczbaZnakowProperty { get; set; }
        public List<string> ListaNazwPlikow { get; set; }
        public List<string> ListaNazwKatalogow { get; set; }
        public LinkedList<ServiceReference1.rozszerzeniaPlikow> LiczbaPlikowODanymRozszerzeniuProperty { get; set; }
        public LinkedList<ServiceReference1.typyPlikow> LiczbaPlikowDanegoTypuProperty { get; set; }
        public bool MozliwoscWczytywaniaPlikowProperty { get; set; }
        public int LiczbaDanychWejsciowychProperty { get; set; }
        public string JezykInterfejsuProperty { get; set; }
        public LinkedList<ServiceReference1.typyZmiennych> LiczbaZmiennychDanegoTypuProperty { get; set; }
        public LinkedList<ServiceReference1.hashePlikow> SkrotyPlikowProperty { get; set; }
        public string ParadygmatProperty { get; set; }
        public bool MechanizmWielowatkowosciProperty { get; set; }

        public Podmiot(ServiceReference1.ServiceSAClient soap, string link)
        {
            this.NazwaPodmiotu = link.Split('/').Last();
            soap.przeslijPlik(link);
            this.SkrotyPlikowProperty = new LinkedList<ServiceReference1.hashePlikow>(soap.getSkrotyPlikow(NazwaPodmiotu));
            this.LiczbaPlikowProperty = soap.getLiczbaPlikow(NazwaPodmiotu);
            this.LiczbaMetodProperty = soap.getLiczbaMetod(NazwaPodmiotu);
            this.LiczbaAtrybutowProperty = soap.getLiczbaAtrybutow(NazwaPodmiotu);
            this.LiczbaAtrybutowWKlasach = new LinkedList<ServiceReference1.atrybutyPlikow>(soap.getLiczbaAtrybutowWKlasach(NazwaPodmiotu));
            this.ZbiorBibliotekProperty = new List<string>(soap.getZbiorBibliotek(NazwaPodmiotu));
            this.LiczbaLiniiKoduProperty = new LinkedList<ServiceReference1.liczbaLiniiKodu>(soap.getLiczbaLiniiKodu(NazwaPodmiotu));
            this.RozmiaryPlikowKodowZrodlowychProperty = new LinkedList<ServiceReference1.rozmiaryPlikow>(soap.getRozmiaryPlikowKodow(NazwaPodmiotu));
            this.JezykProgramowaniaProperty = soap.getJezykProgramowania(NazwaPodmiotu);
            this.LiczbaZnakowProperty = soap.getLiczbaZnakow(NazwaPodmiotu);
            this.ListaNazwPlikow = new List<string>(soap.getListaNazwPlikow(NazwaPodmiotu));
            this.ListaNazwKatalogow = new List<string>(soap.getListaNazwKatalogow(NazwaPodmiotu));
            this.LiczbaPlikowODanymRozszerzeniuProperty = new LinkedList<ServiceReference1.rozszerzeniaPlikow>(soap.getLiczbaPlikowDanegoRozszerzenia(NazwaPodmiotu));
            this.LiczbaPlikowDanegoTypuProperty = new LinkedList<ServiceReference1.typyPlikow>(soap.getLiczbaPlikowDanegoTypu(NazwaPodmiotu));
            this.MozliwoscWczytywaniaPlikowProperty = soap.getMozliwosciWczytywaniaPlikow(NazwaPodmiotu);
            this.LiczbaDanychWejsciowychProperty = soap.getLiczbaDanychWejsciowych(NazwaPodmiotu);
            this.JezykInterfejsuProperty = soap.getJezykInterfejsu(NazwaPodmiotu);
            this.LiczbaZmiennychDanegoTypuProperty = new LinkedList<ServiceReference1.typyZmiennych>(soap.getLiczbaZmiennychDanegoTypu(NazwaPodmiotu));
            this.ParadygmatProperty = soap.getParadygmat(NazwaPodmiotu);
            this.MechanizmWielowatkowosciProperty = soap.getWykorzystanieWielowatkowosci(NazwaPodmiotu);
        }
    }
}
