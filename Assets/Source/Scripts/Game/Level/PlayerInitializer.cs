using ArenaHero.Battle.Level;
using ArenaHero.Battle.PlayableCharacter;
using UnityEngine;

namespace ArenaHero.Game.Level
{
	public class PlayerInitializer : MonoBehaviour
	{
		[SerializeField] private LookTargetPoint _lookTargetPoint;
		[SerializeField] private Hero _heroPrefab;

		private Hero _hero;

		public LookTargetPoint LookTargetPoint => _lookTargetPoint;

		public void OnStageChanged(LevelStageObjectsHolder holder) =>
			GetHero().transform.position = holder.PlayerSpawnPoint.transform.position;

		public Hero GetHero()
		{
			Init();
			
			return _hero;
		}
		
		private void Init()
		{
			if (_hero == null)
			{
				_hero = CreateHero().Init(_lookTargetPoint);
			}
		}
		
		private Hero CreateHero() =>
			Instantiate(_heroPrefab);
	}
}