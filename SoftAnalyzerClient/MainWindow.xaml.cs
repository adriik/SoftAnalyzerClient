using System;
using System.Collections.Generic;
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
        
        private KontenerCech cechy;
        Podmiot podmiot1;
        Podmiot podmiot2;
        Szczegoly szczegoly;
        public static List<KontenerCech> listaBadan;
        public static float sumaOgolna = 0.0f;
        public static float sumaKodu = 0.0f;
        public static float sumaStruktury = 0.0f;

        public MainWindow()
        {
            InitializeComponent();
            listaBadan = new List<KontenerCech>();
            
            this.Closing += MainWindow_Closing;

            link2.Text = "https://github.com/UniversalMediaServer/UniversalMediaServer/archive/clean_up_the_code.zip";
            link1.Text = "https://github.com/UniversalMediaServer/UniversalMediaServer/archive/8.0.0.zip";

        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PobierzUstawieniaXML()
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load("cechy.xml");
            XmlNodeList elemList = XmlDoc.GetElementsByTagName("cecha");



            foreach (XmlNode item in elemList)
            {
                cechy.DodajCeche(item.Attributes["identyfikator"].Value, Int32.Parse(item.Attributes["waga"].Value), item.Attributes["typ"].Value, item.Attributes["nazwa"].Value);
            }
        }

        private async void Button_Porownaj_Click(object sender, RoutedEventArgs e)
        {
            podmiot1 = null;
            podmiot2 = null;

            if(szczegoly != null)
            {
                szczegoly.Close();
            }

            bool result = Uri.TryCreate(link1.Text, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            bool result2 = Uri.TryCreate(link2.Text, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            if (result && result2)
            {
                link1.BorderBrush = System.Windows.Media.Brushes.Green;
                link2.BorderBrush = System.Windows.Media.Brushes.Green;

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
            else
            {
                if (!result)
                {
                    link1.BorderBrush = System.Windows.Media.Brushes.Red;
                }
                else
                {
                    link1.BorderBrush = System.Windows.Media.Brushes.Green;
                }

                if (!result2)
                {
                    link2.BorderBrush = System.Windows.Media.Brushes.Red;
                }
                else
                {
                    link2.BorderBrush = System.Windows.Media.Brushes.Green;
                }
            }
        }

        private void PobranieIPrzeliczenieCech()
        {
            


            ServiceReference1.ServiceSAClient soap = new ServiceReference1.ServiceSAClient();

            string NazwaProjekt1 = "";
            string NazwaProjekt2 = "";
            string link1Text = "";
            string link2Text = "";

            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {

                NazwaProjekt1 = link1.Text.Split('/').Last();
                NazwaProjekt2 = link2.Text.Split('/').Last();
                link1Text = link1.Text;
                link2Text = link2.Text;
            });

            try
            {
                
                cechy = new KontenerCech();
                PobierzUstawieniaXML();

                podmiot1 = new Podmiot(soap, link1Text);
                podmiot2 = new Podmiot(soap, link2Text);
                

                cechy.WyliczPodobienstwaCech(ref podmiot1, ref podmiot2);
                listaBadan.Add(cechy);


                System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {

                    this.Height = 300;
                    PorownajButton.IsEnabled = true;
                    SzczegolyButton.IsEnabled = true;

                    SzczegolySpinner.Visibility = Visibility.Collapsed;
                    SzczegolyTextBlock.Visibility = Visibility.Visible;
                    SzczegolyInfo.Visibility = Visibility.Visible;

                    PorownajSpinner.Visibility = Visibility.Collapsed;
                    PorownajTextBlock.Visibility = Visibility.Visible;
                    PorownajClone.Visibility = Visibility.Visible;
                    ComboBoxHistoria.Items.Add(NazwaProjekt1 + " ↔ " + NazwaProjekt2);
                    ComboBoxHistoria.Visibility = Visibility.Visible;
                    ComboBoxHistoria.SelectedIndex = ComboBoxHistoria.Items.Count-1;
                    //labelPodobienstwo.Content = "Podobieństwo ogólne: " + Math.Round(GetPodobienstwoOgolne(listaBadan.ElementAt(ComboBoxHistoria.SelectedIndex)), 2);
                    SzczegolyButton.IsEnabled = true;
                });

                System.Media.SoundPlayer player = new System.Media.SoundPlayer("alert.wav");
                player.Play();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
                System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    link1.BorderBrush = System.Windows.Media.Brushes.Red;
                    link2.BorderBrush = System.Windows.Media.Brushes.Red;

                });
            }

        }

        private void Button_Click_Szczegoly(object sender, RoutedEventArgs e)
        {
            if(szczegoly != null)
            {
                szczegoly.Ustawienia();
                szczegoly.Visibility = Visibility.Visible;
            }
            else
            {
                szczegoly = new Szczegoly();

                szczegoly.Show();

            }
        }

        private float GetPodobienstwoOgolne(KontenerCech cechy)
        {
            sumaOgolna = 0.0f;
            sumaKodu = 0.0f;
            sumaStruktury = 0.0f;

            foreach (var item in cechy.listaCech)
            {
                switch (item.typ)
                {
                    case "Cecha kodu":
                        sumaKodu += item.podobienstwo * item.waga;
                        break;
                    case "Cecha struktury":
                        sumaStruktury += item.podobienstwo * item.waga;
                        break;
                }
                sumaOgolna += item.podobienstwo * item.waga;
            }

            return sumaOgolna / cechy.listaCech.Sum(item => item.waga);
        }

        private void ComboBoxHistoria_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            labelPodobienstwo.Content = "Podobieństwo ogólne: " + Math.Round(GetPodobienstwoOgolne(listaBadan.ElementAt(ComboBoxHistoria.SelectedIndex)), 2);
        }
    }
}
