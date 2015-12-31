using System;
using WinComposition.Playground.Models;
using WinComposition.Playground.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WinComposition.Playground {

	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class DemoViewer : Page {

		public DemoViewer() {
			this.InitializeComponent();
			this.Loaded += DemoViewer_Loaded;
			TocListView.DataContext = new DemosViewModel();
			TocListView.SelectionChanged += TocListView_SelectionChanged;
		}

		private void TocListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			var demoItem = TocListView.SelectedItem as DemoItem;
			if (demoItem != null)
			{
				Uri uri = ReadMeWebView.BuildLocalStreamUri("someTag", demoItem.DocPath);
				StreamUriWinRTResolver resolver = new StreamUriWinRTResolver();
				ReadMeWebView.NavigateToLocalStreamUri(uri, resolver);

				DemoFrame.Navigate(demoItem.DemoPageType);
			}
		}

		private void DemoViewer_Loaded(object sender, RoutedEventArgs e) {
			Uri uri = ReadMeWebView.BuildLocalStreamUri("someTag", "/Docs/Checkerboard1.html");
			StreamUriWinRTResolver resolver = new StreamUriWinRTResolver();
			ReadMeWebView.NavigateToLocalStreamUri(uri, resolver);

			ReadMeWebView.NavigationFailed += ReadMeWebView_NavigationFailed;
		}

		private void ReadMeWebView_NavigationFailed(object sender, WebViewNavigationFailedEventArgs e) {
		}
	}
}