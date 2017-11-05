using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Sprites.Enemies.Orbs
{
	public class PlayerOrb : LinearOrb
	{
		private static readonly Vector2 Direction = new Vector2(0, -1);
		private const float SPEED = 10f;


		public PlayerOrb(Vector2 position) : base(position, Direction, SPEED)
		{
			this.Hue = Color.Red;
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

					if ((sprite.GetPosition() - this.Position).Length() < 20f)
					{
						// TODO: 
					}

				}
			}
		}
	}
}
