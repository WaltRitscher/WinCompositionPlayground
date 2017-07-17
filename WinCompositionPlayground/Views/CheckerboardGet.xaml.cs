using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WinComposition.Playground.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WinComposition.Playground.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class CheckerboardGet : Page
	{
		public CheckerboardGet()
		{
			this.InitializeComponent();
			this.DataContext = new SensorsViewModel();
			this.Loaded += Page_Loaded;

			DataContextChanged += (s, e) =>
			{
				ViewModel = DataContext as SensorsViewModel;
			};

			Compositor compositor;
			Visual visual;

			var q = from c in MainGrid.Items
							select c;
			var result = q.ToList();
			
		}
		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			Messenger.Default.Send(new ChildPageLoadedMessage());
			MainGrid.Visibility = Visibility.Visible;
		}

		public SensorsViewModel ViewModel { get; set; }
	}
}
