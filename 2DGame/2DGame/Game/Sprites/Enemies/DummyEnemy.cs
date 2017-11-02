using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Intro2DGame.Game.Sprites.Enemies.Orbs;

namespace Intro2DGame.Game.Sprites.Enemies
{
	public class DummyEnemy : AbstractEnemy
	{
		public DummyEnemy(Vector2 position) : base("orb", position)
		{
            this.Hue = Color.Red;
		}


		private float f, z = 0.0f;
        int k = 0;
        int t = 6;
		public override void Update(GameTime gameTime)
		{

			f += (float)(((72.5f/360f) + 0.0001f) / Math.PI);

            Vector2 p = new Vector2((float)Math.Sin(f), (float)Math.Cos(f));
            Vector2 p2 = new Vector2((float)Math.Cos(f), (float)Math.Sin(f));
            if (++k >= t)
            {
                k %= t;

                ShootOrb<LinearIncreasingOrb>(this.Position, p, 0.1f, 1.01f);
            }
            else if (k == t / 4)
            {

                ShootOrb<LinearIncreasingOrb>(this.Position, p2, 0.1f, 1.01f);
            }
            else if (k == t / 2)
            {

                ShootOrb<LinearIncreasingOrb>(this.Position, -p, 0.1f, 1.01f);
            }
            else if (k == 3 * t / 4)
            {

                ShootOrb<LinearIncreasingOrb>(this.Position, -p2, 0.1f, 1.01f);
            }
        }
	}
}
