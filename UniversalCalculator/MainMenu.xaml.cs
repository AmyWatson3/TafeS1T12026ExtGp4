using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Calculator
{
	public sealed partial class MainMenu : Page
	{
		// default var
		public static MainMenu mainMenu { get; set; }

		// default functions
		public MainMenu()
		{
			InitializeComponent();

			mainMenu = this;

		}

		// page functions
		private void pageLoaded(object sender, RoutedEventArgs e)
		{

			// window minimum size
			ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(350, 600));

			// enable title bar full customiztion
			CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
			// title bar customization
			ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;

			titleBar.ButtonBackgroundColor = Windows.UI.Colors.Transparent;
			titleBar.ButtonInactiveBackgroundColor = Windows.UI.Colors.Transparent;
			titleBar.ButtonInactiveForegroundColor = Windows.UI.Colors.White;
		}

		private void mathButton_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(MainPage));
		}

		// Open the mortgage calculator page
		private void mortgageButton_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(MortgageCalculator));
		}

		private void currencyButton_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(CurrencyCalculator));
		}

		private void exitButton_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Exit();
		}

	}
}