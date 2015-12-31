using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

namespace WinComposition.Playground.Views {

	public sealed partial class Checkerboard2 : Page {
		public Checkerboard2() {
			this.InitializeComponent();
			this.DataContext = SetupSquares(3000);
		}
		

		private ObservableCollection<Rectangle> SetupSquares(int count) {
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
