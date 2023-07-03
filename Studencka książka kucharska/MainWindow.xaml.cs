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
using System.IO;

namespace Studencka_książka_kucharska
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
  
    public partial class MainWindow : Window
    {
        private string szukana;
        public string Szukana
        {
            get{
                return szukana;
            }
            set{ 
                szukana=value;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Nowy_Click(object sender, RoutedEventArgs e)
        {
            nowy kreator= new nowy();
            kreator.ShowDialog();

        }

        private void Przegladaj_Click(object sender, RoutedEventArgs e)
        {
            ksiega przeglad = new ksiega();
            try
            {
                przeglad.ShowDialog();
            }
            catch (Exception)
            {
            }

        }

        private void Szukaj_Click(object sender, RoutedEventArgs e)
        {
            string[] przepisy = Directory.GetFiles("..\\..\\..\\przepisy", "*.txt");
            int licznik = przepisy.Length;
            for (int i = 0; i < licznik; i++)
            {
                przepisy[i] = przepisy[i].Substring(18);
                przepisy[i] = przepisy[i].Remove(przepisy[i].Length - 4);
            }
            
            for (int i= 0;i < licznik; i++){
                if (przepisy[i] == szukaj.Text) {
                    Szukana = szukaj.Text;
                    ksiega przeglad = new ksiega();
                    przeglad.ShowDialog();
                }
            }
        }
    }
}
