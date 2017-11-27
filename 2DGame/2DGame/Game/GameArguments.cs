using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro2DGame.Game
{
    public class GameArguments
    {
        public bool IsFullScreen { get; private set; }
        public bool ShowConsole { get; private set; }

        public bool IsCheatsEnabled { get; private set; }


        public int BackbufferWidth { get; private set; }
        public int BackbufferHeight { get; private set; }

        public GameArguments(params string[] args)
        {
            Queue<string> argsQueue = new Queue<string>(args);

            // setting default values
            this.BackbufferWidth = 800;
            this.BackbufferHeight = 600;


            while (argsQueue.Count > 0)
            {
                string arg = argsQueue.Dequeue();
                if (arg.StartsWith("--"))
                {
                    // flag
                    if (arg == "--fullscreen") IsFullScreen = true;
                    else if (arg == "--console") ShowConsole = true;
                    else if (arg == "--mynameisclucht") IsCheatsEnabled = true;
                }
                else if (arg.StartsWith("-"))
                {
                    if (argsQueue.Count == 0) continue;

                    string param = argsQueue.Dequeue();

                    if (arg == "-backbufferwidth")
                    {
                        int i = 0;
                        int.TryParse(param, out i);
                        BackbufferWidth = i;
                    }
                    else if (arg == "-backbufferheight")
                    {
                        int i = 0;
                        int.TryParse(param, out i);
                        BackbufferHeight = i;
                    }

                }
            }
        }

    }
}
