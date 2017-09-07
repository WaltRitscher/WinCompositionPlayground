using GalaSoft.MvvmLight.Messaging;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace WinComposition.Playground.Views.Effects
{
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class BlurPage : Page
  {
    public BlurPage()
    {
      this.InitializeComponent();
      this.Loaded += Page_Loaded; ;
    }
    private Compositor _compositor;
    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
      Messenger.Default.Send(new ChildPageLoadedMessage());
      SetupVisual();
    }

    public void SetupVisual()
    {

      _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
      var myVisual = ElementCompositionPreview.GetElementChildVisual(RedRobot);
      //myVisual.Size = new System.Numerics.Vector2(x: (float) RedRobot.ActualHeight,
      //                                            y: (float) RedRobot.ActualWidth);

      // myVisual.Offset = new System.Numerics.Vector3(30, 30, 0); ;
      // create a blue drop shadow
      //var s = new CompositionEffectBrush

      // .CreateDropShadow is part of the Win2D.uwp NuGet package
      // need to add a reference to 

      GaussianBlurEffect blurEffect = new GaussianBlurEffect()
      {
        Name = "Blur",
        BlurAmount = 10.0f, // You can place your blur amount here.
        BorderMode = EffectBorderMode.Hard,
        Optimization = EffectOptimization.Balanced,
        Source = new CompositionEffectSourceParameter("source")
      };

      var effectFactory = _compositor.CreateEffectFactory(graphicsEffect: blurEffect,
                                                          animatableProperties: new[] { nameof(GaussianBlurEffect.BlurAmount) });
      var effectBrush = effectFactory.CreateBrush();
      var surfaceBrush = _compositor.CreateSurfaceBrush();
      effectBrush.SetSourceParameter(name: "source", source: _compositor.CreateBackdropBrush());


      // https://stackoverflow.com/questions/46052953/uwp-create-shadow-in-xaml
      // https://stackoverflow.com/questions/36276856/uwp-app-realtime-blur-background-using-dx-compositor/36441888
      //https://stackoverflow.com/questions/40035604/how-to-apply-a-simple-blur-effect
      //var shadow = _compositor.CreateDropShadow()
      //shadow.Offset = new System.Numerics.Vector3(30, 30, 0);
      //shadow.Color = Colors.Blue;
      //myVisual.Shadow = shadow;

      // render on page
      ElementCompositionPreview.SetElementChildVisual(this, myVisual);
    }
  }
}
