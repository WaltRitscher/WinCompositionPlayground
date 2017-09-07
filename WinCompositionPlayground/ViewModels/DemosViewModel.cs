using System.Collections.Generic;
using WinComposition.Playground.Models;

namespace WinComposition.Playground.ViewModels
{
	// I'm not using the usual ViewModel features, like INotifyPropertyChanged
	// because this is a fixed list of demo pages.
	// I may need to rethink this later...
	internal class DemosViewModel
	{
		public DemosViewModel()
		{
			var demoItems = new List<DemoItem>();
			demoItems.Add(new DemoItem
			{
				DemoName = "Checkerboard Static",
				DocPath = "/Docs/CheckerboardStatic.html",
				DemoPageType = typeof(WinComposition.Playground.Views.CheckerboardStatic)
			});
			demoItems.Add(new DemoItem
			{
				DemoName = "Checkerboard Get Composition",
				DocPath = "/Docs/CheckerboardGet.html",
				DemoPageType = typeof(WinComposition.Playground.Views.CheckerboardGet)
			});

			demoItems.Add(new DemoItem
			{
				DemoName = "Batch Animation",
				DocPath = "/Docs/Checkerboard1.html",
				DemoPageType = typeof(WinComposition.Playground.Views.Animation.Batches)
			});
			demoItems.Add(new DemoItem
			{
				DemoName = "Checkerboard (Fade animation)",
				DocPath = "/Docs/Checkerboard1.html",
				DemoPageType = typeof(WinComposition.Playground.Views.CheckerboardView)
			});

			demoItems.Add(new DemoItem
			{
				DemoName = "Checkerboard (Rotate animation)",
				DocPath = "/Docs/Checkerboard2.html",
				DemoPageType = typeof(WinComposition.Playground.Views.CheckerboardSpin)
			});
			DemoItems = demoItems;

      demoItems.Add(new DemoItem
      {
        DemoName = "Add Drop Shadow",
        DocPath = "/Docs/Checkerboard2.html",
        DemoPageType = typeof(WinComposition.Playground.Views.Effects.SimpleDropShadow)
      });

      demoItems.Add(new DemoItem
      {
        DemoName = "First Experiment",
        DocPath = "/Docs/Checkerboard2.html",
        DemoPageType = typeof(WinComposition.Playground.MainPage)
      });

      demoItems.Add(new DemoItem
      {
        DemoName = "Trailing Shapes",
        DocPath = "/Docs/Checkerboard2.html",
        DemoPageType = typeof(WinComposition.Playground.Views.Fun.TrailingShapes)
      });
      DemoItems = demoItems;
    }

		public List<DemoItem> DemoItems {
			get;
			set;
		}
	}
}