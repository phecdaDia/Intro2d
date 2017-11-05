using Microsoft.Xna.Framework;

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
			this.SymbolPosition.Add('a', new Point(0, 0));
			this.SymbolPosition.Add('b', new Point(16, 0));
			this.SymbolPosition.Add('c', new Point(32, 0));
			this.SymbolPosition.Add('d', new Point(48, 0));

			this.SymbolPosition.Add('e', new Point(64, 0));
			this.SymbolPosition.Add('f', new Point(80, 0));
			this.SymbolPosition.Add('g', new Point(96, 0));
			this.SymbolPosition.Add('h', new Point(112, 0));

			this.SymbolPosition.Add('i', new Point(128, 0));
			this.SymbolPosition.Add('j', new Point(144, 0));
			this.SymbolPosition.Add('k', new Point(160, 0));
			this.SymbolPosition.Add('l', new Point(176, 0));

			this.SymbolPosition.Add('m', new Point(192, 0));
			this.SymbolPosition.Add('n', new Point(208, 0));
			this.SymbolPosition.Add('o', new Point(224, 0));
			this.SymbolPosition.Add('p', new Point(240, 0));

			// Second Row
			this.SymbolPosition.Add('q', new Point(0, 32));
			this.SymbolPosition.Add('r', new Point(16, 32));
			this.SymbolPosition.Add('s', new Point(32, 32));
			this.SymbolPosition.Add('t', new Point(48, 32));

			this.SymbolPosition.Add('u', new Point(64, 32));
			this.SymbolPosition.Add('v', new Point(80, 32));
			this.SymbolPosition.Add('w', new Point(96, 32));
			this.SymbolPosition.Add('x', new Point(112, 32));

			this.SymbolPosition.Add('y', new Point(128, 32));
			this.SymbolPosition.Add('z', new Point(144, 32));

			// Third Row
			this.SymbolPosition.Add('0', new Point(0, 64));
			this.SymbolPosition.Add('1', new Point(16, 64));
			this.SymbolPosition.Add('2', new Point(32, 64));
			this.SymbolPosition.Add('3', new Point(48, 64));

			this.SymbolPosition.Add('4', new Point(64, 64));
			this.SymbolPosition.Add('5', new Point(80, 64));
			this.SymbolPosition.Add('6', new Point(96, 64));
			this.SymbolPosition.Add('7', new Point(112, 64));

			this.SymbolPosition.Add('8', new Point(128, 64));
			this.SymbolPosition.Add('9', new Point(144, 64));
		}
	}
}
