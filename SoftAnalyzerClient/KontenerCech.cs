using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace SoftAnalyzerClient
{
    public class KontenerCech
    {
        public LinkedList<Cecha> listaCech;
        private Podmiot podmiot1;
        private Podmiot podmiot2;

        public KontenerCech()
        {
            listaCech = new LinkedList<Cecha>();
        }

        public void WyliczPodobienstwaCech(ref Podmiot podmiot1, ref Podmiot podmiot2)
        {
            this.podmiot1 = podmiot1;
            this.podmiot2 = podmiot2;
            SetLiczbaAtrybutowProperty();
            SetLiczbaMetodProperty();
            SetLiczbaAtrybutowWKlasach();
            SetZbiorBibliotekProperty();
            SetLiczbaPlikowProperty();
            SetJezykProgramowaniaProperty();
            SetLiczbaZnakowProperty();
            SetListaNazwPlikowProperty();
            SetListaNazwKatalogowProperty();
            SetLiczbaPlikowODanymRozszerzeniuProperty();
            SetLiczbaPlikowDanegoTypuProperty();
            SetMozliwoscWczytywaniaPlikowProperty();
            SetLiczbaDanychWejsciowychProperty();
            SetJezykInterfejsuProperty();
            SetLiczbaZmiennychDanegoTypuProperty();
            SetSkrotyPlikowProperty();
            SetParadygmatProperty();
            SetMechanizmWielowatkowosciPropertyy();
            SetLiczbaLiniiKoduProperty();
            SetRozmiaryPlikowKodowZrodlowychProperty();
        }

        public void DodajCeche(String nazwa, int waga)
        {
            listaCech.AddLast(new Cecha(nazwa, waga));
        }

        private Cecha GetCecha(String nazwa)
        {
            foreach (var item in listaCech)
            {
                if (item.nazwa.Equals(nazwa))
                {
                    return item;
                }
            }
            return null;
        }

        public int GetWaga(String nazwa)
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

        public void SetPodobienstwo(string nazwa, float wartosc)
        {
            foreach (var item in listaCech)
            {
                if (item.nazwa.Equals(nazwa))
                {
                    if (wartosc > 1.0f)
                    {
                        item.podobienstwo = wartosc - 1.0f;
                    }
                    else
                    {
                        item.podobienstwo = wartosc;
                    }
                    
                }
            }
        }

        public float GetPodobienstwo(string nazwa)
        {
            foreach (var item in listaCech)
            {
                if (item.nazwa.Equals(nazwa))
                {
                    return item.podobienstwo;
                }
            }
            return 0;
        }

        public void SetSzczegoloweDane(string nazwa, DataGrid dg)
        {
            foreach (var item in listaCech)
            {
                if (item.nazwa.Equals(nazwa))
                {
                    item.szczegoloweDane = dg;
                }
            }
        }

        private void SetLiczbaAtrybutowProperty()
        {
            //SetPodobienstwo("LiczbaAtrybutowProperty", (podmiot1.LiczbaAtrybutowProperty.Equals(podmiot2.LiczbaAtrybutowProperty)) ? 1.0f : 0.0f);
            SetPodobienstwo("LiczbaAtrybutowProperty", (float)Math.Min(podmiot1.LiczbaAtrybutowProperty, podmiot2.LiczbaAtrybutowProperty) / (float)Math.Max(podmiot1.LiczbaAtrybutowProperty, podmiot2.LiczbaAtrybutowProperty));
            DataGrid dg = new DataGrid();
            DataGridTextColumn col1 = new DataGridTextColumn();
            DataGridTextColumn col2 = new DataGridTextColumn();
            DataGridTextColumn col3 = new DataGridTextColumn();

            col1.Header = "";
            col1.Binding = new Binding("nazwa");
            dg.Columns.Add(col1);
            col2.Header = podmiot1.NazwaPodmiotu;
            col2.Binding = new Binding("wartosc1");
            dg.Columns.Add(col2);
            col3.Header = podmiot2.NazwaPodmiotu;
            col3.Binding = new Binding("wartosc2");
            dg.Columns.Add(col3);

            dg.Items.Add(new SzczegolowaTabela() { nazwa = "Liczba atrybutów", wartosc1 = podmiot1.LiczbaAtrybutowProperty.ToString(), wartosc2 = podmiot2.LiczbaAtrybutowProperty.ToString() });
            SetSzczegoloweDane("LiczbaAtrybutowProperty", dg);

        }

        private void SetLiczbaMetodProperty()
        {
            SetPodobienstwo("LiczbaMetodProperty", (podmiot1.LiczbaMetodProperty.Equals(podmiot2.LiczbaMetodProperty)) ? 1.0f : 0.0f);

            DataGrid dg = new DataGrid();
            DataGridTextColumn col1 = new DataGridTextColumn();
            DataGridTextColumn col2 = new DataGridTextColumn();
            DataGridTextColumn col3 = new DataGridTextColumn();

            col1.Header = "";
            col1.Binding = new Binding("nazwa");
            dg.Columns.Add(col1);
            col2.Header = podmiot1.NazwaPodmiotu;
            col2.Binding = new Binding("wartosc1");
            dg.Columns.Add(col2);
            col3.Header = podmiot2.NazwaPodmiotu;
            col3.Binding = new Binding("wartosc2");
            dg.Columns.Add(col3);

            dg.Items.Add(new SzczegolowaTabela() { nazwa = "Liczba Metod", wartosc1 = podmiot1.LiczbaMetodProperty.ToString(), wartosc2 = podmiot2.LiczbaMetodProperty.ToString() });
            SetSzczegoloweDane("LiczbaMetodProperty", dg);
        }
        
        private void SetLiczbaAtrybutowWKlasach()
        {
            List<SzczegolowaTabela> wykryte = new List<SzczegolowaTabela>();
            int liczbaWykryc = 0;
            foreach (var item in podmiot1.LiczbaAtrybutowWKlasach)
            {
                foreach (var item2 in podmiot2.LiczbaAtrybutowWKlasach)
                {
                    if (item.liczba.Equals(item2.liczba) && item.nazwa.Equals(item2.nazwa))
                    {
                        wykryte.Add(new SzczegolowaTabela() { wartosc1 = item.nazwa, wartosc2 = item.liczba.ToString()});
                        liczbaWykryc++;
                    }
                }
            }

            SetPodobienstwo("LiczbaAtrybutowWKlasach", (float)liczbaWykryc / (float)podmiot1.LiczbaAtrybutowWKlasach.Count);

            
            DataGrid dg = new DataGrid();
            DataGridTextColumn col1 = new DataGridTextColumn();
            DataGridTextColumn col2 = new DataGridTextColumn();


            col1.Header = "Wykryty plik";
            col1.Binding = new Binding("wartosc1");
            dg.Columns.Add(col1);
            col2.Header = "Liczba atrybutów";
            col2.Binding = new Binding("wartosc2");
            dg.Columns.Add(col2);

            dg.ItemsSource = new ObservableCollection<SzczegolowaTabela>(wykryte);
            SetSzczegoloweDane("LiczbaAtrybutowWKlasach", dg);
        }

        private void SetZbiorBibliotekProperty()
        {
            int liczbaWykryc = 0;
            foreach (var item in podmiot1.ZbiorBibliotekProperty)
            {
                foreach (var item2 in podmiot2.ZbiorBibliotekProperty)
                {
                    if (item.Equals(item2))
                    {
                        liczbaWykryc++;
                    }
                }
            }

            SetPodobienstwo("ZbiorBibliotekProperty", (float)liczbaWykryc / (float)podmiot1.ZbiorBibliotekProperty.Count);
        }

        private void SetLiczbaPlikowProperty()
        {
            SetPodobienstwo("LiczbaPlikowProperty", (podmiot1.LiczbaPlikowProperty.Equals(podmiot2.LiczbaPlikowProperty)) ? 1.0f : 0.0f);
        }

        private void SetLiczbaLiniiKoduProperty()
        {
            int liczbaWykryc = 0;
            foreach (var item in podmiot1.LiczbaLiniiKoduProperty)
            {
                foreach (var item2 in podmiot2.LiczbaLiniiKoduProperty)
                {
                    if (item.liczbaLinii.Equals(item2.liczbaLinii) && item.nazwa.Equals(item2.nazwa))
                    {
                        liczbaWykryc++;
                    }
                }
            }

            SetPodobienstwo("LiczbaLiniiKoduProperty", (float)liczbaWykryc / (float)podmiot1.LiczbaLiniiKoduProperty.Count);
        }

        private void SetRozmiaryPlikowKodowZrodlowychProperty()
        {
            int liczbaWykryc = 0;
            foreach (var item in podmiot1.RozmiaryPlikowKodowZrodlowychProperty)
            {
                foreach (var item2 in podmiot2.RozmiaryPlikowKodowZrodlowychProperty)
                {
                    if (item.rozmiar.Equals(item2.rozmiar) && item.nazwa.Equals(item2.nazwa))
                    {
                        liczbaWykryc++;
                    }
                }
            }

            SetPodobienstwo("RozmiaryPlikowKodowZrodlowychProperty", (float)liczbaWykryc / (float)podmiot1.RozmiaryPlikowKodowZrodlowychProperty.Count);
        }

        private void SetJezykProgramowaniaProperty()
        {
            SetPodobienstwo("JezykProgramowaniaProperty", podmiot1.JezykProgramowaniaProperty.Equals(podmiot2.JezykProgramowaniaProperty) ? 1.0f : 0.0f);
        }

        private void SetLiczbaZnakowProperty()
        {
            SetPodobienstwo("LiczbaZnakowProperty", (podmiot1.LiczbaZnakowProperty.Equals(podmiot2.LiczbaZnakowProperty)) ? 1.0f : 0.0f);
        }

        private void SetListaNazwPlikowProperty()
        {
            int liczbaWykryc = 0;
            foreach (var item in podmiot1.ListaNazwPlikow)
            {
                if (podmiot2.ListaNazwPlikow.Contains(item))
                {
                    podmiot2.ListaNazwPlikow.Remove(item);
                    liczbaWykryc++;
                }
            }
            SetPodobienstwo("ListaNazwPlikowProperty", (float)liczbaWykryc / (float)podmiot1.ListaNazwPlikow.Count);
        }

        private void SetListaNazwKatalogowProperty()
        {
            int liczbaWykryc = 0;
            foreach (var item in podmiot1.ListaNazwKatalogow)
            {
                if (podmiot2.ListaNazwKatalogow.Contains(item))
                {
                    podmiot2.ListaNazwKatalogow.Remove(item);
                    liczbaWykryc++;
                }
            }
            SetPodobienstwo("ListaNazwKatalogowProperty", (float)liczbaWykryc / (float)podmiot1.ListaNazwKatalogow.Count);
        }

        private void SetLiczbaPlikowODanymRozszerzeniuProperty()
        {
            //List<float> listaPodobienstw = new List<float>();
            int liczbaWykryc = 0;
            foreach (var item in podmiot1.LiczbaPlikowODanymRozszerzeniuProperty)
            {
                foreach (var item2 in podmiot2.LiczbaPlikowODanymRozszerzeniuProperty)
                {
                    if (item.rozszerzenie.Equals(item2.rozszerzenie) && item.liczba.Equals(item2.liczba))
                    {
                        //listaPodobienstw.Add(((float)item.liczba / (float)item2.liczba) %1);
                        liczbaWykryc++;
                    }
                }
            }
            
            SetPodobienstwo("LiczbaPlikowODanymRozszerzeniuProperty", liczbaWykryc / podmiot1.LiczbaPlikowODanymRozszerzeniuProperty.Count);
        }

        private void SetLiczbaPlikowDanegoTypuProperty()
        {
            //List<float> listaPodobienstw = new List<float>();
            int liczbaWykryc = 0;
            foreach (var item in podmiot1.LiczbaPlikowDanegoTypuProperty)
            {
                foreach (var item2 in podmiot2.LiczbaPlikowDanegoTypuProperty)
                {
                    if (item.typ.Equals(item2.typ) && item.liczba.Equals(item2.liczba))
                    {
                        //listaPodobienstw.Add((float)item.liczba / (float)item2.liczba);
                        liczbaWykryc++;
                    }
                }
            }

            SetPodobienstwo("LiczbaPlikowDanegoTypuProperty", (float)liczbaWykryc / podmiot1.LiczbaPlikowDanegoTypuProperty.Count);
        }

        private void SetMozliwoscWczytywaniaPlikowProperty()
        {
            SetPodobienstwo("MozliwoscWczytywaniaPlikowProperty", podmiot1.MozliwoscWczytywaniaPlikowProperty.Equals(podmiot2.MozliwoscWczytywaniaPlikowProperty) ? 1.0f : 0.0f);
        }

        private void SetLiczbaDanychWejsciowychProperty()
        {
            SetPodobienstwo("LiczbaDanychWejsciowychProperty", (podmiot1.LiczbaDanychWejsciowychProperty.Equals(podmiot2.LiczbaDanychWejsciowychProperty)) ? 1.0f : 0.0f);
        }

        private void SetJezykInterfejsuProperty()
        {
            SetPodobienstwo("JezykInterfejsuProperty", podmiot1.JezykInterfejsuProperty.Equals(podmiot2.JezykInterfejsuProperty) ? 1.0f : 0.0f);
        }

        private void SetLiczbaZmiennychDanegoTypuProperty()
        {
            //List<float> listaPodobienstw = new List<float>();
            int liczbaWykryc = 0;
            foreach (var item in podmiot1.LiczbaZmiennychDanegoTypuProperty)
            {
                foreach (var item2 in podmiot2.LiczbaZmiennychDanegoTypuProperty)
                {
                    if (item.typ.Equals(item2.typ) && item.liczba.Equals(item2.liczba))
                    {
                        //listaPodobienstw.Add((float)item.liczba / (float)item2.liczba);
                        liczbaWykryc++;
                    }
                }
            }

            SetPodobienstwo("LiczbaZmiennychDanegoTypuProperty", (float)liczbaWykryc / (float)podmiot1.LiczbaZmiennychDanegoTypuProperty.Count);
        }

        private void SetSkrotyPlikowProperty()
        {
            int liczbaWykryc = 0;
            foreach (var item in podmiot1.SkrotyPlikowProperty)
            {
                foreach (var item2 in podmiot2.SkrotyPlikowProperty)
                {
                    if (item.hash.Equals(item2.hash))
                    {
                        liczbaWykryc++;
                    }
                }
            }

            SetPodobienstwo("SkrotyPlikowProperty", (float)liczbaWykryc / (float)podmiot1.SkrotyPlikowProperty.Count);
        }

        private void SetParadygmatProperty()
        {
            SetPodobienstwo("ParadygmatProperty", podmiot1.ParadygmatProperty.Equals(podmiot2.ParadygmatProperty) ? 1.0f : 0.0f);
        }

        private void SetMechanizmWielowatkowosciPropertyy()
        {
            SetPodobienstwo("MechanizmWielowatkowosciProperty", podmiot1.MechanizmWielowatkowosciProperty.Equals(podmiot2.MechanizmWielowatkowosciProperty) ? 1.0f : 0.0f);
        }
    }
}
