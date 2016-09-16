using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace WinComposition.Playground.Views {
	// https://msdn.microsoft.com/en-us/magazine/mt590968

	// Created by Walt Ritscher
	// https://www.linkedin.com/in/waltritscher
	// I'm a staff author at Lynda.com, part of the LinkedIn family. 
	// My content team produces hundreds of technical training courses for software developers each year.

	// Lynda.com has over 4200 courses (Topics include IT (dev, devops, web), creative, business, photography, music and lots of documentaries).
	// Get your free trial at http://lynda.com
	// My courses are at http://lynda.com/waltritscher
	// 
	public sealed partial class CheckerboardSpin : Page {
		private ObservableCollection<Rectangle> _squares = new ObservableCollection<Rectangle>();

		public CheckerboardSpin() {
			this.InitializeComponent();

			this.Loaded += Checkerboard_Loaded;
		}

		private async void Checkerboard_Loaded(object sender, RoutedEventArgs e) {
			await SetupSquares();
			Messenger.Default.Send(new ChildPageLoadedMessage());
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
				//visual.StartAnimation("Opacity", fadeAnimation);

				var animation2 = compositor.CreateScalarKeyFrameAnimation();
				animation2.InsertKeyFrame(0.0f, 0.0f);
				animation2.InsertKeyFrame(0.2f, (float)ran.NextDouble() * 3.18F);
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