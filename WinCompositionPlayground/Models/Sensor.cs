using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinComposition.Playground.Models
{
	public class Sensor : ObservableObject
	{
		private string _name;

		public string Name {
			get { return _name; }
			set { Set<string>(() => this.Name, ref _name, value); }
		}
		private int _signalStrength;

		public int SignalStrength {
			get { return _signalStrength; }
			set { Set<int>(() => this.SignalStrength, ref _signalStrength, value); }
		}
		private string _sensorId;

		public string SensorID {
			get { return _sensorId; }
			set { Set<string>(() => this.SensorID, ref _sensorId, value); }
		}



	}
}
