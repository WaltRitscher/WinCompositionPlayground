using GalaSoft.MvvmLight.Messaging;
using System;
using System.Threading.Tasks;
using WinComposition.Playground.Models;
using WinComposition.Playground.ViewModels;
using Windows.UI.Core;
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
            Messenger.Default.Register<ChildPageLoadedMessage>(this, ChildPageLoaded);
            this.Loaded += DemoViewer_Loaded;
            TocListView.DataContext = new DemosViewModel();
            TocListView.SelectionChanged += TocListView_SelectionChanged;
            DemoFrame.Navigated += DemoFrame_Navigated;
        }



        private async void TocListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {



            ProgressGrid.Visibility = Visibility.Visible;
            ProgressRing1.IsActive = true;


            var demoItem = TocListView.SelectedItem as DemoItem;
            if (demoItem != null)
            {
                Uri uri = ReadMeWebView.BuildLocalStreamUri("someTag", demoItem.DocPath);
                StreamUriWinRTResolver resolver = new StreamUriWinRTResolver();
                ReadMeWebView.NavigateToLocalStreamUri(uri, resolver);
                await Dispatcher.RunAsync(CoreDispatcherPriority.Low, ChangeTextCallBack);


            }
        }

        public void ChangeTextCallBack() {
            var demoItem = TocListView.SelectedItem as DemoItem;
            DemoFrame.Navigate(demoItem.DemoPageType);
        }

        private void DemoFrame_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e) {

            DemoFrame.BackStack.Clear();
        }

        private void DemoViewer_Loaded(object sender, RoutedEventArgs e) {
            Uri uri = ReadMeWebView.BuildLocalStreamUri("someTag", "/Docs/Checkerboard1.html");
            StreamUriWinRTResolver resolver = new StreamUriWinRTResolver();
            ReadMeWebView.NavigateToLocalStreamUri(uri, resolver);
            DemoFrame.Navigate(typeof(Views.Start));


        }

        private void ChildPageLoaded(ChildPageLoadedMessage msg) {
            ProgressGrid.Visibility = Visibility.Collapsed;
            ProgressRing1.IsActive = false;



        }


    }
}