using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Composition;

// Created by Walt Ritscher
// https://www.linkedin.com/in/waltritscher
// I'm a staff author at Lynda.com, part of the LinkedIn family. 
// My content team produces hundreds of technical training courses for software developers each year.

// Lynda.com has over 4200 courses (Topics include IT (dev, devops, web), creative, business, photography, music and lots of documentaries).
// Get your free trial at http://lynda.com
// My courses are at http://lynda.com/waltritscher
// 

namespace WinComposition.Playground {

	
	class SpriteGen {
		private Compositor _compositor;

		public SpriteGen(Compositor compositor) {
			_compositor = compositor;

		}
		public SpriteVisual FirstSprite(Color color) {
			var colorVisual = _compositor.CreateSpriteVisual();
			colorVisual.Brush = _compositor.CreateColorBrush(color);
			colorVisual.Size = new Vector2(120.0f, 120.0f);
			colorVisual.Offset = new Vector3(200.0f, 300f, 0.0f);
			colorVisual.CenterPoint = new Vector3(colorVisual.Size.X / 2, colorVisual.Size.Y / 2, 0);
			return colorVisual;
		}

		public SpriteVisual Snowflake() {
			var colorVisual = _compositor.CreateSpriteVisual();
			colorVisual.Brush = _compositor.CreateColorBrush(Colors.White);
			colorVisual.Opacity = .8f;
			colorVisual.Size = new Vector2(30.0f, 30.0f);
			colorVisual.Offset = new Vector3(200.0f, 300f, 0.0f);
			colorVisual.CenterPoint = new Vector3(colorVisual.Size.X / 2, colorVisual.Size.Y / 2, 0);
			return colorVisual;
		}
	}
}
