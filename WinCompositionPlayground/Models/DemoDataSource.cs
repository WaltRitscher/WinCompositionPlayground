using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinComposition.Playground.Models
{
	public class DemoDataSource
	{
		private List<Sensor> _sensors = new List<Sensor>();
		private int _count = 3000;
		public DemoDataSource()
		{
			//var squares = new ObservableCollection<Rectangle>();
			Sensor sensor;
			for (int counter = 0; counter < _count; counter++)
			{
				sensor = new Sensor
				{
					Name = "",
					SensorID = counter.ToString("D2"),
					SignalStrength = 10
				};

				if (counter % 3 == 0)
				{
					sensor.SignalStrength = 20;
				}
				if (counter % 3 == 1)
				{
					sensor.SignalStrength = 30;
				}
				_sensors.Add(sensor);

			}
		}

		public List<Sensor> GetSensors()
		{
			return _sensors;
		}


	}
}
