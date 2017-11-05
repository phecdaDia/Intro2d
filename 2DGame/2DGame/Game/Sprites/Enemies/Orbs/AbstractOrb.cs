using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro2DGame.Game.Sprites.Enemies.Orbs
{
    public abstract class AbstractOrb : AbstractSprite
    {
        public AbstractOrb(String textureKey, Vector2 position) : base(textureKey, position)
        {
        }

        public override void Update(GameTime gameTime)
        {
            UpdatePosition(gameTime);

            List<PlayerSprite> players = SceneManager.GetSprites<PlayerSprite>();
            foreach (PlayerSprite ps in players)
            {
                if (ps.DoesCollide(this)) this.Hue = Color.Red;
            }
        }

        protected abstract void UpdatePosition(GameTime gameTime);

    }
}
