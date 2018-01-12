using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.ExtensionMethods;
using Intro2DGame.Game.Sprites;
using Intro2DGame.Game.Sprites.Orbs;
using Intro2DGame.Game.Fonts;
using Intro2DGame.Game.Pattern;
using Intro2DGame.Game.Pattern.Movement;
using Intro2DGame.Game.Pattern.Orbs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
			AddSprite(new PlayerSprite(new Vector2(250, 750)));
			AddSprite(new LaserGuySprite(new Vector2(250, 150)));

		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			var lg = SceneManager.GetSprites<LaserGuySprite>().FirstOrDefault();
			FontManager.DrawString(spriteBatch, "example", new Vector2(510, 10), $"Gegner:{lg?.Health ?? 0}");
		}
	}

	internal class LaserGuySprite : AbstractAnimatedSprite
	{

		// This is the current state our enemy is in.

		private int BulletState;

		private readonly Queue<IPattern> Pattern;


		public LaserGuySprite(Vector2 position):base("Enemies/LSprite-0001", position)
		{

			this.MaxHealth = 750;
			this.Health = 750;
			this.Enemy = true;
			this.Persistence = true;
			this.LayerDepth = 1;

			this.Pattern = new Queue<IPattern>();
			AddStates();

		}

		protected override void AddFrames()
		{
			AddAnimation(new [] {new Point(), });
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
			if (Pattern.Peek().Execute(this, gameTime))
			{
				Pattern.Dequeue();

				if (Pattern.Count == 0)
				{
					BulletState++;

					// Add the new states. 
					AddStates();
				} // else we still have patterns left. 
			}
		}

		private void AddStates()
		{
			switch (BulletState)
			{
				case 0:
					Pattern.Enqueue(new SingleLaserPattern());
					Pattern.Enqueue(new SleepPattern(0.5f));
					break;
				case 1:
					Pattern.Enqueue(new BarrageLinearPattern(5.0f, 12.0d, 0.0d));
					Pattern.Enqueue(new SleepPattern(0.5f));
					Pattern.Enqueue(new BarrageLinearPattern(5.0f, 12.0d, 6.0d));
					Pattern.Enqueue(new SleepPattern(0.5f));
					Pattern.Enqueue(new BarrageLinearPattern(5.0f, 12.0d, 0.0d));
					break;
				case 2:
					Pattern.Enqueue(new LinearMovementPattern(new Vector2(0, -100), 0.5d));
					break;
				case 3:
					Pattern.Enqueue(new BarrageLinearPattern(5.0f, 12.0d, 6.0d));
					Pattern.Enqueue(new SleepPattern(0.5f));
					Pattern.Enqueue(new BarrageLinearPattern(5.0f, 12.0d, 0.0d));
					Pattern.Enqueue(new SleepPattern(0.5f));
					Pattern.Enqueue(new BarrageLinearPattern(5.0f, 12.0d, 6.0d));
					break;
				case 4:
					Pattern.Enqueue(new LinearMovementPattern(new Vector2(0, 200), 0.25d));
					break;
				case 5:
					Pattern.Enqueue(new BarrageLinearPattern(5.0f, 12.0d, 6.0d));
					Pattern.Enqueue(new SleepPattern(0.5f));
					Pattern.Enqueue(new BarrageLinearPattern(5.0f, 12.0d, 0.0d));
					Pattern.Enqueue(new SleepPattern(0.5f));
					Pattern.Enqueue(new BarrageLinearPattern(5.0f, 12.0d, 6.0d));
					break;
				case 6:
					Pattern.Enqueue(new LinearMovementPattern(new Vector2(0, -100), 0.5d));
					break;
				case 7:
					Pattern.Enqueue(new RadialMovementPattern(this.Position, new Vector2(-50, 0), 360d, 1.0d));
					break;
			}
		}

		public override bool DoesCollide(Vector2 position)
		{
			return (position - this.Position).Length() <= 16 ||
			       (position - this.Position - new Vector2(0, 16)).Length() <= 16 ||
			       (position - this.Position - new Vector2(0, 32)).Length() <= 16 ||
			       (position - this.Position + new Vector2(0, 16)).Length() <= 16 ||
			       (position - this.Position + new Vector2(0, 32)).Length() <= 16;
		}
	}
}

