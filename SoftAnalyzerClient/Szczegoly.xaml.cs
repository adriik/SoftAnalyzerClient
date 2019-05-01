using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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

                ListCollectionView collectionView = new ListCollectionView(lista);
                collectionView.GroupDescriptions.Add(new PropertyGroupDescription("typ"));
                dataGrid1.ItemsSource = collectionView;

            }

        }

        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cecha != null)
            {
                szczegolyGrid.Children.Remove(cecha.szczegoloweDane);
            }
            DataGrid gd = (DataGrid)sender;
            
            cecha = gd.SelectedItem as Cecha;

            if (cecha.szczegoloweDane != null)
            {
                cecha.szczegoloweDane.SetValue(Grid.ColumnProperty, 1);
                cecha.szczegoloweDane.SetValue(Grid.RowProperty, 1);
                Thickness margin = new Thickness();
                margin.Left = 20;
                margin.Right = 20;
                margin.Top = 20;
                margin.Bottom = 20;
                cecha.szczegoloweDane.Margin = margin;
                cecha.szczegoloweDane.IsReadOnly = true;
                cecha.szczegoloweDane.AutoGenerateColumns = false;
                cecha.szczegoloweDane.VerticalAlignment = VerticalAlignment.Top;
                cecha.szczegoloweDane.HorizontalAlignment = HorizontalAlignment.Stretch;
                cecha.szczegoloweDane.HorizontalContentAlignment = HorizontalAlignment.Stretch;

                cecha.szczegoloweDane.Columns.ToList().ForEach(n => n.Width = new DataGridLength(1, DataGridLengthUnitType.Star));

                cecha.szczegoloweDane.Height = Double.NaN;

                if (etykietaPodglad.Visibility == Visibility.Hidden)
                {
                    ColumnDefinition gridCol2 = new ColumnDefinition();
                    szczegolyGrid.ColumnDefinitions.Add(gridCol2);
                    etykietaPodglad.Visibility = Visibility.Visible;
                    szczegolyWindow.Width = 1000;
                    szczegolyWindow.Height = 600;
                }

                szczegolyGrid.Children.Add(cecha.szczegoloweDane);
            }
        }

    }
}
