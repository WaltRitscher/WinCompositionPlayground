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
      RedRobot.Loaded += Grid_Loaded;
      
    }

    private void Grid_Loaded(object sender, RoutedEventArgs e)
    {
      Messenger.Default.Send(new ChildPageLoadedMessage());
      SetupVisual();
    }

    internal void SetupVisual()
    {
      _compositor = ElementCompositionPreview.GetElementVisual(RedRobot).Compositor;

     
   
      var sprite = _compositor.CreateSpriteVisual();
      sprite.Size = new System.Numerics.Vector2((float)RedRobot.ActualWidth, (float)RedRobot.ActualHeight);

      var shadow = _compositor.CreateDropShadow();
      shadow.Offset = new System.Numerics.Vector3(3, 3, 0);
      shadow.Color = Colors.Red;
      var colorBrush = _compositor.CreateColorBrush(Colors.Green);
      sprite.Shadow = shadow;
      sprite.Brush = colorBrush;

    
      // render on page
      ElementCompositionPreview.SetElementChildVisual(RedRobot,sprite);
    }

    private void RedRobot_PointerMoved(object sender, PointerRoutedEventArgs e)
    {
      var x = RedRobot.ActualWidth;
    }
  }
}
