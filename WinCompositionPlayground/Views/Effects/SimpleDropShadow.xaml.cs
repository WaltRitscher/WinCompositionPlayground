using GalaSoft.MvvmLight.Messaging;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WinComposition.Playground.Views.Effects
{
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class SimpleDropShadow : Page
  {
    private Compositor _compositor;
    public SimpleDropShadow()
    {
      this.InitializeComponent();
      this.Loaded += Batches_Loaded;
    }

    private void Batches_Loaded(object sender, RoutedEventArgs e)
    {
      Messenger.Default.Send(new ChildPageLoadedMessage());
      SetupVisual();
    }

    internal void SetupVisual()
    {
      _compositor = ElementCompositionPreview.GetElementVisual(RedRobot).Compositor;

     // var myVisual = ElementCompositionPreview.GetElementVisual(RedRobot);

      // create a blue drop shadow
      var shadow = _compositor.CreateDropShadow();
      var x = _compositor.CreateSpriteVisual();
      x.Size = new System.Numerics.Vector2((float)RedRobot.ActualWidth, (float)RedRobot.ActualHeight);
      x.Size = new System.Numerics.Vector2((float)100, (float)100);
      shadow.Offset = new System.Numerics.Vector3(30, 30, 0);
      shadow.Color = Colors.DarkBlue;
      x.Shadow = shadow;

      // render on page
      ElementCompositionPreview.SetElementChildVisual(this, x);
    }



  }
}
