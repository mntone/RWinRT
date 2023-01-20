#include "pch.h"
#include "MainWindow.xaml.h"
#if __has_include("MainWindow.g.cpp")
#include "MainWindow.g.cpp"
#endif

namespace winrt {
	using namespace ::winrt::Windows::Foundation;

	using namespace ::winrt::Microsoft::UI::Xaml;
	using namespace ::winrt::Microsoft::UI::Xaml::Controls;
}

using namespace winrt::RWinRT::CppApp::implementation;

MainWindow::MainWindow() {
	InitializeComponent();

	Title(L"R/WinRT (C++)");
}

void MainWindow::OnMessageButtonClick(IInspectable const&, RoutedEventArgs const&) {
	hstring message;
	switch (i++ % 4) {
	case 0:
		message = resourceContext_.Value<R::MainWindow_MessageButton_Message>();
		break;
	case 1:
		message = resourceContext_.Value<R::MainWindow_MessageButton_Message2>();
		break;
	case 2:
		message = resourceContext_.Format<R::MainWindow_MessageButton_Message3>(i);
		break;
	case 3:
	default:
		message = resourceContext_.Value<R::MainWindow_MessageButton_Message4>();
		break;
	}

	std::optional<hstring> language{ languagesRadioButtons().SelectedItem().try_as<hstring>() };

	ContentDialog dialog;
	if (language) {
		// Apply specified settings, such as font family.
		dialog.Language(language.value());
	}
	dialog.XamlRoot(Content().XamlRoot());
	dialog.Title(box_value(L"Hello, R/WinRT (C++)"));
	dialog.Content(box_value(message));
	dialog.CloseButtonText(L"OK");
	dialog.ShowAsync();
}

void MainWindow::OnSelectionChanged(IInspectable const& sender, SelectionChangedEventArgs const& /*args*/) {
	std::optional<hstring> selectedValue{ sender.as<RadioButtons>().SelectedItem().try_as<hstring>() };
	if (selectedValue) {
		resourceContext_.ChangeLanguage(selectedValue.value());
	}
}
