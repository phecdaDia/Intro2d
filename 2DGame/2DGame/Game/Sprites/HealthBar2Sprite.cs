using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Intro2DGame.Game.Sprites
{
	public class HealthBar2Sprite : AbstractSprite
	{
		private const float HEALTH_BAR_WIDTH_MAX = 500;

		private readonly AbstractSprite HostSprite;


		public HealthBar2Sprite(HealthBarPosition position, AbstractSprite hostSprite)
		{
			switch (position)
			{
				case HealthBarPosition.ScreenTop:
					this.Position = new Vector2(0, 0);
					break;
				case HealthBarPosition.ScreenBottom:
					this.Position = new Vector2(0, Game.RenderSize.Y - 10);
					break;
			}

			this.HostSprite = hostSprite;

			this.Scale = new Vector2(HEALTH_BAR_WIDTH_MAX, 10);
		}

		public override void Update(GameTime gameTime)
		{
			this.Scale.X = (HEALTH_BAR_WIDTH_MAX * this.HostSprite.Health) / this.HostSprite.MaxHealth;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Game.WhitePixel, this.Position, null, new Color(Color.White, 0.85f), 0.0f, new Vector2(), this.Scale, SpriteEffects.None, 0.0f);
		}
	}

	public enum HealthBarPosition
	{
		ScreenTop,
		ScreenBottom,
	}
}
