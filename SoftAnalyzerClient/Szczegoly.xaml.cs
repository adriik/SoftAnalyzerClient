using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using System.Windows.Shapes;

namespace SoftAnalyzerClient
{
    /// <summary>
    /// Logika interakcji dla klasy Szczegoly.xaml
    /// </summary>
    public partial class Szczegoly : Window
    {
        private Cecha cecha;
        public Szczegoly()
        {
            InitializeComponent();

            if (MainWindow.cechy.listaCech != null)
            {
                var lista = new ObservableCollection<Cecha>(MainWindow.cechy.listaCech);
                dataGrid1.ItemsSource = lista;

            }

        }

        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cecha != null)
            {
                gridziok.Children.Remove(cecha.szczegoloweDane);
            }

            DataGrid gd = (DataGrid)sender;
            cecha = gd.SelectedItem as Cecha;

            cecha.szczegoloweDane.SetValue(Grid.ColumnProperty, 1);
            cecha.szczegoloweDane.IsReadOnly = true;
            cecha.szczegoloweDane.AutoGenerateColumns = false;
            cecha.szczegoloweDane.HorizontalAlignment = HorizontalAlignment.Left;
            cecha.szczegoloweDane.VerticalAlignment = VerticalAlignment.Top;
            gridziok.Children.Add(cecha.szczegoloweDane);

            Console.WriteLine("Byłem " + cecha.nazwa);
            
        }

    }
}
