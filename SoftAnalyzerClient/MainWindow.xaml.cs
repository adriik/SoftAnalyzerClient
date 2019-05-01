using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Xml;

namespace SoftAnalyzerClient
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static KontenerCech cechy;
        Podmiot podmiot1;
        Podmiot podmiot2;
        Szczegoly szczegoly;

        public MainWindow()
        {
            InitializeComponent();
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load("cechy.xml");
            XmlNodeList elemList = XmlDoc.GetElementsByTagName("cecha");
            cechy = new KontenerCech();
            foreach (XmlNode item in elemList)
            {
                cechy.DodajCeche(item.Attributes["nazwa"].Value, Int32.Parse(item.Attributes["waga"].Value), item.Attributes["typ"].Value);
            }

            link2.Text = "https://github.com/UniversalMediaServer/UniversalMediaServer/archive/clean_up_the_code.zip";
            link1.Text = "https://github.com/UniversalMediaServer/UniversalMediaServer/archive/8.0.0.zip";

        }

        private async void Button_Porownaj_Click(object sender, RoutedEventArgs e)
        {
            podmiot1 = null;
            podmiot2 = null;

            if(szczegoly != null)
            {
                szczegoly.Close();
            }

            SzczegolyButton.IsEnabled = false;
            SzczegolySpinner.Visibility = Visibility.Visible;
            SzczegolyTextBlock.Visibility = Visibility.Collapsed;
            SzczegolyInfo.Visibility = Visibility.Collapsed;

            PorownajSpinner.Visibility = Visibility.Visible;
            PorownajTextBlock.Visibility = Visibility.Collapsed;
            PorownajClone.Visibility = Visibility.Collapsed;
            PorownajButton.IsEnabled = false;
            await Task.Run(() => PobranieIPrzeliczenieCech());
            
        }

        private void PobranieIPrzeliczenieCech()
        {
            ServiceReference1.ServiceSAClient soap = new ServiceReference1.ServiceSAClient();

            string NazwaProjekt1;
            string NazwaProjekt2;
            string link1Text = "";
            string link2Text = "";

            System.Windows.Application.Current.Dispatcher.Invoke( DispatcherPriority.Normal, (ThreadStart)delegate {
                NazwaProjekt1 = link1.Text.Split('/').Last();
                NazwaProjekt2 = link2.Text.Split('/').Last();
                link1Text = link1.Text;
                link2Text = link2.Text;
            });
            podmiot1 = new Podmiot(soap, link1Text);
            podmiot2 = new Podmiot(soap, link2Text);

            cechy.WyliczPodobienstwaCech(ref podmiot1, ref podmiot2);

            float suma = 0.0f;

            foreach (var item in cechy.listaCech)
            {
                suma += item.podobienstwo * item.waga;
            }

            float podobienstwo = suma / cechy.listaCech.Sum(item => item.waga);

            Console.WriteLine("Podobieństwo projektów: " + podobienstwo);

            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                labelPodobienstwo.Content = "Podobieństwo systemów: " + Math.Round(podobienstwo, 2);
                PorownajButton.IsEnabled = true;
                SzczegolyButton.IsEnabled = true;

                SzczegolySpinner.Visibility = Visibility.Collapsed;
                SzczegolyTextBlock.Visibility = Visibility.Visible;
                SzczegolyInfo.Visibility = Visibility.Visible;

                PorownajSpinner.Visibility = Visibility.Collapsed;
                PorownajTextBlock.Visibility = Visibility.Visible;
                PorownajClone.Visibility = Visibility.Visible;
            });

            System.Media.SoundPlayer player = new System.Media.SoundPlayer("alert.wav");
            player.Play();
        }

        private void Button_Click_Szczegoly(object sender, RoutedEventArgs e)
        {
            szczegoly = new Szczegoly();
            szczegoly.Show();
        }
    }
}
