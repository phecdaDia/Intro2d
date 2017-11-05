using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


            if (++k > 60)
            {
                k %= 60;
                // Shoot something
            }
        }
	}
}
