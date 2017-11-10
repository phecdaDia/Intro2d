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
			SymbolPosition.Add('a', new Point(0, 0));
			SymbolPosition.Add('b', new Point(16, 0));
			SymbolPosition.Add('c', new Point(32, 0));
			SymbolPosition.Add('d', new Point(48, 0));

			SymbolPosition.Add('e', new Point(64, 0));
			SymbolPosition.Add('f', new Point(80, 0));
			SymbolPosition.Add('g', new Point(96, 0));
			SymbolPosition.Add('h', new Point(112, 0));

			SymbolPosition.Add('i', new Point(128, 0));
			SymbolPosition.Add('j', new Point(144, 0));
			SymbolPosition.Add('k', new Point(160, 0));
			SymbolPosition.Add('l', new Point(176, 0));

			SymbolPosition.Add('m', new Point(192, 0));
			SymbolPosition.Add('n', new Point(208, 0));
			SymbolPosition.Add('o', new Point(224, 0));
			SymbolPosition.Add('p', new Point(240, 0));

			// Second Row
			SymbolPosition.Add('q', new Point(0, 32));
			SymbolPosition.Add('r', new Point(16, 32));
			SymbolPosition.Add('s', new Point(32, 32));
			SymbolPosition.Add('t', new Point(48, 32));

			SymbolPosition.Add('u', new Point(64, 32));
			SymbolPosition.Add('v', new Point(80, 32));
			SymbolPosition.Add('w', new Point(96, 32));
			SymbolPosition.Add('x', new Point(112, 32));

			SymbolPosition.Add('y', new Point(128, 32));
			SymbolPosition.Add('z', new Point(144, 32));

			// Third Row
			SymbolPosition.Add('0', new Point(0, 64));
			SymbolPosition.Add('1', new Point(16, 64));
			SymbolPosition.Add('2', new Point(32, 64));
			SymbolPosition.Add('3', new Point(48, 64));

			SymbolPosition.Add('4', new Point(64, 64));
			SymbolPosition.Add('5', new Point(80, 64));
			SymbolPosition.Add('6', new Point(96, 64));
			SymbolPosition.Add('7', new Point(112, 64));

			SymbolPosition.Add('8', new Point(128, 64));
			SymbolPosition.Add('9', new Point(144, 64));
		}
	}
}