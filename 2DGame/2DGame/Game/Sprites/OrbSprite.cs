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
		// I'll just use this sprite to test things for now.


		public OrbSprite(Vector2 position) : base("orb", position)
		{
			Random r = new Random();
			this.Hue = new Color(r.Next(0xFF), r.Next(0xFF), r.Next(0xFF));
		}

		private Vector2 lastMovement = new Vector2();
		public override void Update(GameTime gameTime)
		{
			Vector2 bufferedMovement = new Vector2();
			List<PlayerSprite> playerList = SceneManager.GetSprites<PlayerSprite>();
			foreach (PlayerSprite ps in playerList) {
				Vector2 dist = (ps.GetPosition() - this.position);

				if (dist.Length() < 30)
				{
					this.Delete();
				}
				bufferedMovement += (dist / (float)Math.Pow(dist.Length(), 1.5));
			}

			bufferedMovement *= 0.9992f; // Basically drag
			lastMovement += bufferedMovement;
			this.position += lastMovement;
			
			// Prevents player from leaving the screen
			if ((this.position.X + this.Texture.Width / 2) > Game.GraphicsArea.X) this.position.X = Game.GraphicsArea.X - this.Texture.Width / 2;
			if ((this.position.Y + this.Texture.Height / 2) > Game.GraphicsArea.Y) this.position.Y = Game.GraphicsArea.Y - this.Texture.Height / 2;
			if ((this.position.X - this.Texture.Width / 2) < 0) this.position.X = this.Texture.Width / 2;
			if ((this.position.Y - this.Texture.Height / 2) < 0) this.position.Y = this.Texture.Height / 2;

		}
	}
}
