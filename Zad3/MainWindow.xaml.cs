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
using System.Globalization;

namespace Zad3
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Mydata> _data = new List<Mydata>();

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            _data = new List<Mydata>();
            var filePath = "C:/Users/Mikołaj/Downloads/skradzione auta.csv";

            using (var reader = new StreamReader(filePath))
            {
              
                reader.ReadLine();
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var data = new Mydata
                    {
                        
                        State = values[0],
                        LiczbaZgloszen = int.TryParse(values[1], NumberStyles.Any, CultureInfo.InvariantCulture, out int tmpLiczbaZgloszen) ? tmpLiczbaZgloszen : 0,
                        WartoscSkradzionychAut = decimal.TryParse(values[2], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal tmpWartoscSkradzionychAut) ? tmpWartoscSkradzionychAut : 0m,
                        WartoscOdzyskanychAut = decimal.TryParse(values[3], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal tmpWartoscOdzyskanychAut) ? tmpWartoscOdzyskanychAut : 0m,
                        ProcentOdzyskanychAut = decimal.TryParse(values[4], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal tmpProcentOdzyskanychAut) ? tmpProcentOdzyskanychAut : 0m
                    };

                    _data.Add(data);
                    var output = new StringBuilder();
                    
                }
            }
        }
        // Analiza !
        public void btnLiczbaZgloszen_Click(object sender, RoutedEventArgs e)
        {
            var totalReports = _data.Sum(d => d.LiczbaZgloszen);
            MessageBox.Show($"Łączna liczba zgłoszonych kradzieży aut w całym kraju: {totalReports}", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            historyWindow.AddHistoryEntry("Łączna liczba zgłoszonych kradzieży aut w całym kraju:", $"{totalReports:C}");

            
        }
        //Analiza 2
        private void btnSredniaWartosc_Click(object sender, RoutedEventArgs e)
        {
            if (_data != null && _data.Any())
            {
                var srednia = _data.Average(d => d.WartoscSkradzionychAut);
                MessageBox.Show($"Średnia wartość skradzionych aut na stan: {srednia:C}", "Średnia wartość", MessageBoxButton.OK, MessageBoxImage.Information);
                historyWindow.AddHistoryEntry("Średnia wartość skradzionych aut", $"{srednia:C}");
            }
            else
            {
                MessageBox.Show("Dane są puste.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        //analiza3
        private void btnMaxZgloszenia_Click(object sender, RoutedEventArgs e)
        {
            if (_data != null && _data.Any())
            {
                var maxZgloszeniaStan = _data.OrderByDescending(d => d.LiczbaZgloszen).FirstOrDefault();
                if (maxZgloszeniaStan != null)
                {
                    MessageBox.Show($"Stan z największą liczbą zgłoszeń kradzieży: {maxZgloszeniaStan.State} ({maxZgloszeniaStan.LiczbaZgloszen} zgłoszeń)", "Stan z największą liczbą zgłoszeń", MessageBoxButton.OK, MessageBoxImage.Information);
                    historyWindow.AddHistoryEntry("Analiza stanu z największą liczbą zgłoszeń kradzieży", $"{maxZgloszeniaStan.State} - {maxZgloszeniaStan.LiczbaZgloszen} zgłoszeń");              
                 }
            }
            else
            {
                MessageBox.Show("Dane są puste.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        //analiza4
        private void btnMinZgloszenia_Click(object sender, RoutedEventArgs e)
        {
            if (_data != null && _data.Any())
            {
                var minZgloszeniaStan = _data.OrderBy(d => d.LiczbaZgloszen).FirstOrDefault();
                if (minZgloszeniaStan != null)
                {
                    MessageBox.Show($"Stan z najmniejszą liczbą zgłoszeń kradzieży: {minZgloszeniaStan.State} ({minZgloszeniaStan.LiczbaZgloszen} zgłoszeń)", "Stan z najmniejszą liczbą zgłoszeń", MessageBoxButton.OK, MessageBoxImage.Information);
                    historyWindow.AddHistoryEntry("Analiza stanu z najmniejszą liczbą zgłoszeń kradzieży", $"{minZgloszeniaStan.State} - {minZgloszeniaStan.LiczbaZgloszen} zgłoszeń");
                }
            }
            else
            {
                MessageBox.Show("Dane są puste.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        //analiza5
        private void btnLacznaWartoscOdzyskanych_Click(object sender, RoutedEventArgs e)
        {
            if (_data != null && _data.Any())
            {
                var lacznaWartoscOdzyskanych = _data.Sum(d => d.WartoscOdzyskanychAut);
                MessageBox.Show($"Łączna wartość odzyskanych aut w całym kraju: {lacznaWartoscOdzyskanych:C}", "Łączna wartość odzyskanych aut", MessageBoxButton.OK, MessageBoxImage.Information);
                historyWindow.AddHistoryEntry("Łączna wartość odzyskanych aut w całym kraju", $"{lacznaWartoscOdzyskanych:C}");
            }
            else
            {
                MessageBox.Show("Dane są puste.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        //analiza6
        private void btnNajwyzszyProcentOdzyskanych_Click(object sender, RoutedEventArgs e)
        {
            if (_data != null && _data.Any())
            {
                var maxProcentOdzyskanychStan = _data.OrderByDescending(d => d.ProcentOdzyskanychAut).FirstOrDefault();
                if (maxProcentOdzyskanychStan != null)
                {
                    MessageBox.Show($"Stan z najwyższym procentem odzyskanych aut: {maxProcentOdzyskanychStan.State} ({maxProcentOdzyskanychStan.ProcentOdzyskanychAut}%)", "Stan z najwyższym procentem odzyskanych aut", MessageBoxButton.OK, MessageBoxImage.Information);
                    historyWindow.AddHistoryEntry("Analiza stanu z najwyższym procentem odzyskanych aut", $"{maxProcentOdzyskanychStan.State} - {maxProcentOdzyskanychStan.ProcentOdzyskanychAut}%");
                }
            }
            else
            {
                MessageBox.Show("Dane są puste.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        //analiza7
        private void btnNajnizszyProcentOdzyskanych_Click(object sender, RoutedEventArgs e)
        {
            if (_data != null && _data.Any())
            {
                var minProcentOdzyskanychStan = _data.OrderBy(d => d.ProcentOdzyskanychAut).FirstOrDefault();
                if (minProcentOdzyskanychStan != null)
                {
                    MessageBox.Show($"Stan z najniższym procentem odzyskanych aut: {minProcentOdzyskanychStan.State} ({minProcentOdzyskanychStan.ProcentOdzyskanychAut}%)", "Stan z najniższym procentem odzyskanych aut", MessageBoxButton.OK, MessageBoxImage.Information);
                    historyWindow.AddHistoryEntry("Analiza stanu z najniższym procentem odzyskanych aut", $"{minProcentOdzyskanychStan.State} - {minProcentOdzyskanychStan.ProcentOdzyskanychAut}%");
                }
            }
            else
            {
                MessageBox.Show("Dane są puste.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        //analiza8
        private void btnStanWyzejSredniej_Click(object sender, RoutedEventArgs e)
        {
            if (_data != null && _data.Any())
            {
                var sredniaKrajowa = _data.Average(d => d.ProcentOdzyskanychAut);
                var stanyWyzejSredniej = _data.Where(d => d.ProcentOdzyskanychAut > sredniaKrajowa)
                                               .OrderBy(d => d.State)
                                               .Select(d => $"{d.State} - {d.ProcentOdzyskanychAut}%");

                var wynik = new StringBuilder($"Średnia krajowa procentu odzyskanych aut: {sredniaKrajowa:F2}%\n\n");
                wynik.AppendLine("Stany z procentem odzyskanych aut wyższym niż średnia krajowa:");

                if (!stanyWyzejSredniej.Any())
                {
                    wynik.AppendLine("Brak stanów spełniających kryteria.");
                }
                else
                {
                    foreach (var stan in stanyWyzejSredniej)
                    {
                        wynik.AppendLine(stan);
                    }
                }

                MessageBox.Show(wynik.ToString(), "Stany wyżej niż średnia krajowa", MessageBoxButton.OK, MessageBoxImage.Information);
                historyWindow.AddHistoryEntry("Analiza stanów z procentem odzyskanych aut wyższym niż średnia krajowa", wynik.ToString());
            }
            else
            {
                MessageBox.Show("Dane są puste.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        //analina9
        private void btnWartoscPowyzejSredniej_Click(object sender, RoutedEventArgs e)
        {
            if (_data != null && _data.Any())
            {
                var sredniaZgloszen = _data.Average(d => d.LiczbaZgloszen);
                var lacznaWartosc = _data.Where(d => d.LiczbaZgloszen > sredniaZgloszen)
                                          .Sum(d => d.WartoscSkradzionychAut);

                MessageBox.Show($"Łączna wartość skradzionych aut w stanach z liczbą zgłoszeń powyżej średniej: {lacznaWartosc:C}", "Łączna wartość skradzionych aut powyżej średniej", MessageBoxButton.OK, MessageBoxImage.Information);
                historyWindow.AddHistoryEntry("Łączna wartość skradzionych aut w stanach z liczbą zgłoszeń powyżej średniej", $"{lacznaWartosc:C}");
            }
            else
            {
                MessageBox.Show("Dane są puste.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        //analiza10
        private void btnRankingSkradzionychAut_Click(object sender, RoutedEventArgs e)
        {
            if (_data != null && _data.Any())
            {
                var ranking = _data.OrderByDescending(d => d.WartoscSkradzionychAut)
                                   .Take(10)
                                   .Select((d, index) => $"{index + 1}. {d.State} - {d.WartoscSkradzionychAut:C}")
                                   .ToList();

                var wynik = new StringBuilder("Ranking stanów według wartości skradzionych aut (Top 10):\n");
                foreach (var pozycja in ranking)
                {
                    wynik.AppendLine(pozycja);
                }

                MessageBox.Show(wynik.ToString(), "Ranking Stanów (Top 10)", MessageBoxButton.OK, MessageBoxImage.Information);
                historyWindow.AddHistoryEntry("Ranking Stanów (Top 10)", $"{wynik:C}");
            }
            else
            {
                MessageBox.Show("Dane są puste.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        //Przycisk dla historii 

        private HistoryWindow historyWindow = new HistoryWindow();

        private void btnShowHistory_Click(object sender, RoutedEventArgs e)
        {
            historyWindow.Show();
        }


    }
}
        
