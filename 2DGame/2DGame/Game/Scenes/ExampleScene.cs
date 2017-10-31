using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro2DGame.Game.Scenes
{
    public class ExampleScene : Scene
    {
        public ExampleScene() : base("example")
        {

        }

		private bool s = false;
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			KeyboardState ks = Keyboard.GetState();
			if (ks.IsKeyDown(Keys.Q) && s)
			{
				this.AddSprite(new PlayerSprite(new Vector2(100, 50)));
				s = false;
			}
			else if (ks.IsKeyUp(Keys.Q))
			{
				s = true;
			}
		}

		protected override void CreateScene()
        {
            this.AddSprite(new PlayerSprite(new Vector2(100, 50)));
			this.AddSprite(new AnimationTestSprite(new Vector2(200, 200)));
			this.AddSprite(new RandomSpawnerSprite<OrbSprite>(1000));
        }
    }
}
