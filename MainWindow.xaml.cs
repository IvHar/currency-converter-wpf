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

namespace CurrencyConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Dictionary<string, decimal> cardBuyRates = new Dictionary<string, decimal>
        {
            { "USD", 17.9251m },
            { "EUR", 20.3091m },
            { "RUB", 0.2456m },
            { "RON", 4.0598m },
            { "UAH", 0.4389m },
        };

        private static Dictionary<string, decimal> cardSellRates = new Dictionary<string, decimal>
        {
            { "USD", 18.1653m },
            { "EUR", 20.6941m },
            { "RUB", 0.2563m },
            { "RON", 4.1950m },
            { "UAH", 0.4671m },
        };

        private static Dictionary<string, decimal> cardBnmRates = new Dictionary<string, decimal>
        {
            { "USD", 18.0378m },
            { "EUR", 20.5402m },
            { "RUB", 0.2504m },
            { "RON", 4.1250m },
            { "UAH", 0.4517m },
        };

        private static Dictionary<string, decimal> bankBuyRates = new Dictionary<string, decimal>
        {
            { "USD", 17.9200m },
            { "EUR", 20.3011m },
            { "RUB", 0.2456m },
            { "RON", 4.0589m },
            { "UAH", 0.4389m },
            { "GBP", 23.8012m },
            { "CHF", 19.6845m }
        };

        private static Dictionary<string, decimal> bankSellRates = new Dictionary<string, decimal>
        {
            { "USD", 18.1800m },
            { "EUR", 20.6050m },
            { "RUB", 0.2563m },
            { "RON", 4.1900m },
            { "UAH", 0.4671m },
            { "GBP", 24.7042m },
            { "CHF", 20.7134m }
        };

        private static Dictionary<string, decimal> bankBnmRates = new Dictionary<string, decimal>
        {
            { "USD", 18.0378m },
            { "EUR", 20.5402m },
            { "RUB", 0.2504m },
            { "RON", 4.1250m },
            { "UAH", 0.4517m },
            { "GBP", 24.2092m },
            { "CHF", 19.7134m }
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (comboCurrency.SelectedIndex < 0 || string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                MessageBox.Show("Fill all the fields!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            decimal amount;
            if (!decimal.TryParse(txtAmount.Text, out amount))
            {
                MessageBox.Show("Invalid input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string selectedCurrency = (comboCurrency.SelectedItem as ComboBoxItem).Content.ToString();
            MessageBoxResult result = MessageBox.Show("Which rate to use?\nYes - card\nNo - bank", "Rate selection", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            Dictionary<string, decimal> buyRates;
            Dictionary<string, decimal> sellRates;
            Dictionary<string, decimal> nbmRates;
            
            if (result == MessageBoxResult.Yes)
            {
                buyRates = cardBuyRates;
                sellRates = cardSellRates;
                nbmRates = cardBnmRates;
            }
            else if (result == MessageBoxResult.No)
            {
                buyRates = bankBuyRates;
                sellRates = bankSellRates;
                nbmRates = bankBnmRates;
            }
            else
            {
                return;
            }

            if (!buyRates.ContainsKey(selectedCurrency) || !sellRates.ContainsKey(selectedCurrency) || !nbmRates.ContainsKey(selectedCurrency))
            {
                MessageBox.Show($"For {selectedCurrency} there is no data in chosen mode!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            lblBuyRate.Content = $"{buyRates[selectedCurrency] * amount:F4} MDL for {amount} {selectedCurrency}";
            lblSellRate.Content = $"{sellRates[selectedCurrency] * amount:F4} MDL for {amount} {selectedCurrency}";
            lblNbmRate.Content = $"{nbmRates[selectedCurrency] * amount :F4} MDL for  {amount}  {selectedCurrency}";
        }
    }
}
