using Microsoft.UI.Xaml;

namespace RWinRT.CSharpApp
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
		}

		protected override void OnLaunched(LaunchActivatedEventArgs args)
		{
			_window = new MainWindow();
			_window.Activate();
		}

		private Window _window;
	}
}
