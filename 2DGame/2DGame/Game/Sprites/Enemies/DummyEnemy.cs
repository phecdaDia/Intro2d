using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;
using Intro2DGame.Game.Sprites.Enemies.Orbs;

namespace Intro2DGame.Game.Sprites.Enemies
{
	public class DummyEnemy : AbstractSprite
	{
		public DummyEnemy(Vector2 position) : base("orb", position)
		{
            this.Hue = Color.Red;

			this.SetEnemy(true);
		}

        int k = 0;
		public override void Update(GameTime gameTime)
		{
            float c = 0;
			Vector2 p = new Vector2((float)Math.Sin(c), (float)Math.Cos(c));


			if (++k > 30)
            {
                k %= 30;
                // Shoot something
	            List<PlayerSprite> players = SceneManager.GetSprites<PlayerSprite>();
	            foreach (PlayerSprite ps in players)
	            {
		            Vector2 dir = ps.GetPosition() - this.GetPosition();
		            double tan = Math.Atan2(dir.X, dir.Y);
		            double degrees = 2 * Math.PI * (22.5f / 360f);
		            int k = 16;
		            double degrees2 = 2 * Math.PI * (360f / k);

					for (int i=0; i<k; i++)
						ShootOrb<LinearIncreasingOrb>(this.Position, new Vector2((float)Math.Sin(tan + i * degrees), (float)Math.Cos(tan + i * degrees)), 1, 1.001f);
				}
			}
        }
	}
}
