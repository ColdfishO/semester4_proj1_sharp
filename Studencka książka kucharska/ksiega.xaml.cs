using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Drawing;
using System.Linq;
using Microsoft.Win32;
namespace Studencka_książka_kucharska
{
    /// <summary>
    /// Logika interakcji dla klasy ksiega.xaml
    /// </summary>
    public partial class ksiega : Window
    {
        int licznik = 0;
        string[] przepisy = Directory.GetFiles("..\\..\\..\\przepisy", "*.txt");
        public ksiega()
        {
            InitializeComponent();
            int x = przepisy.Length;
            try { 
            
                
                for (int i = 0; i < x; i++)
                {
                    przepisy[i] = przepisy[i].Substring(18);
                    przepisy[i] = przepisy[i].Remove(przepisy[i].Length - 4);
                }
                string y = ((MainWindow)Application.Current.MainWindow).Szukana;
                if (y != null){
                    title.Content = y;

                    using (StreamReader czytnik = new StreamReader(@"..\..\..\przepisy\" + y + ".txt"))
                    {
                        string tekst;
                        while ((tekst = czytnik.ReadLine()) != "")
                        {
                            skladniki.Text += tekst + "\n";
                        }
                        while ((tekst = czytnik.ReadLine()) != null)
                        {
                            sposob.Text += tekst + "\n";
                        }
                    }
                    obraz.Source = new BitmapImage(new Uri(@"przepisy/" + y + ".png", UriKind.Relative));
                }
                else {
                    title.Content = przepisy[0];

                    using (StreamReader czytnik = new StreamReader(@"..\..\..\przepisy\" + przepisy[0] + ".txt"))
                    {
                        string tekst;
                        while ((tekst = czytnik.ReadLine()) != "")
                        {
                            skladniki.Text += tekst + "\n";
                        }
                        while ((tekst = czytnik.ReadLine()) != null)
                        {
                            sposob.Text += tekst + "\n";
                        }
                    }
                    obraz.Source = new BitmapImage(new Uri(@"../../../przepisy/" + przepisy[0] + ".png", UriKind.Relative));
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Nie ma żadnych przepisów!");
                Close();
            }
        }
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int dlugosc = przepisy.Length;
                licznik++;
                if (licznik == dlugosc)
                {
                    licznik = 0;
                }
                title.Content = przepisy[licznik];
                skladniki.Text = "";
                sposob.Text = "";
                using (StreamReader czytnik = new StreamReader(@"..\..\..\przepisy\" + przepisy[licznik] + ".txt"))
                {
                    string tekst;
                    while ((tekst = czytnik.ReadLine()) != "")
                    {
                        skladniki.Text += tekst + "\n";
                    }
                    while ((tekst = czytnik.ReadLine()) != null)
                    {
                        sposob.Text += tekst + "\n";
                    }
                }
                obraz.Source = new BitmapImage(new Uri(@"przepisy/" + przepisy[licznik] + ".png", UriKind.Relative));
            }
            catch (Exception) {
                MessageBox.Show("Nie ma żadnych przepisów!");
            }
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                licznik--;
                if (licznik < 0)
                {
                    licznik = przepisy.Length - 1;
                }
                title.Content = przepisy[licznik];
                skladniki.Text = "";
                sposob.Text = "";
                using (StreamReader czytnik = new StreamReader(@"..\..\..\przepisy\" + przepisy[licznik] + ".txt"))
                {
                    string tekst;
                    while ((tekst = czytnik.ReadLine()) != "")
                    {
                        skladniki.Text += tekst + "\n";
                    }
                    while ((tekst = czytnik.ReadLine()) != null)
                    {
                        sposob.Text += tekst + "\n";
                    }
                }
                obraz.Source = new BitmapImage(new Uri(@"przepisy/" + przepisy[licznik] + ".png", UriKind.Relative));
            }
            catch (Exception) {
                MessageBox.Show("Nie ma żadnych przepisów!");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult alert = MessageBox.Show("Czy na pewno chcesz usunąć ten przepis?", "Uwaga!", MessageBoxButton.YesNo);
                if (alert == MessageBoxResult.Yes)
                {
                    File.Delete(@"..\..\..\przepisy\" + przepisy[licznik] + ".txt");
                    File.Delete(@"..\..\..\przepisy\" + przepisy[licznik] + ".png");
                    MessageBox.Show("Przepis został usunięty");
                    przepisy = przepisy.Where(val => val != przepisy[licznik]).ToArray();
                }
            }
            catch (Exception) {
                MessageBox.Show("Tego przepisu już nie ma!");
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            sposob.Visibility=Visibility.Hidden;
            skladniki.Visibility = Visibility.Hidden;
            next.Visibility = Visibility.Hidden;
            prev.Visibility = Visibility.Hidden;
            edit.Visibility = Visibility.Hidden;
            delete.Visibility = Visibility.Hidden;
            addpic.Visibility = Visibility.Visible;
            save.Visibility = Visibility.Visible;
            sposobedit.Visibility = Visibility.Visible;
            skladnikiedit.Visibility = Visibility.Visible;
            skladnikiedit.Document.Blocks.Clear();
            skladnikiedit.Document.Blocks.Add(new Paragraph(new Run(skladniki.Text)));
            sposobedit.Document.Blocks.Clear();
            sposobedit.Document.Blocks.Add(new Paragraph(new Run(sposob.Text)));
        }


        private void Addpic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Wybierz obraz";
                openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg|All files (*.*)|*.*";
                openFileDialog.CheckFileExists = true;
                openFileDialog.CheckPathExists = true;
                if (openFileDialog.ShowDialog() == true)
                    obraz.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
            catch (Exception)
            {

            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string tytul =Convert.ToString(title.Content);
            string tekst(RichTextBox x)
            {
                TextRange textRange = new TextRange(
                    x.Document.ContentStart,
                    x.Document.ContentEnd
                );
                return textRange.Text;
            }
            
                try
                {


                    using (StreamWriter pisak = new StreamWriter("..\\..\\..\\przepisy\\" + tytul + ".txt"))
                    {
                        string zawartosc = tekst(skladnikiedit);
                        pisak.Write(zawartosc);
                        zawartosc = tekst(sposobedit);
                        pisak.Write("\n" + zawartosc);
                        MessageBox.Show("Zapisano pomyślnie!");
                    }
                   
                }
                
                catch (Exception)
                {
                    MessageBox.Show("Błąd zapisu!");
                }
                try {
                    var kodownik = new PngBitmapEncoder();
                    kodownik.Frames.Add(BitmapFrame.Create((BitmapSource)obraz.Source));
                    using (FileStream stream = new FileStream("..\\..\\..\\przepisy\\" + tytul + ".png", FileMode.Create))
                        kodownik.Save(stream);
                }
                catch (Exception)
                {
                    MessageBox.Show("Błąd zapisu obrazka!");
                }
            addpic.Visibility = Visibility.Hidden;
            save.Visibility = Visibility.Hidden;
            sposobedit.Visibility = Visibility.Hidden;
            skladnikiedit.Visibility = Visibility.Hidden;
            sposob.Visibility = Visibility.Visible;
            skladniki.Visibility = Visibility.Visible;
            next.Visibility = Visibility.Visible;
            prev.Visibility = Visibility.Visible;
            edit.Visibility = Visibility.Visible;
            delete.Visibility = Visibility.Visible;

        }
    }
}
