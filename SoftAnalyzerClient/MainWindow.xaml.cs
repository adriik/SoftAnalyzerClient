using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace SoftAnalyzerClient
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static KontenerCech cechy;
        public MainWindow()
        {
            InitializeComponent();
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load("cechy.xml");
            XmlNodeList elemList = XmlDoc.GetElementsByTagName("cecha");
            cechy = new KontenerCech();
            foreach (XmlNode item in elemList)
            {
                cechy.DodajCeche(item.Attributes["nazwa"].Value, Int32.Parse(item.Attributes["waga"].Value));
            }

            link2.Text = "https://github.com/UniversalMediaServer/UniversalMediaServer/archive/clean_up_the_code.zip";
            link1.Text = "https://github.com/UniversalMediaServer/UniversalMediaServer/archive/8.0.0.zip";
        }

        private void Button_Porownaj_Click(object sender, RoutedEventArgs e)
        {
            
            ServiceReference1.ServiceSAClient soap = new ServiceReference1.ServiceSAClient();
            //soap.przeslijPlik("https://github.com/adriik/InteligentnyDom/archive/master.zip");
            
            
            String NazwaProjekt1 = link1.Text.Split('/').Last();
            String NazwaProjekt2 = link2.Text.Split('/').Last();

            Podmiot podmiot1 = new Podmiot(soap, link1.Text);
            Podmiot podmiot2 = new Podmiot(soap, link2.Text);

            cechy.WyliczPodobienstwaCech(ref podmiot1, ref podmiot2);

            

            float suma = 0.0f;

            foreach (var item in cechy.listaCech)
            {
                suma += item.podobienstwo * item.waga;
            }

            float podobienstwo = suma / cechy.listaCech.Sum(item => item.waga);

            Console.WriteLine("Podobieństwo projektów: " + podobienstwo);

            labelPodobienstwo.Content = "Podobieństwo projektów: " + podobienstwo;
                // 1 PROJEKT ---------------------------------------------------------------------------------------------------------------------------
                //soap.przeslijPlik(link1.Text);
                //podmiot1.SkrotyPlikowProperty = new LinkedList<ServiceReference1.hashePlikow>(soap.getSkrotyPlikow(NazwaProjekt1));
                //podmiot1.LiczbaPlikowProperty = soap.getLiczbaPlikow(NazwaProjekt1);
                //podmiot1.LiczbaMetodProperty = soap.getLiczbaMetod(NazwaProjekt1);

                // -------------------------------------------------------------------------------------------------------------------------------------


                // 2 PROJEKT ---------------------------------------------------------------------------------------------------------------------------
                //soap.przeslijPlik(link2.Text);
                //podmiot2.SkrotyPlikowProperty = new LinkedList<ServiceReference1.hashePlikow>(soap.getSkrotyPlikow(NazwaProjekt2));
                //podmiot2.LiczbaPlikowProperty = soap.getLiczbaPlikow(NazwaProjekt2);
                // -------------------------------------------------------------------------------------------------------------------------------------

                //LinkedList<ServiceReference1.hashePlikow> wykrytePliki = new LinkedList<ServiceReference1.hashePlikow>();

                //foreach (var item in podmiot1.SkrotyPlikowProperty)
                //{
                //    foreach (var item2 in podmiot2.SkrotyPlikowProperty)
                //    {
                //        if (item.hash.Equals(item2.hash))
                //        {
                //            wykrytePliki.AddLast(item);
                //        }
                //    }
                //}

                //Console.WriteLine("Te same pliki: ");
                //foreach (var item in wykrytePliki)
                //{
                //    Console.WriteLine(item.nazwa);
                //}

                //Console.WriteLine("\n\nPodobieństwo: ");
                //cechy.SetPodobienstwo("SkrotyPlikowProperty", (float)wykrytePliki.Count / (float)podmiot1.LiczbaPlikowProperty);
                //Console.WriteLine((float)wykrytePliki.Count / (float)podmiot1.LiczbaPlikowProperty);
        }

        private void Button_Click_Szczegoly(object sender, RoutedEventArgs e)
        {
            Szczegoly szczegoly = new Szczegoly();
            szczegoly.Show();
        }
    }
}
