using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.Scenes;
using Intro2DGame.Game.Sprites.Enemies.Orbs;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Patterns
{
	public class ExamplePattern : IPattern
	{

		private readonly Vector2 Position;

		public ExamplePattern(Vector2 position)
		{
			this.Position = position;
		}

		public void Shoot(Vector2 position)
		{
			SceneManager.GetCurrentScene().AddSprite(new LinearOrb(this.Position, -Vector2.UnitX, 5f));
		}
	}
}
