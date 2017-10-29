using Intro2DGame.Game.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro2DGame.Game.Scenes
{
    public class ExampleScene : Scene
    {
        private PlayerSprite player;
		private AnimationTestSprite ats;

        public ExampleScene() : base("example")
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
			this.player.Draw(spriteBatch);
			this.ats.Draw(spriteBatch);

		}

        public override void Update(GameTime gameTime)
        {
            this.player.Update(gameTime);
			this.ats.Update(gameTime);
        }

        protected override void CreateScene()
        {
            this.player = new PlayerSprite(new Vector2(100, 50));
			this.ats = new AnimationTestSprite(new Vector2(200, 200));
        }

        public override void ResetScene()
        {
            CreateScene();
        }
    }
}
