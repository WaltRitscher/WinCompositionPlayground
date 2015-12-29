using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WinComposition.Playground;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WinComposition.Playground {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class TestPage : Page {
		public TestPage() {
			this.InitializeComponent();

			this.Loaded += TestPage_Loaded;
		}

		private void TestPage_Loaded(object sender, RoutedEventArgs e) {
			Compositor compositor = CreateCompositorFromGrid();
			ContainerVisual mainContainer = CreateContainerFromGrid(compositor);

			var gen = new SpriteGen(compositor);

			mainContainer.Children.InsertAtTop(gen.FirstSprite(Colors.Blue));

		}

		

		private Compositor CreateCompositorFromGrid() {
			var compositor = new Compositor();
			var root = ElementCompositionPreview.GetElementVisual(MainGrid);
			compositor = root.Compositor;
			return compositor;
		}

		private ContainerVisual CreateContainerFromGrid(Compositor compositor) {
			var mainContainer = compositor.CreateContainerVisual();
			ElementCompositionPreview.SetElementChildVisual(MainGrid, mainContainer);
			return mainContainer;
		}
	}
}
