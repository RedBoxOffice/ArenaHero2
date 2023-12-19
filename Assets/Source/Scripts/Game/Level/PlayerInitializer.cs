using System;
using ArenaHero.Battle.Level;
using ArenaHero.Battle.PlayableCharacter;
using ArenaHero.Utils.Other;
using ArenaHero.Utils.StateMachine;
using ArenaHero.Yandex.SaveSystem;
using ArenaHero.Yandex.SaveSystem.Data;
using Reflex.Attributes;
using UnityEngine;

namespace ArenaHero.Game.Level
{
	public class PlayerInitializer : MonoBehaviour
	{
		[SerializeField] private LookTargetPoint _lookTargetPoint;
		[SerializeField] private Player _playerPrefab;
		[SerializeField] private PlayerSpawnPoint _playerSpawnPoint;

		private Hero _hero;
		private IStateChangeable _stateChangeable;

		public LookTargetPoint LookTargetPoint => _lookTargetPoint;

		[Inject]
		private void Inject(IStateChangeable stateChangeable) =>
			stateChangeable.StateChanged += OnStateChanged;

		public Hero GetHero()
		{
			Init();
			
			return _hero;
		}

		private void OnStateChanged(Type stateType)
		{
			// if (stateType == typeof(EndLevelState))
			// {
			// 	DontDestroyOnLoad(_playerInstance.gameObject);
			// 	_playerInstance.gameObject.SetActive(false);
			// }
		}
		
		private void Init()
		{
			if (_hero == null)
			{
				_hero = CreatePlayer().GetComponentInChildren<Hero>().Init(_lookTargetPoint);
			}
		}
		
		private Player CreatePlayer() =>
			Instantiate(_playerPrefab, _playerSpawnPoint.gameObject.transform.position, Quaternion.identity);
	}
}