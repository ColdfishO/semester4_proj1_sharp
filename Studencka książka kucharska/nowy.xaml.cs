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
using Microsoft.Win32;
using System.Drawing;

namespace Studencka_książka_kucharska
{
    /// <summary>
    /// Logika interakcji dla klasy nowy.xaml
    /// </summary>
    public partial class nowy : Window
    {
        public nowy()
        {
            InitializeComponent();
        }

        private void Dodajobraz_Click(object sender, RoutedEventArgs e)
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

        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {
            string tytul = title.Text;
            string tekst(RichTextBox x)
            {
                TextRange textRange = new TextRange(
                    x.Document.ContentStart,
                    x.Document.ContentEnd
                );
                return textRange.Text;
            }
            if (File.Exists("..\\..\\..\\przepisy\\" + tytul + ".txt"))
            {
                info.Content = "Błąd! Przepis już istnieje!";
            }
            else
            {
                try
                {


                    using (StreamWriter pisak = new StreamWriter("..\\..\\..\\przepisy\\" + tytul + ".txt"))
                    {
                        string zawartosc = tekst(skladniki);
                        pisak.Write(zawartosc);
                        zawartosc = tekst(sposob);
                        pisak.Write("\n" + zawartosc);
                        info.Content = "Zapisano pomyślnie!";
                    }
                   
                }
                
                catch (Exception)
                {
                    info.Content = "Błąd zapisu!";
                }
                try {
                    var kodownik = new PngBitmapEncoder();
                    kodownik.Frames.Add(BitmapFrame.Create((BitmapSource)obraz.Source));
                    using (FileStream stream = new FileStream("..\\..\\..\\przepisy\\" + tytul + ".png", FileMode.Create))
                        kodownik.Save(stream);
                }
                catch (Exception)
                {
                    info.Content = "Błąd zapisu obrazka!";
                }
            }
        
        }
    }
}
