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
        private KontenerCech cechy;
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
            

        }

        private void Button_Porownaj_Click(object sender, RoutedEventArgs e)
        {
            ServiceReference1.ServiceSAClient soap = new ServiceReference1.ServiceSAClient();
            //soap.przeslijPlik("https://github.com/adriik/InteligentnyDom/archive/master.zip");
            String NazwaProjekt1 = link1.Text.Split('/').Last();
            String NazwaProjekt2 = link2.Text.Split('/').Last();

            // 1 PROJEKT ---------------------------------------------------------------------------------------------------------------------------
            soap.przeslijPlik(link1.Text);
            LinkedList<ServiceReference1.hashePlikow> hashe1 = new LinkedList<ServiceReference1.hashePlikow>(soap.getSkrotyPlikow(NazwaProjekt1));
            //System.Diagnostics.Debug.WriteLine("Hash: " + hashe1.First().hash);
            int liczbaPlikow1 = soap.getLiczbaPlikow(NazwaProjekt1);
            // -------------------------------------------------------------------------------------------------------------------------------------


            // 2 PROJEKT ---------------------------------------------------------------------------------------------------------------------------
            soap.przeslijPlik(link2.Text);
            LinkedList<ServiceReference1.hashePlikow> hashe2 = new LinkedList<ServiceReference1.hashePlikow>(soap.getSkrotyPlikow(NazwaProjekt2));
            // -------------------------------------------------------------------------------------------------------------------------------------

            LinkedList<ServiceReference1.hashePlikow> wykrytePliki = new LinkedList<ServiceReference1.hashePlikow>();

            foreach (var item in hashe1)
            {
                foreach (var item2 in hashe2)
                {
                    if (item.hash.Equals(item2.hash))
                    {
                        wykrytePliki.AddLast(item);
                    }
                }
            }

            Console.WriteLine("Te same pliki: ");
            foreach (var item in wykrytePliki)
            {
                Console.WriteLine(item.nazwa);
            }

            Console.WriteLine("\n\nPodobieństwo: ");
            Console.WriteLine((float)wykrytePliki.Count / (float)liczbaPlikow1);
            
        }
    }
}
