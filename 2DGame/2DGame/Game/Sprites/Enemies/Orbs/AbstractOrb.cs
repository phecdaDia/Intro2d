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
        public AbstractOrb(String textureKey, Vector2 Position) : base(textureKey, Position)
        {
        }
        
    }
}
