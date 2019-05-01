using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

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

        public void DodajCeche(String nazwa, int waga, string typ)
        {
            listaCech.AddLast(new Cecha(nazwa, waga, typ));
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
                    //if (wartosc > 1.0f)
                    //{
                    //    item.podobienstwo = wartosc - 1.0f;
                    //}
                    //else
                    //{
                    //    item.podobienstwo = wartosc;
                    //}
                    item.podobienstwo = wartosc;
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

            Tabela3x2("LiczbaAtrybutowProperty", "Liczba atrybutów", podmiot1.LiczbaAtrybutowProperty.ToString(), podmiot2.LiczbaAtrybutowProperty.ToString());

        }

        private void SetLiczbaMetodProperty()
        {
            //SetPodobienstwo("LiczbaMetodProperty", (podmiot1.LiczbaMetodProperty.Equals(podmiot2.LiczbaMetodProperty)) ? 1.0f : 0.0f);
            SetPodobienstwo("LiczbaMetodProperty", (float)Math.Min(podmiot1.LiczbaMetodProperty, podmiot2.LiczbaMetodProperty) / (float)Math.Max(podmiot1.LiczbaMetodProperty, podmiot2.LiczbaMetodProperty));

            Tabela3x2("LiczbaMetodProperty", "Liczba Metod", podmiot1.LiczbaMetodProperty.ToString(), podmiot2.LiczbaMetodProperty.ToString());
        }
        
        private void SetLiczbaAtrybutowWKlasach()
        {
            List<SzczegolowaTabela> wykryte = new List<SzczegolowaTabela>();
            float liczbaWykryc = 0.0f;
            foreach (var item in podmiot1.LiczbaAtrybutowWKlasach)
            {
                foreach (var item2 in podmiot2.LiczbaAtrybutowWKlasach)
                {
                    if (item.nazwa.Equals(item2.nazwa))
                    {
                        wykryte.Add(new SzczegolowaTabela() { nazwa = item.nazwa, wartosc1 = item.liczba.ToString(), wartosc2 = item2.liczba.ToString()});

                        if (item.liczba != 0 && item2.liczba != 0)
                        {
                            liczbaWykryc += Math.Min(item.liczba, item2.liczba) / Math.Max(item.liczba, item2.liczba);
                        }
                        else if(item.liczba == 0 && item2.liczba == 0)
                        {
                            liczbaWykryc += 1.0f;
                        }
                    }
                }
            }

            //SetPodobienstwo("LiczbaAtrybutowWKlasach", (float)liczbaWykryc / (float)podmiot1.LiczbaAtrybutowWKlasach.Count);
            //SetPodobienstwo("LiczbaAtrybutowWKlasach", (float)liczbaWykryc / (float)(podmiot1.LiczbaAtrybutowWKlasach.Count + podmiot2.LiczbaAtrybutowWKlasach.Count));
            SetPodobienstwo("LiczbaAtrybutowWKlasach", liczbaWykryc / (float)wykryte.Count);

            //Tabela2xM("LiczbaAtrybutowWKlasach", "Wykryty plik", "Liczba atrybutów", wykryte);
            Tabela3xM("LiczbaAtrybutowWKlasach","Nazwa pliku",wykryte);
        }

        private void SetZbiorBibliotekProperty()
        {
            int liczbaWykryc = 0;
            List<SzczegolowaTabela> wykryte = new List<SzczegolowaTabela>();
            foreach (var item in podmiot1.ZbiorBibliotekProperty)
            {
                foreach (var item2 in podmiot2.ZbiorBibliotekProperty)
                {
                    if (item.Equals(item2))
                    {
                        liczbaWykryc++;
                        wykryte.Add(new SzczegolowaTabela() { wartosc1 = item });
                    }
                }
            }

            //SetPodobienstwo("ZbiorBibliotekProperty", (float)liczbaWykryc / (float)podmiot1.ZbiorBibliotekProperty.Count);
            SetPodobienstwo("ZbiorBibliotekProperty", (float)liczbaWykryc / (float)(podmiot1.ZbiorBibliotekProperty.Count + podmiot2.ZbiorBibliotekProperty.Count));

            Tabela1xM("ZbiorBibliotekProperty", "Wykryte biblioteki", wykryte);
        }

        private void SetLiczbaPlikowProperty()
        {
            //SetPodobienstwo("LiczbaPlikowProperty", (podmiot1.LiczbaPlikowProperty.Equals(podmiot2.LiczbaPlikowProperty)) ? 1.0f : 0.0f);
            SetPodobienstwo("LiczbaPlikowProperty", (float)Math.Min(podmiot1.LiczbaPlikowProperty, podmiot2.LiczbaPlikowProperty) / (float)Math.Max(podmiot1.LiczbaPlikowProperty, podmiot2.LiczbaPlikowProperty));

            Tabela3x2("LiczbaPlikowProperty", "Liczba plików", podmiot1.LiczbaAtrybutowProperty.ToString(), podmiot2.LiczbaAtrybutowProperty.ToString());
        }

        private void SetLiczbaLiniiKoduProperty()
        {
            float liczbaWykryc = 0.0f;
            List<SzczegolowaTabela> wykryte = new List<SzczegolowaTabela>();
            foreach (var item in podmiot1.LiczbaLiniiKoduProperty)
            {
                foreach (var item2 in podmiot2.LiczbaLiniiKoduProperty)
                {
                    if (item.nazwa.Equals(item2.nazwa))
                    {
                        wykryte.Add(new SzczegolowaTabela() { nazwa = item.nazwa, wartosc1 = item.liczbaLinii.ToString(), wartosc2 = item2.liczbaLinii.ToString() });

                        if (item.liczbaLinii != 0 && item2.liczbaLinii != 0)
                        {
                            liczbaWykryc += (float)Math.Min(item.liczbaLinii, item2.liczbaLinii) / (float)Math.Max(item.liczbaLinii, item2.liczbaLinii);
                        }
                        else if (item.liczbaLinii == 0 && item2.liczbaLinii == 0)
                        {
                            liczbaWykryc += 1.0f;
                        }
                    }
                }
            }

            //SetPodobienstwo("LiczbaLiniiKoduProperty", (float)liczbaWykryc / (float)podmiot1.LiczbaLiniiKoduProperty.Count);
            //SetPodobienstwo("LiczbaLiniiKoduProperty", (float)liczbaWykryc / (float)(podmiot1.LiczbaLiniiKoduProperty.Count + podmiot2.LiczbaLiniiKoduProperty.Count));
            SetPodobienstwo("LiczbaLiniiKoduProperty", liczbaWykryc / (float)(wykryte.Count));
            
            //Tabela2xM("LiczbaLiniiKoduProperty", "Nazwa pliku", "Liczba linii kodu", wykryte);
            Tabela3xM("LiczbaLiniiKoduProperty","Nazwa Pliku",wykryte);
        }

        private void SetRozmiaryPlikowKodowZrodlowychProperty()
        {
            float liczbaWykryc = 0.0f;
            List<SzczegolowaTabela> wykryte = new List<SzczegolowaTabela>();
            foreach (var item in podmiot1.RozmiaryPlikowKodowZrodlowychProperty)
            {
                foreach (var item2 in podmiot2.RozmiaryPlikowKodowZrodlowychProperty)
                {
                    if (item.nazwa.Equals(item2.nazwa))
                    {
                        
                        wykryte.Add(new SzczegolowaTabela() { nazwa = item.nazwa, wartosc1 = item.rozmiar.ToString(), wartosc2 = item2.rozmiar.ToString() });

                        if (item.rozmiar != 0 && item2.rozmiar != 0)
                        {
                            liczbaWykryc += (float)Math.Min(item.rozmiar, item2.rozmiar) / (float)Math.Max(item.rozmiar, item2.rozmiar);
                        }
                        else if (item.rozmiar == 0 && item2.rozmiar == 0)
                        {
                            liczbaWykryc += 1.0f;
                        }
                    }
                }
            }
            Console.WriteLine("liczba wykryć: " + liczbaWykryc + " wykryte: " + wykryte.Count);
            //SetPodobienstwo("RozmiaryPlikowKodowZrodlowychProperty", (float)liczbaWykryc / (float)podmiot1.RozmiaryPlikowKodowZrodlowychProperty.Count);
            //SetPodobienstwo("RozmiaryPlikowKodowZrodlowychProperty", (float)liczbaWykryc / (float)(podmiot1.RozmiaryPlikowKodowZrodlowychProperty.Count + podmiot2.RozmiaryPlikowKodowZrodlowychProperty.Count));
            SetPodobienstwo("RozmiaryPlikowKodowZrodlowychProperty", liczbaWykryc / (float)wykryte.Count);

            //Tabela2xM("RozmiaryPlikowKodowZrodlowychProperty","Nazwa pliku", "Rozmiar pliku", wykryte);
            Tabela3xM("RozmiaryPlikowKodowZrodlowychProperty", "Nazwa Pliku", wykryte);
        }

        private void SetJezykProgramowaniaProperty()
        {
            SetPodobienstwo("JezykProgramowaniaProperty", podmiot1.JezykProgramowaniaProperty.Equals(podmiot2.JezykProgramowaniaProperty) ? 1.0f : 0.0f);
            Tabela3x2("JezykProgramowaniaProperty","Język programowania", podmiot1.JezykProgramowaniaProperty, podmiot2.JezykProgramowaniaProperty);
        }

        private void SetLiczbaZnakowProperty()
        {
            //SetPodobienstwo("LiczbaZnakowProperty", podmiot1.LiczbaZnakowProperty.Equals(podmiot2.LiczbaZnakowProperty) ? 1.0f : 0.0f);
            SetPodobienstwo("LiczbaZnakowProperty", (float)Math.Min(podmiot1.LiczbaZnakowProperty, podmiot2.LiczbaZnakowProperty) / (float)Math.Max(podmiot1.LiczbaZnakowProperty, podmiot2.LiczbaZnakowProperty));
            Tabela3x2("LiczbaZnakowProperty","Liczba znaków", podmiot1.LiczbaZnakowProperty.ToString(), podmiot2.LiczbaZnakowProperty.ToString());
        }

        private void SetListaNazwPlikowProperty()
        {
            int liczbaWykryc = 0;
            List<SzczegolowaTabela> wykryte = new List<SzczegolowaTabela>();
            foreach (var item in podmiot1.ListaNazwPlikow)
            {
                if (podmiot2.ListaNazwPlikow.Contains(item))
                {
                    podmiot2.ListaNazwPlikow.Remove(item);
                    liczbaWykryc++;
                    wykryte.Add(new SzczegolowaTabela() { wartosc1 = item });
                }
            }
            //SetPodobienstwo("ListaNazwPlikowProperty", (float)liczbaWykryc / (float)podmiot1.ListaNazwPlikow.Count);
            SetPodobienstwo("ListaNazwPlikowProperty", (float)liczbaWykryc / (float)(podmiot1.ListaNazwPlikow.Count + podmiot2.ListaNazwPlikow.Count));
            Tabela1xM("ListaNazwPlikowProperty","Wykryte pliki", wykryte);
        }

        private void SetListaNazwKatalogowProperty()
        {
            int liczbaWykryc = 0;
            List<SzczegolowaTabela> wykryte = new List<SzczegolowaTabela>();
            foreach (var item in podmiot1.ListaNazwKatalogow)
            {
                if (podmiot2.ListaNazwKatalogow.Contains(item))
                {
                    podmiot2.ListaNazwKatalogow.Remove(item);
                    liczbaWykryc++;
                    wykryte.Add(new SzczegolowaTabela() { wartosc1 = item });
                }
            }
            //SetPodobienstwo("ListaNazwKatalogowProperty", (float)liczbaWykryc / (float)podmiot1.ListaNazwKatalogow.Count);
            SetPodobienstwo("ListaNazwKatalogowProperty", (float)liczbaWykryc / (float)(podmiot1.ListaNazwKatalogow.Count + podmiot2.ListaNazwKatalogow.Count));
            Tabela1xM("ListaNazwKatalogowProperty", "Wykryte katalogi", wykryte);
        }

        private void SetLiczbaPlikowODanymRozszerzeniuProperty()
        {
            List<SzczegolowaTabela> wykryte = new List<SzczegolowaTabela>();
            int liczbaWykryc = 0;
            foreach (var item in podmiot1.LiczbaPlikowODanymRozszerzeniuProperty)
            {
                foreach (var item2 in podmiot2.LiczbaPlikowODanymRozszerzeniuProperty)
                {
                    if (item.rozszerzenie.Equals(item2.rozszerzenie) && item.liczba.Equals(item2.liczba))
                    {
                        liczbaWykryc++;
                        wykryte.Add(new SzczegolowaTabela() { wartosc1 = item.rozszerzenie, wartosc2 = item.liczba.ToString() });
                    }
                }
            }

            //SetPodobienstwo("LiczbaPlikowODanymRozszerzeniuProperty", liczbaWykryc / podmiot1.LiczbaPlikowODanymRozszerzeniuProperty.Count);
            SetPodobienstwo("LiczbaPlikowODanymRozszerzeniuProperty", (float)liczbaWykryc / (float)(podmiot1.LiczbaPlikowODanymRozszerzeniuProperty.Count + podmiot2.LiczbaPlikowODanymRozszerzeniuProperty.Count));
            Tabela2xM("LiczbaPlikowODanymRozszerzeniuProperty","Rozszerzenie pliku","Liczba plików",wykryte);
        }

        private void SetLiczbaPlikowDanegoTypuProperty()
        {
            List<SzczegolowaTabela> wykryte = new List<SzczegolowaTabela>();
            float liczbaWykryc = 0.0f;
            foreach (var item in podmiot1.LiczbaPlikowDanegoTypuProperty)
            {
                foreach (var item2 in podmiot2.LiczbaPlikowDanegoTypuProperty)
                {
                    if (item.typ.Equals(item2.typ))
                    {
                        wykryte.Add(new SzczegolowaTabela() { nazwa = item.typ, wartosc1 = item.liczba.ToString(), wartosc2 = item2.liczba.ToString() });

                        if (item.liczba != 0 && item2.liczba != 0)
                        {
                            liczbaWykryc += (float)Math.Min(item.liczba, item2.liczba) / (float)Math.Max(item.liczba, item2.liczba);
                        }
                        else if (item.liczba == 0 && item2.liczba == 0)
                        {
                            liczbaWykryc += 1.0f;
                        }
                    }
                }
            }

            //SetPodobienstwo("LiczbaPlikowDanegoTypuProperty", (float)liczbaWykryc / podmiot1.LiczbaPlikowDanegoTypuProperty.Count);
            //SetPodobienstwo("LiczbaPlikowDanegoTypuProperty", (float)liczbaWykryc / (float)(podmiot1.LiczbaPlikowDanegoTypuProperty.Count + podmiot2.LiczbaPlikowDanegoTypuProperty.Count));
            SetPodobienstwo("LiczbaPlikowDanegoTypuProperty", liczbaWykryc / (float)podmiot1.LiczbaPlikowDanegoTypuProperty.Count);
            //Tabela2xM("LiczbaPlikowDanegoTypuProperty", "Typ pliku", "Liczba plików", wykryte);
            Tabela3xM("LiczbaPlikowDanegoTypuProperty","Typ pliku", wykryte);
        }

        private void SetMozliwoscWczytywaniaPlikowProperty()
        {
            SetPodobienstwo("MozliwoscWczytywaniaPlikowProperty", podmiot1.MozliwoscWczytywaniaPlikowProperty.Equals(podmiot2.MozliwoscWczytywaniaPlikowProperty) ? 1.0f : 0.0f);
            Tabela3x2("MozliwoscWczytywaniaPlikowProperty","Czy system wczytuje pliki", podmiot1.MozliwoscWczytywaniaPlikowProperty ? "TAK" : "NIE", podmiot2.MozliwoscWczytywaniaPlikowProperty ? "TAK" : "NIE");
        }

        private void SetLiczbaDanychWejsciowychProperty()
        {
            //SetPodobienstwo("LiczbaDanychWejsciowychProperty", (podmiot1.LiczbaDanychWejsciowychProperty.Equals(podmiot2.LiczbaDanychWejsciowychProperty)) ? 1.0f : 0.0f);
            SetPodobienstwo("LiczbaDanychWejsciowychProperty", (float)Math.Min(podmiot1.LiczbaDanychWejsciowychProperty, podmiot2.LiczbaDanychWejsciowychProperty) / (float)Math.Max(podmiot1.LiczbaDanychWejsciowychProperty, podmiot2.LiczbaDanychWejsciowychProperty));
            Tabela3x2("LiczbaDanychWejsciowychProperty","Liczba użytych mechanizmów wczytujących dane", podmiot1.LiczbaDanychWejsciowychProperty.ToString(), podmiot2.LiczbaDanychWejsciowychProperty.ToString());
        }

        private void SetJezykInterfejsuProperty()
        {
            SetPodobienstwo("JezykInterfejsuProperty", podmiot1.JezykInterfejsuProperty.Equals(podmiot2.JezykInterfejsuProperty) ? 1.0f : 0.0f);
            Tabela3x2("JezykInterfejsuProperty","Lokalizacja systemu (język)", podmiot1.JezykInterfejsuProperty, podmiot2.JezykInterfejsuProperty);
        }

        private void SetLiczbaZmiennychDanegoTypuProperty()
        {
            List<SzczegolowaTabela> wykryte = new List<SzczegolowaTabela>();
            float liczbaWykryc = 0.0f;
            foreach (var item in podmiot1.LiczbaZmiennychDanegoTypuProperty)
            {
                foreach (var item2 in podmiot2.LiczbaZmiennychDanegoTypuProperty)
                {
                    if (item.typ.Equals(item2.typ))
                    {
                        wykryte.Add(new SzczegolowaTabela() { nazwa = item.typ, wartosc1 = item.liczba.ToString(), wartosc2 = item2.liczba.ToString() });

                        if (item.liczba != 0 && item2.liczba != 0)
                        {
                            liczbaWykryc += (float)Math.Min(item.liczba, item2.liczba) / (float)Math.Max(item.liczba, item2.liczba);
                        }
                        else if (item.liczba == 0 && item2.liczba == 0)
                        {
                            liczbaWykryc += 1.0f;
                        }
                    }
                }
            }

            //SetPodobienstwo("LiczbaZmiennychDanegoTypuProperty", (float)liczbaWykryc / (float)podmiot1.LiczbaZmiennychDanegoTypuProperty.Count);
            //SetPodobienstwo("LiczbaZmiennychDanegoTypuProperty", (float)liczbaWykryc / (float)(podmiot1.LiczbaZmiennychDanegoTypuProperty.Count + podmiot2.LiczbaZmiennychDanegoTypuProperty.Count));
            SetPodobienstwo("LiczbaZmiennychDanegoTypuProperty", (float)liczbaWykryc / (float)wykryte.Count);
            //Tabela2xM("LiczbaZmiennychDanegoTypuProperty","Typ zmiennych", "Liczba zmiennych", wykryte);
            Tabela3xM("LiczbaZmiennychDanegoTypuProperty","Typ zmiennej",wykryte);
        }

        private void SetSkrotyPlikowProperty()
        {
            int liczbaWykryc = 0;
            List<SzczegolowaTabela> wykryte = new List<SzczegolowaTabela>();
            foreach (var item in podmiot1.SkrotyPlikowProperty)
            {
                foreach (var item2 in podmiot2.SkrotyPlikowProperty)
                {
                    if (item.hash.Equals(item2.hash))
                    {
                        liczbaWykryc++;
                        wykryte.Add(new SzczegolowaTabela() { wartosc1 = item.nazwa });
                    }
                }
            }

            //SetPodobienstwo("SkrotyPlikowProperty", (float)liczbaWykryc / (float)podmiot1.SkrotyPlikowProperty.Count);
            SetPodobienstwo("SkrotyPlikowProperty", (float)liczbaWykryc / (float)(podmiot1.SkrotyPlikowProperty.Count + podmiot2.SkrotyPlikowProperty.Count));
            Tabela1xM("SkrotyPlikowProperty","Identyczne pliki", wykryte);
        }

        private void SetParadygmatProperty()
        {
            SetPodobienstwo("ParadygmatProperty", podmiot1.ParadygmatProperty.Equals(podmiot2.ParadygmatProperty) ? 1.0f : 0.0f);
            Tabela3x2("ParadygmatProperty","Paradygmat", podmiot1.ParadygmatProperty, podmiot2.ParadygmatProperty);
        }

        private void SetMechanizmWielowatkowosciPropertyy()
        {
            SetPodobienstwo("MechanizmWielowatkowosciProperty", podmiot1.MechanizmWielowatkowosciProperty.Equals(podmiot2.MechanizmWielowatkowosciProperty) ? 1.0f : 0.0f);
            Tabela3x2("MechanizmWielowatkowosciProperty","Czy występuje wielowątkowość", podmiot1.MechanizmWielowatkowosciProperty ? "TAK" : "NIE", podmiot2.MechanizmWielowatkowosciProperty ? "TAK" : "NIE");
        }

        private void Tabela3x2(string nazwaProperty, string etykieta, string wartosc1, string wartosc2)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
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

                dg.Items.Add(new SzczegolowaTabela() { nazwa = etykieta, wartosc1 = wartosc1, wartosc2 = wartosc2 });
                SetSzczegoloweDane(nazwaProperty, dg);
            });
        }

        private void Tabela3xM(string nazwaProperty, string etykieta, List<SzczegolowaTabela> data)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                DataGrid dg = new DataGrid();
                DataGridTextColumn col1 = new DataGridTextColumn();
                DataGridTextColumn col2 = new DataGridTextColumn();
                DataGridTextColumn col3 = new DataGridTextColumn();

                col1.Header = etykieta;
                col1.Binding = new Binding("nazwa");
                dg.Columns.Add(col1);
                col2.Header = podmiot1.NazwaPodmiotu;
                col2.Binding = new Binding("wartosc1");
                dg.Columns.Add(col2);
                col3.Header = podmiot2.NazwaPodmiotu;
                col3.Binding = new Binding("wartosc2");
                dg.Columns.Add(col3);

                dg.ItemsSource = new ObservableCollection<SzczegolowaTabela>(data);
                SetSzczegoloweDane(nazwaProperty, dg);
            });
        }

        private void Tabela2xM(string nazwaProperty, string etykieta1, string etykieta2, List<SzczegolowaTabela> data)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                DataGrid dg = new DataGrid();
                DataGridTextColumn col1 = new DataGridTextColumn();
                DataGridTextColumn col2 = new DataGridTextColumn();


                col1.Header = etykieta1;
                col1.Binding = new Binding("wartosc1");
                dg.Columns.Add(col1);
                col2.Header = etykieta2;
                col2.Binding = new Binding("wartosc2");
                dg.Columns.Add(col2);

                dg.ItemsSource = new ObservableCollection<SzczegolowaTabela>(data);
                SetSzczegoloweDane(nazwaProperty, dg);
            });
        }

        private void Tabela1xM(string nazwaProperty, string etykieta1, List<SzczegolowaTabela> data)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                DataGrid dg = new DataGrid();
                DataGridTextColumn col1 = new DataGridTextColumn();

                col1.Header = etykieta1;
                col1.Binding = new Binding("wartosc1");
                dg.Columns.Add(col1);

                dg.ItemsSource = new ObservableCollection<SzczegolowaTabela>(data);
                SetSzczegoloweDane(nazwaProperty, dg);
            });
        }
    }
}
