using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

namespace Rejest_Wynagrodzeń_Desktop
{
    /// <summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Historia> historia =
            new ObservableCollection<Historia>();

        public MainWindow()
        {
            InitializeComponent();
            DgHistoria.ItemsSource = historia;
        }

        private void BtnOblicz_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(
                TxtBrutto.Text.Replace(',', '.'),out double brutto) || brutto <= 0)
            {
                MessageBox.Show("Podaj poprawną kwotę brutto!");
                return;
            }

            bool ulga = ChkUlga.IsChecked == true;

            string umowa = RbPraca.IsChecked == true
                ? "O pracę"
                : "Zlecenie";

            Kalkulator kalkulator = new Kalkulator(brutto, ulga);

            double zus = kalkulator.ZUS();
            double zdrowotna = kalkulator.Zdrowotna();
            double pit = kalkulator.PIT();
            double netto = kalkulator.Netto();

            TxtNetto.Text = $"Wynagrodzenie netto: {netto:F2} PLN";
            TxtZUS.Text = $"Składki ZUS: {zus:F2} PLN";
            TxtZdrowotna.Text = $"Składka zdrowotna: {zdrowotna:F2} PLN";
            TxtPIT.Text = $"Zaliczka na podatek PIT: {pit:F2} PLN";

            historia.Add(new Historia
            {
                Data = DateTime.Now.ToString("yyyy-MM-dd"),
                Brutto = brutto.ToString("F2"),
                Umowa = umowa,
                Netto = netto.ToString("F2"),
                Ulga = ulga ? "Tak" : "Nie"
            });
        }
    }

    public class Historia
    {
        public string Data { get; set; }
        public string Brutto { get; set; }
        public string Umowa { get; set; }
        public string Netto { get; set; }
        public string Ulga { get; set; }
    }
}
