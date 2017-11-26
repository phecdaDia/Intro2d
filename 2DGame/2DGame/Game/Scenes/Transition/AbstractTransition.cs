using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro2DGame.Game.Scenes.Transition
{
	public abstract class AbstractTransition : Scene
	{
		public Scene TransitioningScene;
		public string TransitioningKey;

		private bool DidTransition;

		protected AbstractTransition (string key) : base(key) { }

		protected AbstractTransition(Scene newScene, string key) : base(key)
		{
			this.TransitioningScene = newScene;
		}

		protected AbstractTransition(string newScene, string key) : base(key)
		{
			this.TransitioningKey = newScene;
		}

		protected void AddTransitioningScene()
		{
			if (DidTransition) return;
			DidTransition = true;

			if (TransitioningScene != null) SceneManager.AddScene(TransitioningScene);
			else if (TransitioningKey != null) SceneManager.AddScene(TransitioningKey);
		}

	}
}
