using GalaSoft.MvvmLight.Messaging;
using System;
using WinComposition.Playground.Models;
using WinComposition.Playground.ViewModels;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WinComposition.Playground
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class DemoViewer : Page
	{
		public DemoViewer()
		{
			this.InitializeComponent();
			Messenger.Default.Register<ChildPageLoadedMessage>(this, ChildPageLoaded);
			this.Loaded += DemoViewer_Loaded;
			TocListView.DataContext = new DemosViewModel();
			TocListView.SelectionChanged += TocListView_SelectionChanged;
			DemoFrame.Navigated += DemoFrame_Navigated;
      PivotMain.SelectionChanged += PivotMain_SelectionChanged;
     
		}

    private void PivotMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (true)
      {


      }
    }

    private async void TocListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      ShowProgressUI(showUI: true);

      var demoItem = TocListView.SelectedItem as DemoItem;
      if (demoItem != null)
      {
        Uri uri = ReadMeWebView.BuildLocalStreamUri("someTag", demoItem.DocPath);
        var resolver = new StreamUriWinRTResolver();
        ReadMeWebView.NavigateToLocalStreamUri(uri, resolver);
        await Dispatcher.RunAsync(CoreDispatcherPriority.Low, NavigateToDemoPage);
        //NavigateToDemoPage

      }
    }

    private void ShowProgressUI(bool showUI)
    {
      if (showUI)
      {
        ProgressGrid.Visibility = Visibility.Visible;
        ProgressRing1.IsActive = true;
      }
      else {
        ProgressGrid.Visibility = Visibility.Collapsed;
        ProgressRing1.IsActive = false;
      }
     
    }

    // https://stackoverflow.com/questions/36326032/uwp-exclude-navigation-from-back-button-stack
    public void NavigateToDemoPage()
		{
			var demoItem = TocListView.SelectedItem as DemoItem;
			DemoFrame.Navigate(demoItem.DemoPageType);

		}

		private void DemoFrame_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
		{
			//DemoFrame.BackStack.Clear();
     
		}

		private void DemoViewer_Loaded(object sender, RoutedEventArgs e)
		{
			//Uri uri = ReadMeWebView.BuildLocalStreamUri("someTag", "/Docs/Checkerboard1.html");
			//var resolver = new StreamUriWinRTResolver();
			//ReadMeWebView.NavigateToLocalStreamUri(uri, resolver);
			DemoFrame.Navigate(typeof(Views.StartHere));
		}

		private void ChildPageLoaded(ChildPageLoadedMessage msg)
		{
      ShowProgressUI(showUI: false);

    }
	}
}