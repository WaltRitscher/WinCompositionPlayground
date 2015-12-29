using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;

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
