using System;
using UnityEngine;

namespace ArenaHero.Battle
{
	public class HealthViewCanvasInitializer : MonoBehaviour
	{
		private void Awake()
		{
			GetComponent<Canvas>().worldCamera = Camera.main;
		}
	}
}