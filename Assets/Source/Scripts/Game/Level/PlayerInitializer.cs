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
		private static Player _playerInstance;
		
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
			if (_hero == null)
			{
				Init();
			}

			return _hero;
		}

		private void OnStateChanged(Type stateType)
		{
			if (stateType == typeof(EndLevelState))
			{
				DontDestroyOnLoad(_playerInstance.gameObject);
				_playerInstance.gameObject.SetActive(false);
			}
		}
		
		private void Init()
		{
			if (GameDataSaver.Instance.Get<CurrentLevelStage>().Value == 0 && _playerInstance != null)
			{
				Destroy(_playerInstance);
				_playerInstance = null;
			}

			if (_playerInstance == null)
			{
				_playerInstance = CreatePlayer();
			}
			else
			{
				_playerInstance.gameObject.DestroyOnLoad();
				_playerInstance.gameObject.SetActive(true);
			}
			
			if (_hero == null)
			{
				_hero = _playerInstance.GetComponentInChildren<Hero>().Init(_lookTargetPoint);
				_hero.transform.localPosition = Vector3.zero;
				_playerInstance.transform.position = _playerSpawnPoint.gameObject.transform.position;
			}
		}
		
		private Player CreatePlayer() =>
			Instantiate(_playerPrefab);
	}
}