using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinComposition.Playground.Models;

namespace WinComposition.Playground.ViewModels {

	// I'm not using the usual ViewModel features, like INotifyPropertyChanged
	// because this is a fixed list of demo pages.
	// I may need to rethink this later...
	class DemosViewModel  {

		public DemosViewModel() {
			var demoItems = new List<DemoItem>();
			demoItems.Add(new DemoItem {DemoName="Checkerboard 1",
																	DocPath = "/Docs/Checkerboard1.html",
																  DemoPageType = typeof(WinComposition.Playground.Views.CheckerboardView) });
			demoItems.Add(new DemoItem
			{
				DemoName = "Checkerboard 2",
				DocPath = "/Docs/Checkerboard2.html",
				DemoPageType = typeof(WinComposition.Playground.Views.CheckerboardView)
			});
			DemoItems = demoItems;
		}

	public	List<DemoItem> DemoItems {
			get;
			set;

		}
	}
}
