using ArenaHero.Battle.Level;
using ArenaHero.Battle.PlayableCharacter;
using UnityEngine;

namespace ArenaHero.Game.Level
{
	public class PlayerInitializer : MonoBehaviour
	{
		[SerializeField] private LookTargetPoint _lookTargetPoint;
		[SerializeField] private Player _playerPrefab;

		private Hero _hero;

		public LookTargetPoint LookTargetPoint => _lookTargetPoint;

		public void OnStageChanged(LevelStageObjectsHolder holder)
		{
			GetHero().transform.localPosition = holder.PlayerSpawnPoint.transform.position;
			
			_lookTargetPoint.SetDefaultPosition(GetHero().transform);
		}

		public Hero GetHero()
		{
			Init();
			
			return _hero;
		}
		
		private void Init()
		{
			if (_hero == null)
			{
				_hero = CreatePlayer().GetComponentInChildren<Hero>().Init(_lookTargetPoint);
			}
		}
		
		private Player CreatePlayer() =>
			Instantiate(_playerPrefab);
	}
}