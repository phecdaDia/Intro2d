using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Intro2DGame.Game.Patterns
{
	internal interface IPattern
	{
		void Shoot(Vector2 position);
	}
}
