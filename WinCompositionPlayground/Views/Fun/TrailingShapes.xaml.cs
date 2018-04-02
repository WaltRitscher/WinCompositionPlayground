using GalaSoft.MvvmLight.Messaging;
using System;
using System.Linq;
using System.Numerics;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WinComposition.Playground.Views.Fun
{
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class TrailingShapes : Page
  {
    private Compositor _compositor;

    private ContainerVisual _mainContainer;
    private PointerPoint _point;
    private Random _random = new Random();
    private float _offsetBlue = 1;
    private int _offsetRed = 45;
    private int _offsetGreen = 120;

    public TrailingShapes()
    {
      this.InitializeComponent();
      this.Loaded += MainPage_Loaded;
      this.PointerMoved += MainPage_PointerMoved;
    }

    private void MainPage_PointerMoved(object sender, PointerRoutedEventArgs e)
    {
      _point = e.GetCurrentPoint(this);
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

    private void CreateSprite(Color selectedColor)
    {
      SpriteVisual colorVisual = CreateVisual(selectedColor);
      var opacityAnimation = CreateOpacityAnimation(colorVisual);
      //if (_offsetBlue % 20 != 0)
      //{
      //  colorVisual.StartAnimation("Opacity", opacityAnimation);
      //}

      colorVisual.StartAnimation(nameof(colorVisual.Opacity), opacityAnimation);

      #region Rotation animation

      //rotation animation
      var rotationAnimation = CreateRotateAnimation();
      colorVisual.StartAnimation(nameof(colorVisual.RotationAngleInDegrees), rotationAnimation);

      #endregion Rotation animation

      var easing2 = _compositor.CreateCubicBezierEasingFunction(new Vector2(0.1f, 0.9f), new Vector2(0.6f, 1f));

      var batch = _compositor.CreateScopedBatch(CompositionBatchTypes.Animation);

      var sizeAnimation = CreateSizeAnimation();

      colorVisual.StartAnimation("Size.X", sizeAnimation);
      colorVisual.StartAnimation("Size.Y", sizeAnimation);

      batch.Completed += Batch_Completed;

      batch.End();
      _mainContainer.Children.InsertAtTop(colorVisual);
    }
    private SpriteVisual CreateVisual(Color selectedColor)
    {
      var colorVisual = _compositor.CreateSpriteVisual();
      colorVisual.Brush = _compositor.CreateColorBrush(CreateNextColor(selectedColor));
      colorVisual.CenterPoint = new Vector3(colorVisual.Size.X / 2, colorVisual.Size.Y / 2, 0);

      colorVisual.Size = new Vector2(120.0f, 120.0f);
      colorVisual.Offset = new Vector3((float)_point.Position.X, (float)_point.Position.Y, 0.0f);
      colorVisual.Opacity = 0.7f;
      return colorVisual;
    }

    private Color CreateNextColor(Color selectedColor)
    {
       _offsetBlue += 7;
      if (_offsetBlue % 255==0)
      {
        _offsetRed += 14;
      }
      if (_offsetRed % 255 == 0)
      {
        _offsetGreen += 21;
      }
    
     
     
      selectedColor.B = (byte)_offsetBlue;
      selectedColor.R = (byte)_offsetRed;
      selectedColor.G = (byte)_offsetGreen;
      return selectedColor;
    }
    private ScalarKeyFrameAnimation CreateSizeAnimation()
    {
      var easing1 = _compositor.CreateCubicBezierEasingFunction(new Vector2(0.9f, 0.1f), new Vector2(0.1f, 1f));
      var sizeAnimation = _compositor.CreateScalarKeyFrameAnimation();
      sizeAnimation.InsertKeyFrame(0.0f, 0.00f, easing1);
      sizeAnimation.InsertKeyFrame(0.5f, 60.00f);
      sizeAnimation.InsertKeyFrame(1.0f, 0.00f, easing1);
      sizeAnimation.Duration = TimeSpan.FromMilliseconds(4000);
      // sizeAnimation.IterationBehavior = AnimationIterationBehavior.Count;
      // sizeAnimation.IterationCount = 4;

      return sizeAnimation;
    }

    private ScalarKeyFrameAnimation CreateOpacityAnimation(SpriteVisual colorVisual)
    {
      var opacityAnimation = _compositor.CreateScalarKeyFrameAnimation();

      opacityAnimation.InsertKeyFrame(0.0f, .70f);
      opacityAnimation.InsertKeyFrame(1.0f, 0.00f);

      opacityAnimation.Duration = TimeSpan.FromMilliseconds(_random.Next(2000, 4000));

      return opacityAnimation;
    }

   

    private void Batch_Completed(object sender, CompositionBatchCompletedEventArgs args)
    {
      _mainContainer.Children.Remove(_mainContainer.Children.First());
    }

    public ScalarKeyFrameAnimation CreateRotateAnimation()
    {
      var currentAnimation = _compositor.CreateScalarKeyFrameAnimation();
      var easingAnimation = _compositor.CreateCubicBezierEasingFunction(new Vector2(0.9f, 0.1f), new Vector2(0.1f, 1f));
      currentAnimation.InsertKeyFrame(0.0f, (float)_random.Next(-50, 50), easingAnimation);
      currentAnimation.InsertKeyFrame(1.0f, (float)_random.Next(-90, 90));
      currentAnimation.Duration = TimeSpan.FromMilliseconds(_random.Next(6000, 9000));
      return currentAnimation;
    }
  }
}