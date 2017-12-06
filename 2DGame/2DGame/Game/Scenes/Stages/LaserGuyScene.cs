using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.Sprites;
using Intro2DGame.Game.Sprites.Enemies.Orbs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Intro2DGame.Game.Scenes.Stages
{
	public class LaserGuyScene : Scene
	{
		public LaserGuyScene() : base("laserguy")
		{
		}

		protected override void CreateScene()
		{
			AddSprite(new PlayerSprite(new Vector2(100, 360)));
			AddSprite(new LaserGuySprite(new Vector2(1180, 360)));

		}
	}

	internal class LaserGuySprite : AbstractAnimatedSprite
	{

		private double ElapsedSeconds;

		// This is the current state our enemy is in.
		private int State = 0;

		private int HelpState = 0;


		public LaserGuySprite(Vector2 position):base("tutorialplayer",position)
		{
			this.Enemy = true;
			this.Persistence = true;

			this.MaxHealth = 750;
			this.Health = 750;
		}

		protected override void AddFrames()
		{
			AddAnimation(new Point[] {new Point(), });
		}

		public override void Update(GameTime gameTime)
		{
			ElapsedSeconds += gameTime.ElapsedGameTime.TotalSeconds;

			var state = State;

			if (Health < 250) State = 2;
			else if (Health < 500) State = 1;

			if (state != State) // State changed
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

			var player = SceneManager.GetSprites<PlayerSprite>().First();

			if (State == 0)
			{
				if (ElapsedSeconds >= 1.0f)
				{
					ElapsedSeconds -= 1.0f;
					SpawnSprite(new LaserOrb(this.Position, player.GetPosition() - this.GetPosition(), 2f, 10f));
				}
			}
			
		}
	}
}

