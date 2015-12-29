using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WinComposition.Playground {

	public sealed partial class MainPage : Page {

		private Compositor _compositor;
	
		private float _offsetBlue = 1;
		private DispatcherTimer _timer = new DispatcherTimer();
		ContainerVisual _mainContainer;
		private PointerPoint _point;
		private Random _random = new Random();

		public MainPage() {
			this.InitializeComponent();
			this.Loaded += MainPage_Loaded;

			this.PointerMoved += MainPage_PointerMoved;
		}
		private void MainPage_PointerMoved(object sender, PointerRoutedEventArgs e) {
			_point = e.GetCurrentPoint(this);
			var x = Colors.AliceBlue;

			CreateSprite(Color.FromArgb(0x30, 0x00, 0x90, 0x00));

			if (_mainContainer.Children.Count > 400)
			{
				_mainContainer.Children.Remove(_mainContainer.Children.First());
			}

		}
		private void MainPage_Loaded(object sender, RoutedEventArgs e) {

			_compositor = new Compositor();
			 var root = ElementCompositionPreview.GetElementVisual(MainGrid);
			_compositor = root.Compositor;

			_mainContainer = _compositor.CreateContainerVisual();
			ElementCompositionPreview.SetElementChildVisual(MainGrid, _mainContainer);

			// CreateSprite(Color.FromArgb(0x50, 0x00, 0x20, 0x90));
		}
		private int _offsetRed = 45;
		private int _offsetGreen = 120;
		private void CreateSprite(Color selectedColor) {
			_offsetBlue += 1;
			_offsetRed += 2;
			_offsetGreen += 3;
			selectedColor.B = (byte)_offsetBlue;
			selectedColor.R = (byte)_offsetRed;
			selectedColor.G = (byte)_offsetGreen;
			var colorVisual = _compositor.CreateSpriteVisual();
			colorVisual.Brush = _compositor.CreateColorBrush(selectedColor);
			colorVisual.CenterPoint = new Vector3(colorVisual.Size.X / 2, colorVisual.Size.Y / 2, 0);
			var clip = _compositor.CreateInsetClip();

			//clip.TopInset = 30.6f;
			// clip.BottomInset = 30.6f;
			colorVisual.Clip = clip;

			// colorVisual.RotationAngleInDegrees = 45f;
			//  ElementCompositionPreview.



			colorVisual.Size = new Vector2(120.0f, 120.0f);
			colorVisual.Offset = new Vector3((float)_point.Position.X, (float)_point.Position.Y, 0.0f);
			colorVisual.Opacity = 0.7f;

			var animation = _compositor.CreateScalarKeyFrameAnimation();
			// Create two keyframes that define starting and ending value of the property

			animation.InsertKeyFrame(0.0f, .70f);
			animation.InsertKeyFrame(1.0f, 0.00f);

			animation.Duration = TimeSpan.FromMilliseconds(_random.Next(2000, 4000));

			if (_offsetBlue % 20 != 0)
			{
				colorVisual.StartAnimation("Opacity", animation);
			}
			var rotationAnimation = _compositor.CreateScalarKeyFrameAnimation();
			var easingAnimation = _compositor.CreateCubicBezierEasingFunction(new Vector2(0.9f, 0.1f), new Vector2(0.1f, 1f));
			rotationAnimation.InsertKeyFrame(0.0f, (float)_random.Next(-50, 50), easingAnimation);
			rotationAnimation.InsertKeyFrame(1.0f, (float)_random.Next(-90, 90));
			rotationAnimation.Duration = TimeSpan.FromMilliseconds(_random.Next(6000, 9000));

			colorVisual.StartAnimation("RotationAngleInDegrees", rotationAnimation);




			var sizeAnimation = _compositor.CreateScalarKeyFrameAnimation();
			var easingAnimation2 = _compositor.CreateCubicBezierEasingFunction(new Vector2(0.1f, 0.9f), new Vector2(0.6f, 1f));
			sizeAnimation.InsertKeyFrame(0.0f, 0.00f, easingAnimation);
			sizeAnimation.InsertKeyFrame(1.0f, 90.00f);
			sizeAnimation.Duration = TimeSpan.FromMilliseconds(4000);

			//var sizeAnimation2 = _compositor.CreateScalarKeyFrameAnimation();
			//var easingAnimation3 = _compositor.CreateCubicBezierEasingFunction(new Vector2(0.2f, 0.5f), new Vector2(0.6f, 1f));
			//sizeAnimation.InsertKeyFrame(0.0f, 0.00f, easingAnimation);
			//sizeAnimation.InsertKeyFrame(1.0f, 100.00f);
			//sizeAnimation.Duration = TimeSpan.FromMilliseconds(6000);

			colorVisual.StartAnimation("Size.X", sizeAnimation);
			colorVisual.StartAnimation("Size.Y", sizeAnimation);
		//	var a = colorVisual.GetPropertyName(() => colorVisual.Size.X);

			_mainContainer.Children.InsertAtTop(colorVisual);

			//var batch = _compositor.CreateScopedBatch(CompositionBatchTypes.Animation);
			//batch.Completed += Batch_Completed;


		}
		//private void Batch_Completed(object sender, CompositionBatchCompletedEventArgs args) {

		//}
	}
}
