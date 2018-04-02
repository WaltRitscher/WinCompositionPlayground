using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using WinComposition.Playground.ViewModels;
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
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WinComposition.Playground.Views
{
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class BoundGridSelectionChanged : Page
  {
    public BoundGridSelectionChanged()
    {
      this.InitializeComponent();
      this.DataContext = new SensorsViewModel();
      this.Loaded += Page_Loaded;

      DataContextChanged += (s, e) =>
      {
        ViewModel = DataContext as SensorsViewModel;
      };


      

      //this.PointerReleased += CheckerboardGet_PointerReleased;
      MainGrid.SelectionChanged += MainGrid_SelectionChanged;
     

    }

    private void MainGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      var item = MainGrid.SelectedItem;
      var itemContainer = MainGrid.ContainerFromItem(item);
      Visual visual;

      Rectangle rect = FindElementInVisualTree<Rectangle>(itemContainer);
      visual = ElementCompositionPreview.GetElementVisual(rect);


      var growAnim = visual.Compositor.CreateVector3KeyFrameAnimation();

      growAnim.InsertKeyFrame(0.6f, new Vector3(x: 6f, y: 4f, z: 4f));
      growAnim.InsertKeyFrame(1.0f, new Vector3(x: 1f, y: 1f, z: 1f));
      growAnim.Duration = TimeSpan.FromMilliseconds(2000);

      visual.StartAnimation("Scale", growAnim);
    }

  

    private T FindElementInVisualTree<T>(DependencyObject parentElement) where T : DependencyObject
    {
      var count = VisualTreeHelper.GetChildrenCount(parentElement);
      if (count == 0) return null;

      for (int i = 0; i < count; i++)
      {
        var child = VisualTreeHelper.GetChild(parentElement, i);
        if (child != null && child is T)
          return (T)child;
        else
        {
          var result = FindElementInVisualTree<T>(child);
          if (result != null)
            return result;
        }
      }
      return null;
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {




      Messenger.Default.Send(new ChildPageLoadedMessage());
      MainGrid.Visibility = Visibility.Visible;
     
     
    }

    public SensorsViewModel ViewModel { get; set; }

   
  }
}
