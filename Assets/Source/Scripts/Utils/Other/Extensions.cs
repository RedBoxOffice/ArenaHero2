using ArenaHero.Debugs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArenaHero.Utils.Other
{
	public static class Extensions
	{
		public static void MoveGameObjectToActiveFightScene(this GameObject target) =>
			SceneManager.MoveGameObjectToScene(target, SceneManager.GetSceneByName(SceneLoader.Instance.GetDebugFightSceneName()));
	}
}