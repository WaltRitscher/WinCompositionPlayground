using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WinComposition.Playground.Views.Animation
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class Batches : Page
	{

		// inspiration from Composition samples
		// https://docs.microsoft.com/en-us/uwp/api/windows.ui.composition.compositionscopedbatch

		// details about animations
		// https://docs.microsoft.com/en-us/windows/uwp/composition/composition-animation
		private Compositor _compositor;

		private Visual _greenVisual;
		private Visual _redVisual;
		private Visual _blueVisual;

		public Batches()
		{
			this.InitializeComponent();
			this.Loaded += Batches_Loaded;
		}

		private void Batches_Loaded(object sender, RoutedEventArgs e)
		{
			Messenger.Default.Send(new ChildPageLoadedMessage());
			SetupVisual();
		}

		private void AppBarButton_Click(object sender, RoutedEventArgs e)
		{
			MoveAnimation(_greenVisual);
		}

		private void SetupVisual()
		{
			_compositor = ElementCompositionPreview.GetElementVisual(MainGrid).Compositor; 

			_greenVisual = ElementCompositionPreview.GetElementVisual(GreenRobot);
			_redVisual = ElementCompositionPreview.GetElementVisual(RedRobot);
			_blueVisual = ElementCompositionPreview.GetElementVisual(BlueRobot);



		}
		public void MoveAnimation(Visual targetVisual)
		{
			var moveAnim = _compositor.CreateVector3KeyFrameAnimation();

			moveAnim.InsertKeyFrame(0.2f, new Vector3(0.0f, 50, 0.0f));
			moveAnim.InsertKeyFrame(0.4f, new Vector3(50.0f, 50, 0.0f));
			moveAnim.InsertKeyFrame(0.7f, new Vector3(50.0f, 0, 0.0f));
			moveAnim.InsertKeyFrame(1.0f, new Vector3(0f, 0f, 0.0f));
			moveAnim.Duration = TimeSpan.FromMilliseconds(2000);

			targetVisual.StartAnimation("Offset", moveAnim);
		}

		public void RotateAnimation(Visual targetVisual)
		{
			var rotateAnim = _compositor.CreateScalarKeyFrameAnimation();
			var easing = _compositor.CreateLinearEasingFunction();
			targetVisual.CenterPoint = new Vector3((float)(RedRobot.RenderSize.Width * 0.5f), (float)(this.RedRobot.RenderSize.Height * 0.5f), -10.0f);
			;
			rotateAnim.InsertKeyFrame(0.00f, 0.00f, easing);
			rotateAnim.InsertKeyFrame(0.1f, 20, easing);
			rotateAnim.InsertKeyFrame(0.2f, -20, easing);
			rotateAnim.InsertKeyFrame(0.3f, 10, easing);
			rotateAnim.InsertKeyFrame(.4f, -10f, easing);


			rotateAnim.InsertKeyFrame(0.6f, 05, easing);
			rotateAnim.InsertKeyFrame(0.7f, -05, easing);
			rotateAnim.InsertKeyFrame(0.8f, 2, easing);
			rotateAnim.InsertKeyFrame(0.0f, -1, easing);
			rotateAnim.InsertKeyFrame(1.00f, 0f, easing);


			rotateAnim.Duration = TimeSpan.FromMilliseconds(4000);
			//rotateAnim.IterationCount = 2;
		
			targetVisual.StartAnimation("RotationAngleinDegrees", rotateAnim);
		}

	
	public void GrowAnimation(Visual targetVisual)
	{
		var growAnim = _compositor.CreateVector3KeyFrameAnimation();

		growAnim.InsertKeyFrame(0.6f, new Vector3(2f, 2f, 2f));
		growAnim.InsertKeyFrame(1.0f, new Vector3(1f, 1f, 1f));
		growAnim.Duration = TimeSpan.FromMilliseconds(2000);

		targetVisual.StartAnimation("Scale", growAnim);
	}

	private void RotateButton_Click(object sender, RoutedEventArgs e)
		{
			RotateAnimation(_redVisual);
		}

		private void GrowButton_Click(object sender, RoutedEventArgs e)
		{
			GrowAnimation(_blueVisual);
		}
	}
}
