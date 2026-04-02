using Windows.ApplicationModel.Core;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using System;
using System.Linq;

namespace Calculator
{
	public sealed partial class MortgageCalculator : Page
	{
		// default var
		public static MortgageCalculator mortgageCalculator { get; set; }

		// default functions
		public MortgageCalculator()
		{
			InitializeComponent();

			mortgageCalculator = this;

		}

		// page functions
		private void pageLoaded(object sender, RoutedEventArgs e)
		{

			// window minimum size
			ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(450, 600));

			// enable title bar full customiztion
			CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
			// title bar customization
			ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;

			titleBar.ButtonBackgroundColor = Windows.UI.Colors.Transparent;
			titleBar.ButtonInactiveBackgroundColor = Windows.UI.Colors.Transparent;
			titleBar.ButtonInactiveForegroundColor = Windows.UI.Colors.White;
		}

		// Calculate and display the user's monthly repayment
		private void calculateButton_Click(object sender, RoutedEventArgs e)
		{
			// Initialise calculation variables
			double principal = 0;
			int totalMonths = 0;
			double annualRate = 0;

			// Retrieve input values
			MortgageCalculator calculator = MortgageCalculator.mortgageCalculator;
			string principleBorrowed = mortgageCalculator.borrowedTextBox.Text;
			string years = mortgageCalculator.yearsTextBox.Text;
			string months = mortgageCalculator.monthsTextBox.Text;
			string annualInterestRate = mortgageCalculator.annualInterestRateTextBox.Text;

			// Retrieve output controls
			TextBox monthlyRateOutput = mortgageCalculator.monthlyInterestRateTextBox;
			TextBox monthlyRepaymentOutput = mortgageCalculator.monthlyRepaymentTextBox;

			try
			{
				// Convert inputs to numeric values
				principal = double.Parse(principleBorrowed);
				totalMonths = (int.Parse(years) * 12) + int.Parse(months);
				if (totalMonths <= 0)
					throw new Exception("Total loan duration must be greater than zero.");

				annualRate = double.Parse(annualInterestRate) / 100;

				// Calculate monthly interest rate
				double monthlyRate = annualRate / 12;

				// Calculate monthly repayment using the formula: M = P[i(1+i)^n]/[(1+i)^n-1]
				double monthlyRepayment = principal
					* monthlyRate * Math.Pow(1 + monthlyRate, totalMonths)
					/ (Math.Pow(1 + monthlyRate, totalMonths) - 1);

				// Display results
				monthlyRateOutput.Text = (monthlyRate * 100).ToString("F2");
				monthlyRepaymentOutput.Text = monthlyRepayment.ToString("F2");

			}
			catch (Exception problem)
			{ 
				string errorMessage = "ERROR: ";
				if (!double.TryParse(principleBorrowed, out _))
					errorMessage += "Principal amount borrowed is invalid.";
				else if (!int.TryParse(years, out _))
					errorMessage += "Years must be a whole number.";
				else if (!int.TryParse(months, out _))
					errorMessage += "Months must be a whole number.";
				else if (totalMonths <= 0)
					errorMessage += problem.Message;
				else if (!double.TryParse(annualInterestRate, out _))
					errorMessage += "Annual interest rate is invalid.";
				else
					errorMessage += problem.Message;

				mortgageCalculator.errorDisplay.Text = errorMessage;
				FlyoutBase.ShowAttachedFlyout(mortgageCalculator.buttonPanel);
			}

		}

		// Return to the main menu page
		private void exitButton_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(MainMenu));
		}
	}
}