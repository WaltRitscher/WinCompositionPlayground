using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;

// Created by Walt Ritscher
// https://www.linkedin.com/in/waltritscher
// I'm a staff author at Lynda.com, part of the LinkedIn family. 
// My content team produces hundreds of technical training courses for software developers each year.

// Lynda.com has over 4200 courses (Topics include IT (dev, devops, web), creative, business, photography, music and lots of documentaries).
// Get your free trial at http://lynda.com
// My courses are at http://lynda.com/waltritscher
// 

namespace WinCompositionPlayground {
	class AnimationFactory {
		private Compositor _compositor;

		public AnimationFactory(Compositor compositor) {
			_compositor = compositor;

		}
		private Random _random = new Random();
		public ScalarKeyFrameAnimation FadeOut( Duration	 duration) {
			var animation = _compositor.CreateScalarKeyFrameAnimation();
			// Create two keyframes that define starting and ending value of the property

			animation.InsertKeyFrame(0.0f, .70f);
			animation.InsertKeyFrame(1.0f, 0.00f);

			animation.Duration = TimeSpan.FromMilliseconds(_random.Next(2000, 4000));

						
		
			return animation;
		}

		
	}
	public abstract class Duration {
		 int MinimumMilliseconds { get; set; }
	}
	public class RandomDuration : Duration {
		
		
		public int MaximumMilliseconds { get; set; }
	}
}
