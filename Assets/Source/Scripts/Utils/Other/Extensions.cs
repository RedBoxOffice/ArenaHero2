using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArenaHero.Utils.Other
{
	public static class Extensions
	{
		public static void DestroyOnLoad(this GameObject target) =>
			SceneManager.MoveGameObjectToScene(target, SceneManager.GetActiveScene());
	}
}