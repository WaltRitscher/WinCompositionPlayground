using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinComposition.Playground.Models;

namespace WinComposition.Playground.ViewModels
{
	public class SensorsViewModel : GalaSoft.MvvmLight.ViewModelBase
	{
		public SensorsViewModel() {
			LoadSensors();
		}
		private ObservableCollection<Sensor> sensors;
		public ObservableCollection<Sensor> SensorList {
			get
			{
				return sensors;
			}
		}

		private void LoadSensors()
		{
			var datasource = new DemoDataSource();
			sensors = datasource.GetSensors();
			this.RaisePropertyChanged(() => this.SensorList);
			Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Employees Loaded."));
		}
	}


}
