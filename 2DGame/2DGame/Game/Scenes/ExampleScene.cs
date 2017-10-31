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
        public ExampleScene() : base("example")
        {

        }

        protected override void CreateScene()
        {
            this.AddSprite(new PlayerSprite(new Vector2(100, 50)));
			this.AddSprite(new AnimationTestSprite(new Vector2(200, 200)));
			this.AddSprite(new RandomSpawnerSprite<OrbSprite>());
        }
    }
}
