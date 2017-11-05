using Intro2DGame.Game.Scenes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro2DGame.Game.Sprites.Enemies
{
	public abstract class AbstractEnemy : AbstractSprite
	{
		public AbstractEnemy(String textureKey, Vector2 position) : base(textureKey, position)
		{
            this.Persistence = true;

        }

		protected void ShootOrb<T>(params object[] parameters) where T : AbstractSprite
		{
			T o = (T)Activator.CreateInstance(typeof(T), parameters);
            SceneManager.GetCurrentScene().AddSprite(o);

        }
    }
}
