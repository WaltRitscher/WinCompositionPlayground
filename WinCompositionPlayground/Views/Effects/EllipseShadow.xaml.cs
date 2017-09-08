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
  public sealed partial class EllipseShadow : Page
  {
    public EllipseShadow()
    {
      this.InitializeComponent();
      this.Loaded += Page_Loaded;
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
      Messenger.Default.Send(new ChildPageLoadedMessage());
      SetupVisual();
    }

    private void SetupVisual()
    {
      var hostVisual = ElementCompositionPreview.GetElementVisual(element: ShadowHost);
      var compositor = hostVisual.Compositor;

      // Create a drop shadow
      DropShadow dropShadow = compositor.CreateDropShadow();
      dropShadow.Color = Color.FromArgb(255, 22, 33, 44);
      dropShadow.BlurRadius = 15.0f;
      dropShadow.Offset = new Vector3(12.5f, 2.5f, 0.0f);
      // Associate the shape of the shadow with the shape of the target element
      dropShadow.Mask = CircleImage.GetAlphaMask();

      // Create a Visual to hold the shadow
      SpriteVisual shadowVisual = compositor.CreateSpriteVisual();
      shadowVisual.Shadow = dropShadow;

      // Add the shadow as a child of the host (Ellipse) in the visual tree
      ElementCompositionPreview.SetElementChildVisual(element: ShadowHost,
                                                      visual: shadowVisual);

      // Ensure the size of shadow host and shadow visual always stay in sync
      ExpressionAnimation bindSizeAnimation = compositor.CreateExpressionAnimation(expression: "hostVisual.Size");
      bindSizeAnimation.SetReferenceParameter(key: "hostVisual", compositionObject: hostVisual);

      shadowVisual.StartAnimation(propertyName: nameof(shadowVisual.Size),
                                  animation: bindSizeAnimation);
    }
  }
}
