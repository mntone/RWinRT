using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace RWinRT.CSharpApp
{
	public sealed partial class MainWindow : Window
	{
		private int i = 0;

		public MainWindow()
		{
			InitializeComponent();

			Title = "R/WinRT (C#)";
		}

		private void OnMessageButtonClick(object sender, RoutedEventArgs e)
		{
			string message;
#if !CSHARP_AUTOGEN_IS_V2
			// vvv General version (CSharpAutogen V3) vvv
			switch (i++ % 4)
			{
				case 0:
					message = R.MainWindow_MessageButton_Message.Value;
					break;
				case 1:
					message = R.MainWindow_MessageButton_Message2.Value;
					break;
				case 2:
					message = R.MainWindow_MessageButton_Message3.Format(i);
					break;
				case 3:
				default:
					message = R.MainWindow_MessageButton_Message4.Value;
					break;
			}
#else
			// vvv Initial release version (CSharpAutogen V2) vvv
			// Require mode=2 (<MntoneResourceGenerateMode>2</MntoneResourceGenerateMode> in csproj)
			switch (i++ % 4)
			{
				case 0:
					message = R.MainWindow_MessageButton_Message.GetValue();
					break;
				case 1:
					message = R.MainWindow_MessageButton_Message2.GetValue();
					break;
				case 2:
					message = R.MainWindow_MessageButton_Message3.GetFormatValue(i);
					break;
				case 3:
				default:
					message = R.MainWindow_MessageButton_Message4.GetValue();
					break;
			}
#endif

			var dialog = new ContentDialog()
			{
				// Apply specified settings, such as font family.
				Language = languagesRadioButtons.SelectedItem as string,
				XamlRoot = Content.XamlRoot,
				Title = "Hello, R/WinRT (C#)",
				Content = message,
				CloseButtonText = "OK",
			};
			_ = dialog.ShowAsync();
		}

		private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (sender is RadioButtons radioButtons)
			{
				var selectedValue = radioButtons.SelectedItem as string;
				if (selectedValue != null)
				{
					ResourceManager.ChangeLanguage(selectedValue);
				}
			}
		}
	}
}
