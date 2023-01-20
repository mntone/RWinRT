#pragma once
#include "MainWindow.g.h"

//#define USE_RESOURCE_CACHE // <- if you need on-memory cache
#include <res.g.h>

namespace winrt::RWinRT::CppApp::implementation {

	struct MainWindow: MainWindowT<MainWindow> {
		MainWindow();

		void OnMessageButtonClick(Windows::Foundation::IInspectable const& sender, Microsoft::UI::Xaml::RoutedEventArgs const& args);
		void OnSelectionChanged(Windows::Foundation::IInspectable const& sender, Microsoft::UI::Xaml::Controls::SelectionChangedEventArgs const& args);

	private:
		int i { 0 };
		ResourceManager<R> resourceManager_;
		ResourceContext<R> resourceContext_ { resourceManager_.Context(L"ja-JP") };
	};

}
