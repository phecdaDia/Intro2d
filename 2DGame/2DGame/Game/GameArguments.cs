using System.Collections.Generic;

namespace Intro2DGame.Game
{
	public class GameArguments
	{
		public GameArguments(params string[] args)
		{
			var argsQueue = new Queue<string>(args);

			// setting default values
			BackbufferWidth = Game.RenderSize.X;
			BackbufferHeight = Game.RenderSize.Y;

			LoadStage = "mainmenu";

			while (argsQueue.Count > 0)
			{
				var arg = argsQueue.Dequeue();
				if (arg.StartsWith("--"))
				{
					// flag
					if (arg == "--fullscreen") IsFullScreen = true;
					else if (arg == "--mynameisclucht")
						IsCheatsEnabled = true;
				}
				else if (arg.StartsWith("-"))
				{
					if (argsQueue.Count == 0) continue;

					var param = argsQueue.Dequeue();

					if (arg == "-backbufferwidth")
					{
						var i = 0;
						int.TryParse(param, out i);
						BackbufferWidth = i;
					}
					else if (arg == "-backbufferheight")
					{
						var i = 0;
						int.TryParse(param, out i);
						BackbufferHeight = i;
					}
					else if (arg == "-stage")
					{
						LoadStage = argsQueue.Dequeue();
					}
				}
			}
		}

		public bool IsFullScreen { get; }

		public bool IsCheatsEnabled { get; }


		public int BackbufferWidth { get; }
		public int BackbufferHeight { get; }

		public string LoadStage { get; }
	}
}