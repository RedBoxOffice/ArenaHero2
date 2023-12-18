using ArenaHero.Battle.Level;
using ArenaHero.Battle.PlayableCharacter;
using UnityEngine;

namespace ArenaHero.Game.Level
{
	public class HeroInitializer : MonoBehaviour
	{
		[SerializeField] private LookTargetPoint _lookTargetPoint;
		[SerializeField] private Player _playerPrefab;
		[SerializeField] private PlayerSpawnPoint _playerSpawnPoint;

		private Hero _hero;

		public LookTargetPoint LookTargetPoint => _lookTargetPoint;
		
		public Hero Hero
		{
			get
			{
				if (_hero == null)
					_hero = SpawnPlayer().Init(_lookTargetPoint);

				return _hero;
			}
		}

		private Hero SpawnPlayer() =>
			Instantiate(_playerPrefab, _playerSpawnPoint.gameObject.transform.position, Quaternion.identity).GetComponentInChildren<Hero>();
	}
}