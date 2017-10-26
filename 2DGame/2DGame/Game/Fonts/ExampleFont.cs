using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro2DGame.Game.Fonts
{
	public class ExampleFont : CustomFont
	{
		public ExampleFont() : base("example", "Fonts/Example", new Vector2(16, 32))
		{

		}

		protected override bool IsIgnoreCase()
		{
			return true;
		}

		protected override void SetSymbolPositions()
		{
			this.symbolPosition.Add('a', new Point(0, 0));
			this.symbolPosition.Add('b', new Point(16, 0));
			this.symbolPosition.Add('c', new Point(32, 0));
			this.symbolPosition.Add('d', new Point(48, 0));

			this.symbolPosition.Add('e', new Point(64, 0));
			this.symbolPosition.Add('f', new Point(80, 0));
			this.symbolPosition.Add('g', new Point(96, 0));
			this.symbolPosition.Add('h', new Point(112, 0));

			this.symbolPosition.Add('i', new Point(128, 0));
			this.symbolPosition.Add('j', new Point(144, 0));
			this.symbolPosition.Add('k', new Point(160, 0));
			this.symbolPosition.Add('l', new Point(176, 0));

			this.symbolPosition.Add('m', new Point(192, 0));
			this.symbolPosition.Add('n', new Point(208, 0));
			this.symbolPosition.Add('o', new Point(224, 0));
			this.symbolPosition.Add('p', new Point(240, 0));

			// Second Row
			this.symbolPosition.Add('q', new Point(0, 32));
			this.symbolPosition.Add('r', new Point(16, 32));
			this.symbolPosition.Add('s', new Point(32, 32));
			this.symbolPosition.Add('t', new Point(48, 32));

			this.symbolPosition.Add('u', new Point(64, 32));
			this.symbolPosition.Add('v', new Point(80, 32));
			this.symbolPosition.Add('w', new Point(96, 32));
			this.symbolPosition.Add('x', new Point(112, 32));

			this.symbolPosition.Add('y', new Point(128, 32));
			this.symbolPosition.Add('z', new Point(144, 32));

			// Third Row
			this.symbolPosition.Add('0', new Point(0, 64));
			this.symbolPosition.Add('1', new Point(16, 64));
			this.symbolPosition.Add('2', new Point(32, 64));
			this.symbolPosition.Add('3', new Point(48, 64));

			this.symbolPosition.Add('4', new Point(64, 64));
			this.symbolPosition.Add('5', new Point(80, 64));
			this.symbolPosition.Add('6', new Point(96, 64));
			this.symbolPosition.Add('7', new Point(112, 64));

			this.symbolPosition.Add('8', new Point(128, 64));
			this.symbolPosition.Add('9', new Point(144, 64));
		}
	}
}
