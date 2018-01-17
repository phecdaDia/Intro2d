using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro2DGame.Game.ExtensionMethods
{
	public static class QueueExtension
	{
		public static void EnqueueMany<T>(this Queue<T> queue, params T[] arr)
		{
			foreach (var a in arr)
			{
				queue.Enqueue(a);
			}
		}

	}
}
