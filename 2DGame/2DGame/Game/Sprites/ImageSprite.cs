using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Sprites
{
	public class ImageSprite : AbstractSprite
	{
		public ImageSprite(String key, Vector2 position) : base()
		{
			this.Texture = ImageManager.GetTexture2D(key);
			this.position = position;
		}

		public override void Update(GameTime gameTime) { }
	}
}
