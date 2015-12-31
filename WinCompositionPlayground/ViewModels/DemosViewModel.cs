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
			var docs = new List<DemoItem>();
			docs.Add(new DemoItem { DocPath = "Checkerboard1.html", DemoPageType = typeof(WinComposition.Playground.Views.CheckerboardView) });
			//docs.Add("Checkerboard2.html");

			DocumentUris = docs;
		}

		List<DemoItem> DocumentUris {
			get;
			set;

		}
	}
}
