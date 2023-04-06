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
using System.IO;
using System.Windows.Threading;

namespace Note_App
{
    /// <summary>
    /// The main idea is to take notes, saving them with a name, and having the posibility
    /// </summary>
    public partial class MainWindow : Window
    {
        //list of notes, seguramente sea mas facil empezar a trabajr con un diccionario
        List<Notes> pendingNotesList = new List<Notes>();
        List<OldNotes> archivedNotesList = new List<OldNotes>();
        List<OldNotes> deletedNotesList = new List<OldNotes>();

        public MainWindow()
        {
            InitializeComponent();
            reloj();
            Leer_Notas();
            Mostrar_Notas();

        }


        //funciones para leer y escribir los archivos de texto
        private void Leer_Notas()
        {
            string line;
            StreamReader readerPending = new StreamReader(@"..\..\Notas_Pendientes.txt");
            while ((line = readerPending.ReadLine()) != null)
            {
                string[] words = line.Split(',');
                pendingNotesList.Add(new Notes(words[0], words[1], words[2]));
            }
            readerPending.Close();
            
            StreamReader readerArchived = new StreamReader(@"..\..\Notas_Archivadas.txt");
            while ((line = readerArchived.ReadLine()) != null)
            {
                string[] words = line.Split(',');
                archivedNotesList.Add(new OldNotes(words[0], words[1], words[2], words[3]));
            }
            readerArchived.Close();
            
            StreamReader readerDeleted = new StreamReader(@"..\..\Notas_Borradas.txt");
            while ((line = readerDeleted.ReadLine()) != null)
            {
                string[] words = line.Split(',');
                deletedNotesList.Add(new OldNotes(words[0], words[1], words[2], words[3]));
            }
            readerDeleted.Close();
        }
        private void Escribir_notas_pendientes()
        {
            File.WriteAllText(@"..\..\Notas_Pendientes.txt", String.Empty);
            foreach (Notes notas in pendingNotesList)
            {
                string lineas = notas.note + "," + notas.title + "," + notas.date + "\n";
                File.AppendAllText(@"..\..\Notas_Pendientes.txt", lineas);
            }
        }
        private void Escribir_notas_archivadas()
        {
            File.WriteAllText(@"..\..\Notas_Archivadas.txt", String.Empty);
            foreach (OldNotes notas in archivedNotesList)
            {
                string lineas = notas.note + "," + notas.title + "," + notas.date + "," + notas.FinishDate + "\n";
                File.AppendAllText(@"..\..\Notas_Archivadas.txt", lineas);
            }
        }
        private void Escribir_notas_borradas()
        {
            File.WriteAllText(@"..\..\Notas_Borradas.txt", String.Empty);
            foreach (OldNotes notas in deletedNotesList)
            {
                string lineas = notas.note + "," + notas.title + "," + notas.date + "," + notas.FinishDate + "\n";
                File.AppendAllText(@"..\..\Notas_Borradas.txt", lineas);
            }
        }

        //funcion que actualiza lo que aparece en las lsitview
        private void Mostrar_Notas()
        {
            Note_List.ItemsSource = null;
            Note_List.ItemsSource = pendingNotesList;
            Archive_Notes.ItemsSource = null;
            Archive_Notes.ItemsSource = archivedNotesList;
            Deleted_Notes.ItemsSource = null;
            Deleted_Notes.ItemsSource = deletedNotesList;

        }



        //funciones de los botones 
        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            if (Titulo_Box.Text == null || Titulo_Box.Text.Trim() == "")
            {
                return;
            }
            pendingNotesList.Add(new Notes(Nota_box.Text, Titulo_Box.Text,DateTime.Now.ToString()));
            Mostrar_Notas();
            Titulo_Box.Clear();
            Nota_box.Clear();
            Escribir_notas_pendientes();
        }

        private void Archivar_Click(object sender, RoutedEventArgs e)
        {
            if(Note_List.SelectedValue == null)
            {
                return;
            }
            int indexOfPending = Note_List.SelectedIndex;
            string finishTime = DateTime.Now.ToString();
            archivedNotesList.Add(new OldNotes(pendingNotesList[indexOfPending].note,pendingNotesList[indexOfPending].title, pendingNotesList[indexOfPending].date, finishTime));
            pendingNotesList.RemoveAt(indexOfPending);
            Mostrar_Notas();
            Escribir_notas_archivadas();
            Escribir_notas_pendientes();
        }


        private void Borrar_Click(object sender, RoutedEventArgs e)
        {
            if (Note_List.SelectedValue == null)
            {
                return;
            }
            int indexOfPending = Note_List.SelectedIndex;
            string finishTime = DateTime.Now.ToString();
            deletedNotesList.Add(new OldNotes(pendingNotesList[indexOfPending].note,pendingNotesList[indexOfPending].title, pendingNotesList[indexOfPending].date,finishTime));
            pendingNotesList.RemoveAt(indexOfPending);
            Mostrar_Notas();
            Escribir_notas_borradas();
            Escribir_notas_pendientes();
        }

        private void Mostrar_notas_de_pendientes_Click(object sender, RoutedEventArgs e)
        {
            if (Note_List.SelectedValue == null)
            {
                return;
            }
            int indexOfPending = Note_List.SelectedIndex;
            MessageBox.Show(pendingNotesList[indexOfPending].note,"Nota");
        }
        private void Mostrar_notas_archivadas_Click(object sender, RoutedEventArgs e)
        {
            if (Archive_Notes.SelectedValue == null)
            {
                return;
            }
            int indexOfPending = Archive_Notes.SelectedIndex;
            MessageBox.Show(archivedNotesList[indexOfPending].note, "Nota");
        }
        private void Mostrar_notas_borradas_Click(object sender, RoutedEventArgs e)
        {
            if (Deleted_Notes.SelectedValue == null)
            {
                return;
            }
            int indexOfPending = Deleted_Notes.SelectedIndex;
            MessageBox.Show(deletedNotesList[indexOfPending].note, "Nota");
        }

        //funcion para el reloj
        private void reloj()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += tickevent;
            timer.Start();
        }

        private void tickevent(object sender, EventArgs e)
        {
            Clock_box.Text = DateTime.Now.ToString();
        }
    }
}
