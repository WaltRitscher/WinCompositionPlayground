using System;
using WinComposition.Playground.Models;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace WinComposition.Playground.ValueConverters
{
	internal class StrengthToColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var candidate = (int)value ;
			Color tempColor = Colors.LightBlue;
			if (candidate > 10)
			{
				tempColor = Colors.Orange;
			}
			if (candidate > 20)
			{
				tempColor = Colors.Yellow;
			}
			return new SolidColorBrush(tempColor);
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}