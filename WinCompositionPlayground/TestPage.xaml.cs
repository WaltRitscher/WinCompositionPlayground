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



// Created by Walt Ritscher
// https://www.linkedin.com/in/waltritscher
// I'm a staff author at Lynda.com, part of the LinkedIn family. 
// My content team produces hundreds of technical training courses for software developers each year.

// Lynda.com has over 4200 courses (Topics include IT (dev, devops, web), creative, business, photography, music and lots of documentaries).
// Get your free trial at http://lynda.com
// My courses are at http://lynda.com/waltritscher
// 

namespace WinComposition.Playground {

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
