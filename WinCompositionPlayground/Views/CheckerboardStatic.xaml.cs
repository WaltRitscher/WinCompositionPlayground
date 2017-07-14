using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using WinComposition.Playground.Models;
using WinComposition.Playground.ViewModels;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace WinComposition.Playground.Views
{
	public sealed partial class CheckerboardStatic : Page
	{
		public CheckerboardStatic()
		{
			this.InitializeComponent();

			//var datasource = new DemoDataSource();
			this.DataContext = new SensorsViewModel();
			//this.DataContext = SetupSquares(3000);
			this.Loaded += Page_Loaded;

			DataContextChanged += (s, e) =>
				{
					CurrentViewModel = DataContext as SensorsViewModel;
				};
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			Messenger.Default.Send(new ChildPageLoadedMessage());
			MainGrid.Visibility = Visibility.Visible;
		}

		public SensorsViewModel CurrentViewModel { get; set; }

		private ObservableCollection<Rectangle> SetupSquares(int count)
		{
			var squares = new ObservableCollection<Rectangle>();
			Rectangle square;
			Random ran = new Random();
			for (int counter = 0; counter < count; counter++)
			{
				square = new Rectangle
				{
					Width = 20,
					Height = 20,
					Opacity = .5,
					Fill = new SolidColorBrush(Colors.LightBlue)
				};
				if (counter % 3 == 0)
				{
					square.Fill = new SolidColorBrush(Colors.Orange);
				}
				if (counter % 3 == 1)
				{
					square.Fill = new SolidColorBrush(Colors.Purple);
				}
				squares.Add(square);
			}
			return squares;
		}
	}
}