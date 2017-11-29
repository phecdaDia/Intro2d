using Intro2DGame.Game.Sprites;
using Intro2DGame.Game.Sprites.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Intro2DGame.Game.Fonts;
using Intro2DGame.Game.Sprites.Enemies.Orbs;

namespace Intro2DGame.Game.Scenes.Debug
{
	/// <summary>
	/// This scene is used for the second debug fight.
	/// </summary>
	public class Example2Scene : Scene
	{
		public Example2Scene() : base("example2")
		{
		}

		protected override void CreateScene()
		{
			AddSprite(new PlayerSprite(new Vector2(100, 350)));
			//this.AddSprite(new AnimationTestSprite(new Vector2(200, 200)));
			//this.AddSprite(new RandomSpawnerSprite<OrbSprite>(1000));
			
			AddSprite(new DemoSprite1(-10f, new Vector2(700, 200)));
			AddSprite(new DemoSprite1(-1f, new Vector2(700, 250)));
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			
			int i = 0;
			foreach (Type t in new List<Type>(GetAllSprites().Keys))
			{
				foreach (AbstractSprite de in GetAllSprites()[t])
				{
					if (!de.Enemy) continue;
					spriteBatch.DrawString(Game.FontArial, $"{de.GetType().FullName?.Split('.').Last()}: {de.Health}", new Vector2(30, 110 + (i++ * 20)), Color.Black);
				}
			}
		}
	}

	internal class DemoSprite1 : AbstractSprite
	{
		private float Speed;

		public DemoSprite1(float speed, Vector2 position) : base("orb", position)
		{
			this.Speed = speed;
			this.Hue = Color.Red;

		}

		public override void Update(GameTime gameTime)
		{
			if (this.LifeTime.ElapsedGameTime.Milliseconds % 250 < 16)
			{
														/* DIRECTION | NOT SPEED */
				SpawnSprite(new LinearOrb(this.Position, Vector2.UnitX * this.Speed, /* This is the actual speed */ 1f));
			}
		}
	}
}