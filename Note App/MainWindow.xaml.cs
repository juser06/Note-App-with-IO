using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        List<Notes> pendingNotesList = new List<Notes>();
        List<OldNotes> archivedNotesList = new List<OldNotes>();
        List<OldNotes> deletedNotesList = new List<OldNotes>();
        int contar = 22;
        public MainWindow()
        {
            InitializeComponent();
            Mostrar_Notas();

        }

        private void Mostrar_Notas()
        {
            Note_List.ItemsSource = null;
            Note_List.ItemsSource = pendingNotesList;
            Archive_Notes.ItemsSource = null;
            Archive_Notes.ItemsSource = archivedNotesList;
            Deleted_Notes.ItemsSource = null;
            Deleted_Notes.ItemsSource = deletedNotesList;

        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            if (Entry_Box.Text == null || Entry_Box.Text.Trim() == "")
            {
                return;
            }
            pendingNotesList.Add(new Notes(contar, Entry_Box.Text));
            contar++;
            Mostrar_Notas();
        }

        private void Archivar_Click(object sender, RoutedEventArgs e)
        {
            if(Note_List.SelectedValue == null)
            {
                return;
            }
            int idx = Note_List.SelectedIndex;
            archivedNotesList.Add(new OldNotes(pendingNotesList[idx].id, pendingNotesList[idx].note, pendingNotesList[idx].date));
            pendingNotesList.RemoveAt(idx);
            Mostrar_Notas();
            //string paraArchivar = Note_List.SelectedValue.ToString();
            //int indexParaArchivar = Note_List.SelectedIndex;
            ////Archive_Notes.Items.Add("Fecha y hora de archivado\n" + DateTime.Now.ToString() + "\n" + paraArchivar);
            //if(pendingNotesList != null)
            //{
            //    pendingNotesList.RemoveAt(indexParaArchivar);
            //}
        }


        private void Borrar_Click(object sender, RoutedEventArgs e)
        {
            Deleted_Notes.Items.Add("Fecha y hora de borrado\n" + DateTime.Now.ToString() + "\n" + Note_List.SelectedValue);
            Note_List.Items.Clear();

        }

        private void Note_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
    public class Notes
    {
        public int id { get; set; }
        public string note { get; set; }
        public string date { get; set; }

        public Notes(int id, string note)
        {
            this.id = id;
            this.note = note;
            this.date = DateTime.Now.ToString();
        }
    }
    public class OldNotes : Notes
    {
        public string FinishDate { get; set; }

        public OldNotes(int id, string note, string date) : base(id, note)
        {
            FinishDate = DateTime.Now.ToString();
        }
    }
}
