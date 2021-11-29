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
using System.Data;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json;

namespace CurrencyConverter_Static
{
    public partial class MainWindow : Window
    {
        Root val = new Root();

        public class Root
        {
            public Rate rates { get; set; }
            public long timestamp;
            public string license;
        }

        public class Rate
        {
            public double SEK { get; set; }
            public double GBP { get; set; }
            public double INR { get; set; }
            public double JPY { get; set; }
            public double USD { get; set; }
            public double NZD { get; set; }
            public double EUR { get; set; }
            public double CAD { get; set; }
            public double CNY { get; set; }
            public double ISK { get; set; }
            public double RUB { get; set; }
            public double NOK { get; set; }
            public double PHP { get; set; }
            public double DKK { get; set; }
            public double CZK { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
            GetValue();
        }

        private async void GetValue()
        {
            val = await GetData<Root>("https://openexchangerates.org/api/latest.json?app_id=2f0b7f5639b445e7b178e23024ce56d7");
            BindCurrency();
        }

        public static async Task<Root> GetData<T>(string url)
        {
            var myRoot = new Root();

            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMinutes(1);
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        var responseObject = JsonConvert.DeserializeObject<Root>(responseString);

                        MessageBox.Show("TimeStamp: " + responseObject.timestamp, "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                        return responseObject;
                    }
                    return myRoot;
                }
            }
            catch
            {
                return myRoot;
            }
        }

        private void BindCurrency()
        {
            DataTable dt = new DataTable();

            // Columns i datatable
            dt.Columns.Add("Text");
            dt.Columns.Add("Value");

            // Rows of the diffrent currencies
            dt.Rows.Add("--SELECT--", 0);
            dt.Rows.Add("SEK", val.rates.SEK);
            dt.Rows.Add("GBP", val.rates.GBP);
            dt.Rows.Add("INR", val.rates.INR);
            dt.Rows.Add("JPY", val.rates.JPY);
            dt.Rows.Add("USD", val.rates.USD);
            dt.Rows.Add("NZD", val.rates.NZD);
            dt.Rows.Add("EUR", val.rates.EUR);
            dt.Rows.Add("CAD", val.rates.CAD);
            dt.Rows.Add("CNY", val.rates.CNY);
            dt.Rows.Add("ISK", val.rates.ISK);
            dt.Rows.Add("RUB", val.rates.RUB);
            dt.Rows.Add("NOK", val.rates.NOK);
            dt.Rows.Add("PHP", val.rates.PHP);
            dt.Rows.Add("DKK", val.rates.DKK);
            dt.Rows.Add("CZK", val.rates.CZK);
            
            cmbFromCurrency.ItemsSource = dt.DefaultView;
            cmbFromCurrency.DisplayMemberPath = "Text";
            cmbFromCurrency.SelectedValuePath = "Value";
            cmbFromCurrency.SelectedIndex = 0;

            cmbToCurrency.ItemsSource = dt.DefaultView;
            cmbToCurrency.DisplayMemberPath = "Text";
            cmbToCurrency.SelectedValuePath = "Value";
            cmbToCurrency.SelectedIndex = 0;
        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            double convertedValue;

            if (txtCurrency.Text == null || txtCurrency.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter Currency", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                txtCurrency.Focus();
                return;
            } 
            else if (cmbFromCurrency.SelectedValue == null || cmbFromCurrency.SelectedIndex == 0)
            {
                MessageBox.Show("Please Slect Currency From", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                cmbFromCurrency.Focus();
                return;
            }
            else if (cmbToCurrency.SelectedValue == null || cmbToCurrency.SelectedIndex == 0)
            {
                MessageBox.Show("Please Slect Currency To", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                cmbToCurrency.Focus();
                return;
            }

            if (cmbFromCurrency.Text == cmbToCurrency.Text)
            {
                convertedValue = double.Parse(txtCurrency.Text);
                lblCurrency.Content = cmbToCurrency.Text + " " + convertedValue.ToString("N3");
            }
            else
            {
                convertedValue = double.Parse(cmbToCurrency.SelectedValue.ToString()) * double.Parse(txtCurrency.Text) / double.Parse(cmbFromCurrency.SelectedValue.ToString());

                lblCurrency.Content = cmbToCurrency.Text + " " + convertedValue.ToString("N3");
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
        }

        private void ClearControls()
        {
            txtCurrency.Text = string.Empty;

            if (cmbFromCurrency.Items.Count > 0)
            {
                cmbFromCurrency.SelectedIndex = 0;
            }

            if (cmbToCurrency.Items.Count > 0)
            {
                cmbToCurrency.SelectedIndex = 0;
            }

            lblCurrency.Content = "";
            txtCurrency.Focus();
        }
    }
}
