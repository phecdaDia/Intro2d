using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.Scenes;
using Intro2DGame.Game.Sprites.Enemies.Orbs;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Sprites.Enemies
{
	public class ExampleEnemy : AbstractSprite
	{
		private int State = 0;

		private long Milliseconds = 0;

		public ExampleEnemy() : base("tutorialplayer", new Vector2(700, 350))
		{
			this.Enemy = true;
			this.Persistence = true;

			this.MaxHealth = 300;
			this.Health = 300;
		}

		public override void Update(GameTime gameTime)
		{
			Milliseconds += gameTime.ElapsedGameTime.Milliseconds;
			var state = State;

			State = MaxHealth / 100 - (Health - 1) / 100 - 1;

			if (state != State)
			{
				// Remove all orbs once
				var sprites = SceneManager.GetAllSprites();
				foreach (var keyValuePair in sprites)
				{
					if (!keyValuePair.Key.IsSubclassOf(typeof(AbstractOrb))) continue;

					foreach (AbstractOrb t in keyValuePair.Value)
					{
						t.Delete();
					}
				}
			}

			// Now enemy movement and logic

			if (this.State == 0)
			{
				// Movement and logic for State 0

			} else if (this.State == 1)
			{
				// Movement and logic for State 1

			} else if (this.State == 2)
			{
				// Movement and logic for State 2

			}


		}
	}
}
