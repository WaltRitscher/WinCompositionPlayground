using GalaSoft.MvvmLight.Messaging;
using WinComposition.Playground.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WinComposition.Playground.Views
{
	public sealed partial class CheckerboardStatic : Page
	{
		public CheckerboardStatic()
		{
			this.InitializeComponent();

			this.DataContext = new SensorsViewModel();
			this.Loaded += Page_Loaded;

			DataContextChanged += (s, e) =>
				{
					ViewModel = DataContext as SensorsViewModel;
				};
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			Messenger.Default.Send(new ChildPageLoadedMessage());
			MainGrid.Visibility = Visibility.Visible;
		}

		public SensorsViewModel ViewModel { get; set; }
	}
}