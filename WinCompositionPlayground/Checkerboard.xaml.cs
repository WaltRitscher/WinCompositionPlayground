using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WinComposition.Playground {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class Checkerboard : Page {

		private ObservableCollection<Rectangle> _squares = new ObservableCollection<Rectangle>();
		public Checkerboard() {
			this.InitializeComponent();

		

			this.Loaded += Checkerboard_Loaded;

		}

		private async void Checkerboard_Loaded(object sender, RoutedEventArgs e) {
		await SetupSquares();
		}

		private async Task SetupSquares() {
			var x = DisplayInformation.GetForCurrentView();
			var width = x.RawDpiX;
		
			Compositor compositor;
			Visual visual;
			Rectangle square;
			Random ran = new Random();
			for (int counter = 0; counter < 3000; counter++)
			{
				square = new Rectangle
				{
					Width = 20,
					Height = 20,
					Opacity = .5,
					Fill = new SolidColorBrush(Colors.LightBlue)
				};
				if (counter % 2 == 0)
				{
					square.Fill = new SolidColorBrush(Colors.Orange);
				}
				if (counter % 3 == 0)
				{
					square.Fill = new SolidColorBrush(Colors.Purple);
				}
				_squares.Add(square);

				visual = ElementCompositionPreview.GetElementVisual(square);

				compositor = visual.Compositor;
				var fadeAnimation = compositor.CreateScalarKeyFrameAnimation();
				fadeAnimation.InsertKeyFrame(0.0f, 0.0f);
				fadeAnimation.InsertKeyFrame(0.5f, 1.00f);
				fadeAnimation.InsertKeyFrame(1.0f, 0.00f);
				fadeAnimation.Duration = TimeSpan.FromMilliseconds(ran.Next(8000, 24000));
				fadeAnimation.DelayTime = TimeSpan.FromMilliseconds(ran.Next(0, 2000));


				fadeAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
				visual.StartAnimation("Opacity", fadeAnimation);


				var animation2 = compositor.CreateScalarKeyFrameAnimation();
				animation2.InsertKeyFrame(0.0f, 0.0f);
				animation2.InsertKeyFrame(0.2f, (float)ran.NextDouble()* 3.18F);
				animation2.InsertKeyFrame(0.80f, 6.24f);
					animation2.Duration = TimeSpan.FromMilliseconds(ran.Next(7000, 15000));
			//	animation2.Duration = TimeSpan.FromMilliseconds(7000);
				animation2.DelayTime = TimeSpan.FromMilliseconds(ran.Next(0, 2000));


				animation2.IterationBehavior = AnimationIterationBehavior.Forever;
				visual.StartAnimation("RotationAngle", animation2);
			}


			this.DataContext = _squares;
			
		}
	}
}
