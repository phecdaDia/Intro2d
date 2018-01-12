using System;

namespace Intro2DGame.Game.Scenes.Transition
{
	public abstract class AbstractTransition : Scene
	{
		private bool DidLambda;

		private Action Lambda;

		protected AbstractTransition() : base("transition")
		{
		}

		public void SetLambda(Action lambda)
		{
			Lambda = lambda;
		}

		protected void RunLambda()
		{
			if (DidLambda) return;
			DidLambda = true;
			Lambda();
		}
	}
}