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
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Note_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> notesToList = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            if(Entry_Box.Text == null || Entry_Box.Text.Trim() == "")
            {
                return;
            }
            Note_List.Items.Add("Fecha y hora de creacion\n" + DateTime.Now.ToString() + "\n" + Entry_Box.Text.Trim());
        }

        private void Archivar_Click(object sender, RoutedEventArgs e)
        {
            Archive_Notes.Items.Add("Fecha y hora de archivado\n" + DateTime.Now.ToString() + "\n" + Note_List.SelectedValue);
            Note_List.ItemsSource.;
        }

        private void Borrar_Click(object sender, RoutedEventArgs e)
        {
            Deleted_Notes.Items.Add("Fecha y hora de borrado\n" + DateTime.Now.ToString() + "\n" + Note_List.SelectedValue);
            Note_List.Items.Clear();

        }
    }
}
