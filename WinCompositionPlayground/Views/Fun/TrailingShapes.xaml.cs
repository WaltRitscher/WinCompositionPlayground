using GalaSoft.MvvmLight.Messaging;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WinComposition.Playground.Views.Fun
{
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class TrailingShapes : Page
  {
    private Compositor _compositor;

    private float _offsetBlue = 1;
    private DispatcherTimer _timer = new DispatcherTimer();
    ContainerVisual _mainContainer;
    private PointerPoint _point;
    private Random _random = new Random();

    public TrailingShapes()
    {
      this.InitializeComponent();
      this.Loaded += MainPage_Loaded;

      this.PointerMoved += MainPage_PointerMoved;
    }
    private void MainPage_PointerMoved(object sender, PointerRoutedEventArgs e)
    {
      _point = e.GetCurrentPoint(this);
      var x = Colors.AliceBlue;

      CreateSprite(Color.FromArgb(0x30, 0x00, 0x90, 0x00));

      

    }
    private void MainPage_Loaded(object sender, RoutedEventArgs e)
    {
      Messenger.Default.Send(new ChildPageLoadedMessage());
      _compositor = new Compositor();
      var root = ElementCompositionPreview.GetElementVisual(MainGrid);
      _compositor = root.Compositor;

      _mainContainer = _compositor.CreateContainerVisual();
      ElementCompositionPreview.SetElementChildVisual(MainGrid, _mainContainer);

     
    }
    private int _offsetRed = 45;
    private int _offsetGreen = 120;
    private void CreateSprite(Color selectedColor)
    {
      selectedColor = CreateNextColor(selectedColor);
      var colorVisual = _compositor.CreateSpriteVisual();
      colorVisual.Brush = _compositor.CreateColorBrush(selectedColor);
      colorVisual.CenterPoint = new Vector3(colorVisual.Size.X / 2, colorVisual.Size.Y / 2, 0);
      var clip = _compositor.CreateInsetClip();




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


      var batch = _compositor.CreateScopedBatch(CompositionBatchTypes.Animation);

      var sizeAnimation = _compositor.CreateScalarKeyFrameAnimation();
      var easingAnimation2 = _compositor.CreateCubicBezierEasingFunction(new Vector2(0.1f, 0.9f), new Vector2(0.6f, 1f));
      sizeAnimation.InsertKeyFrame(0.0f, 0.00f, easingAnimation);
      sizeAnimation.InsertKeyFrame(0.5f, 60.00f);
      sizeAnimation.InsertKeyFrame(1.0f, 0.00f);
      sizeAnimation.Duration = TimeSpan.FromMilliseconds(4000);
      sizeAnimation.IterationBehavior = AnimationIterationBehavior.Count;
      // sizeAnimation.IterationCount = 4;


      colorVisual.StartAnimation("Size.X", sizeAnimation);
      colorVisual.StartAnimation("Size.Y", sizeAnimation);
      batch.Completed += Batch_Completed;


      batch.End();
      _mainContainer.Children.InsertAtTop(colorVisual);
    }

    private Color CreateNextColor(Color selectedColor)
    {
      _offsetBlue += 1;
      _offsetRed += 2;
      _offsetGreen += 3;
      selectedColor.B = (byte)_offsetBlue;
      selectedColor.R = (byte)_offsetRed;
      selectedColor.G = (byte)_offsetGreen;
      return selectedColor;
    }

    private void Batch_Completed(object sender, CompositionBatchCompletedEventArgs args)
    {
      _mainContainer.Children.Remove(_mainContainer.Children.First());
    }
  }
}
