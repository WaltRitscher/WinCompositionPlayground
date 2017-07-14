﻿using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using WinComposition.Playground.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

namespace WinComposition.Playground.Views {

	public sealed partial class CheckerboardStatic : Page {
		public CheckerboardStatic() {
			this.InitializeComponent();

			var datasource = new DemoDataSource();
			this.DataContext = datasource.GetSensors();
			//this.DataContext = SetupSquares(3000);
			this.Loaded += Page_Loaded;
		}

		private void Page_Loaded(object sender, RoutedEventArgs e) {
			Messenger.Default.Send(new ChildPageLoadedMessage());
            MainGrid.Visibility = Visibility.Visible;
		}

		private ObservableCollection<Rectangle> SetupSquares(int count) {
			var squares = new ObservableCollection<Rectangle>();
			Rectangle square;
			Random ran = new Random();
			for (int counter = 0; counter < count; counter++)
			{
				square = new Rectangle
				{
					Width = 20,
					Height = 20,
					Opacity = .5,
					Fill = new SolidColorBrush(Colors.LightBlue)
				};
				if (counter % 3 == 0)
				{
					square.Fill = new SolidColorBrush(Colors.Orange);
				}
				if (counter % 3 == 1)
				{
					square.Fill = new SolidColorBrush(Colors.Purple);
				}
				squares.Add(square);


			}
			return squares;
		
		}
	}
}
