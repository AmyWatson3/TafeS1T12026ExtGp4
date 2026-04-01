using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class CurrencyCalculator : Page
	{
		private ObservableCollection<Currency> currencies = new ObservableCollection<Currency>();

		public CurrencyCalculator()
		{
			this.InitializeComponent();

			var usaRates = new Dictionary<CurrencyType, double>();
			usaRates.Add(CurrencyType.USA, 1.0);
			usaRates.Add(CurrencyType.EUR, 0.85189982);
			usaRates.Add(CurrencyType.GBP, 0.72872436);
			usaRates.Add(CurrencyType.INR, 74.257327);
			var usa = new Currency("USD - US Dollar", "US Dollars", CurrencyType.USA, usaRates, "Assets/USA1.PNG");
			currencies.Add(usa);

			var eurRates = new Dictionary<CurrencyType, double>();
			usaRates.Add(CurrencyType.EUR, 1.0);
			eurRates.Add(CurrencyType.USA, 1.1739732);
			eurRates.Add(CurrencyType.GBP, 0.8556672);
			eurRates.Add(CurrencyType.INR, 87.00755);
			var eur = new Currency("EUR - Euro", "Euros", CurrencyType.EUR, eurRates, "Assets/EUR1.PNG");
			currencies.Add(eur);

			var gbpRates = new Dictionary<CurrencyType, double>();
			usaRates.Add(CurrencyType.GBP, 1.0);
			gbpRates.Add(CurrencyType.USA, 1.371907);
			gbpRates.Add(CurrencyType.EUR, 1.1686692);
			gbpRates.Add(CurrencyType.INR, 101.68635);
			var gbp = new Currency("GBP - British Pound", "British Pounds", CurrencyType.GBP, gbpRates, "Assets/UK1.PNG");
			currencies.Add(gbp);

			var inrRates = new Dictionary<CurrencyType, double>();
			usaRates.Add(CurrencyType.INR, 1.0);
			inrRates.Add(CurrencyType.USA, 0.011492628);
			inrRates.Add(CurrencyType.EUR, 0.013492774);
			inrRates.Add(CurrencyType.GBP, 0.0098339397);
			var inr = new Currency("INR - Indian Rupee", "Indian Rupees", CurrencyType.INR, inrRates, "Assets/INDIA.PNG");
			currencies.Add(inr);
		}

		private void exitButton_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(MainMenu));
		}

		private void currencyButton_Click(object sender, RoutedEventArgs e)
		{
			UpdateConversionText(amountTextBox.Text, (Currency)fromComboBox.SelectedValue, (Currency)toComboBox.SelectedValue);
		}

		private void UpdateConversionText(string amount, Currency fromCurrency, Currency toCurrency)
		{
			var conversionAmount = Convert.ToDouble(amount) * fromCurrency.ConversionRates[toCurrency.CurrencyType];

			amountTextBlock.Text = $"{amount} {fromCurrency.CurrencyDisplay} =";
			resultTextBlock.Text = $"{QueryDollarSymbol(toCurrency.CurrencyType)}{conversionAmount.ToString("00.00")} {toCurrency.CurrencyDisplay}";
			fromConversionTextBlock.Text = $"1 {fromCurrency.CurrencyDisplay} = {fromCurrency.ConversionRates[toCurrency.CurrencyType]} {toCurrency.CurrencyDisplay}";
			toConversionTextBlock.Text = $"1 {toCurrency.CurrencyDisplay} = {toCurrency.ConversionRates[fromCurrency.CurrencyType]} {fromCurrency.CurrencyDisplay}";
		}

		private string QueryDollarSymbol(CurrencyType currencyType)
		{
			switch (currencyType)
			{
				case CurrencyType.USA:
					return "$";
				case CurrencyType.EUR:
					return "€";
				case CurrencyType.GBP:
					return "£";
				case CurrencyType.INR:
					return "₹";
				default:
					return "$";
			}
		}
	}

	public class Currency
	{
		public string Name { get; set; }
		public string CurrencyDisplay { get; set; }
		public CurrencyType CurrencyType { get; set; }
		public Dictionary<CurrencyType, double> ConversionRates { get; set; }
		public string Image { get; set; }

		public Currency(string name, string currencyDisplay, CurrencyType currencyType, Dictionary<CurrencyType, double> conversionRates, string image)
		{
			this.Name = name;
			this.CurrencyDisplay = currencyDisplay;
			this.CurrencyType = currencyType;
			this.ConversionRates = conversionRates;
			this.Image = image;
		}
	}

	public enum CurrencyType
	{
		USA,
		EUR,
		GBP,
		INR
	}
}