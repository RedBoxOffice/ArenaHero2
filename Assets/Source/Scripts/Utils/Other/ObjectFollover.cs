using UnityEngine;

namespace ArenaHero.Utils.Other
{
	public class ObjectFollover : MonoBehaviour
	{
		[SerializeField] private Transform _target;

		private void Update() =>
			transform.position = _target.position;
	}
}