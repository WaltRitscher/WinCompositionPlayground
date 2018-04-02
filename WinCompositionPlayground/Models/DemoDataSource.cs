using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinComposition.Playground.Models
{
	public class DemoDataSource
	{
		private ObservableCollection<Sensor> _sensors = new ObservableCollection<Sensor>();
		private int _count = 2000;
    private Random _random = new Random();
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
					SignalStrength = _random.Next(30),
				};

				//if (SignalStrength % 3 == 0)
				//{
				//	sensor.SignalStrength = 20;
				//}
				//if (counter % 3 == 1)
				//{
				//	sensor.SignalStrength = 30;
				//}
				_sensors.Add(sensor);

			}
		}

		public ObservableCollection<Sensor> GetSensors()
		{
			return _sensors;
		}


	}
}
