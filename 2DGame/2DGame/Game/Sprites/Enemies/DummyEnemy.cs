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

        double q = 0;
        int timer = 0;
        double z = 0;
		public override void Update(GameTime gameTime)
		{
            float c = 0;
			Vector2 p = new Vector2((float)Math.Sin(c), (float)Math.Cos(c));

            const int frames = 15;
			if (++timer > frames)
            {
                timer %= frames;
                // Shoot something
	            List<PlayerSprite> players = SceneManager.GetSprites<PlayerSprite>();
	            foreach (PlayerSprite ps in players)
	            {
		            Vector2 dir = ps.GetPosition() - this.GetPosition();
                    double tan = 22.5d * 2 * Math.PI;// Math.Atan2(dir.X, dir.Y);
		            double degrees = 2 * Math.PI * (22.5f / 360f);
		            int k = 24;
                    tan += q;
                    double temp_ = 2 * Math.PI * 0.5d * (1f / k);
                    q += temp_;
                    z += temp_ / 2f;

                    double degrees2 = 2 * Math.PI * (1f / k);

					for (int i=0; i<k; i++)
						ShootOrb<LinearIncreasingOrb>(this.Position, new Vector2((float)Math.Sin(tan + i * degrees2 + z), (float)Math.Cos(tan + i * degrees2 + z)), 0.01f, 1.01025f);
				}
			}
        }
	}
}
