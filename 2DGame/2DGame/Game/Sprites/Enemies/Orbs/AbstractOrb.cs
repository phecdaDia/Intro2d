﻿using System.Collections.Generic;
using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Sprites.Enemies.Orbs
{
    public abstract class AbstractOrb : AbstractSprite
    {
        public AbstractOrb(string textureKey, Vector2 position) : base(textureKey, position)
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
