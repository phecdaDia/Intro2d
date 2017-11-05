using System;
using System.Collections;
using System.Collections.Generic;
using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Sprites.Enemies.Orbs
{
	public class PlayerOrb : LinearOrb
	{
		//private static readonly Vector2 Direction = new Vector2(0, -1);
		private const float SPEED = 10f;


		public PlayerOrb(Vector2 position, Vector2 goal) : base(position, goal - position, SPEED)
		{
			this.Hue = Color.Purple;
		}

		public override void Update(GameTime gameTime)
		{
			UpdatePosition(gameTime);

			Dictionary<Type, IList> sprites = SceneManager.GetAllSprites();

			foreach (Type t in sprites.Keys)
			{
				foreach (AbstractSprite sprite in sprites[t])
				{
					if (!sprite.IsEnemy()) break;

					if (sprite.DoesCollide(this))
					{
						sprite.Damage(1);
					}

				}
			}
		}
	}
}
