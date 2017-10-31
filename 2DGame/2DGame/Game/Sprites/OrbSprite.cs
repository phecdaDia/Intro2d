using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Intro2DGame.Game.Scenes;

namespace Intro2DGame.Game.Sprites
{
	public class OrbSprite : AbstractSprite
	{
		public OrbSprite(Vector2 position) : base("orb", position)
		{
			Random r = new Random();
			this.Hue = new Color(r.Next(0xFF), r.Next(0xFF), r.Next(0xFF));
		}

		public override void Update(GameTime gameTime)
		{
			Vector2 bufferedMovement = new Vector2();
			List<PlayerSprite> playerList = SceneManager.GetSprites<PlayerSprite>();
			foreach (PlayerSprite ps in playerList) {
				bufferedMovement += ps.GetPosition() - this.position;
			}

			if (bufferedMovement.LengthSquared() > 0) bufferedMovement.Normalize();
			this.position += 0.1f * bufferedMovement;

		}
	}
}
