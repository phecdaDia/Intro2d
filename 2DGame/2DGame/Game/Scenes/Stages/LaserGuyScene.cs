using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Intro2DGame.Game.ExtensionMethods;
using Intro2DGame.Game.Pattern;
using Intro2DGame.Game.Pattern.LaserGuy;
using Intro2DGame.Game.Pattern.Movement;
using Intro2DGame.Game.Pattern.Orbs;
using Intro2DGame.Game.Scenes.Transition;
using Intro2DGame.Game.Sprites;
using Intro2DGame.Game.Sprites.Orbs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Scenes.Stages
{
	public class LaserGuyScene : Scene
	{
		private bool ShownDialog = false;

		public LaserGuyScene() : base("laserguy")
		{
		}

		protected override void CreateScene()
		{
			AddSprite(new PlayerSprite(new Vector2(250, 750)));
			AddSprite(new LaserGuySprite(new Vector2(250, 150)));
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			var lg = SceneManager.GetSprites<LaserGuySprite>().First();

			if (lg.Health == 0 && !ShownDialog)
			{
				SceneManager.AddScene(new DialogScene("*** PLACEHOLDER TEXT *** YOU ARE WINNER"));
				ShownDialog = true;

				return;
			}
			else if (ShownDialog)
			{
				SceneManager.CloseScene(new TestTransition(1.0d));
				return;
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			//var lg = SceneManager.GetSprites<LaserGuySprite>().FirstOrDefault();
			//FontManager.DrawString(spriteBatch, "example", new Vector2(510, 10), $"Gegner:{lg?.Health ?? 0}");
		}
	}

	internal class LaserGuySprite : AbstractAnimatedSprite
	{
		private readonly Queue<IPattern> Pattern;
		// This is the current state our enemy is in.

		private int BulletState;


		public LaserGuySprite(Vector2 position) : base("Enemies/LSprite-0001", position)
		{
			MaxHealth = 750;
			Health = 750;
			Enemy = true;
			Persistence = true;
			LayerDepth = 1;

			Pattern = new Queue<IPattern>();
			AddStates();

			SceneManager.GetCurrentScene().AddSprite(new HealthBarSprite(this, new Vector2(510, 50), 50, 800));
			SceneManager.GetCurrentScene().AddSprite(new HealthBar2Sprite(HealthBarPosition.ScreenTop, this));
		}

		protected override void AddFrames()
		{
			AddAnimation(new[] {new Point()});
		}

		public override void Update(GameTime gameTime)
		{
			if (Pattern.Count == 0)
			{
				// just don't do anything this frame. This should never execute!
				Console.WriteLine($"Queue is empty, Bulletstate {BulletState}");
				BulletState = 0;

				AddStates();
			}

			ExecutePattern(gameTime);
		}

		private void ExecutePattern(GameTime gameTime)
		{
			while (Pattern.Count > 0)
			{
				var success = Pattern.Peek().Execute(this, gameTime);

				if (success)
				{
					Pattern.Dequeue();

					if (Pattern.Count == 0)
					{
						BulletState++;

						// Add the new states. 
						AddStates();
					} // else we still have patterns left. 
				}
				else
				{
					break;
				}
			}
		}

		private void AddStates()
		{


			if (BulletState == 0)
			{
				Pattern.EnqueueMany(
					new SingleLaserPattern(90.0d, 1.0f, 1.0f),
					new LinearMovementPattern(new Vector2(0, 100), 1.0f)
				);
			} 
			else if (BulletState == 1)
			{
			}
		}

		public override bool DoesCollide(Vector2 position)
		{
			return (position - Position).Length() <= 16 ||
			       (position - Position - new Vector2(0, 16)).Length() <= 16 ||
			       (position - Position - new Vector2(0, 32)).Length() <= 16 ||
			       (position - Position + new Vector2(0, 16)).Length() <= 16 ||
			       (position - Position + new Vector2(0, 32)).Length() <= 16;
		}
	}
}