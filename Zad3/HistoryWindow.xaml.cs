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
using System.Windows.Shapes;
using System.IO;

namespace Zad3
{
    /// <summary>
    /// Logika interakcji dla klasy HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        public HistoryWindow()
        {
            InitializeComponent();
            LoadHistory();
            this.Closing += HistoryWindow_Closing;
        }
        public void AddHistoryEntry(string analysisDescription, string result)
        {
            string entry = $"{DateTime.Now}: {analysisDescription} - Wynik: {result}\n";
            lbHistory.Items.Add(entry);
            File.AppendAllText("history.txt", entry + Environment.NewLine);
        }
        private void LoadHistory()
        {
            if (File.Exists("history.txt"))
            {
                var historyEntries = File.ReadAllLines("history.txt");
                foreach (var entry in historyEntries)
                {
                    lbHistory.Items.Add(entry);
                }
            }
        }
        public void HistoryWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true; 
            this.Hide(); 
        }
    }
}
