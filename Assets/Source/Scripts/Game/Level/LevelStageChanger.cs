using System;
using System.Collections;
using ArenaHero.Battle;
using ArenaHero.Battle.Level;
using ArenaHero.Data;
using ArenaHero.Debugs;
using ArenaHero.Utils.TypedScenes;
using ArenaHero.Yandex.SaveSystem;
using ArenaHero.Yandex.SaveSystem.Data;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace ArenaHero.Game.Level
{
	public class LevelStageChanger
	{
		private const string StageSceneName = nameof(LevelStageHolderScene);

		private readonly LevelData _levelData;
		private readonly WaveHandler _waveHandler;
		private readonly MonoBehaviour _context;
		private readonly Target _hero;

		private Action _unsubscribe;
		private NavMeshDataInstance _instanceNavMesh;
		private PlayerSpawnPoint _playerSpawnPoint;

		public LevelStageChanger(WaveHandler waveHandler, LevelData levelData, Target hero, MonoBehaviour context)
		{
			_waveHandler = waveHandler;
			_levelData = levelData;
			_hero = hero;
			_context = context;
		}

		public event Action<LevelStageObjectsHolder> StageChanged;

		public EndLevelHandler EndLevelHandler { get; } = new EndLevelHandler();

		public RewardHandler RewardHandler { get; } = new RewardHandler();

		private Scene StageScene => SceneManager.GetSceneByName(StageSceneName);

		public void Dispose()
		{
			_unsubscribe?.Invoke();
			TryRemoveNavMeshData();

			if (StageScene.isLoaded)
			{
				SceneManager.UnloadSceneAsync(StageScene);
			}
		}

		public void ChangeStage()
		{
			if (StageScene.isLoaded)
			{
				ReloadScene();
			}
			else
			{
				LoadScene();
			}
		}

		private void ReloadScene()
		{
			_context.StartCoroutine(WaitAsyncOperation(
				SceneManager.UnloadSceneAsync(StageScene),
				LoadScene));
		}

		private void LoadScene()
		{
			_context.StartCoroutine(WaitAsyncOperation(
				SceneManager.LoadSceneAsync(StageSceneName, LoadSceneMode.Additive),
				InitScene));
		}

		private void InitScene()
		{
			SceneManager.SetActiveScene(StageScene);

			var currentStage = GetStageData();

			SetSpawnConfiguration(currentStage);

			SetEnvironment(currentStage);

			SetNavMeshData(currentStage.NavMeshData);

			SetPlayerSpawnPoint(currentStage);

			var baseSceneName = SceneLoader.Instance.GetDebugFightSceneName();
			SceneManager.SetActiveScene(SceneManager.GetSceneByName(baseSceneName));

			StageChanged?.Invoke(new LevelStageObjectsHolder(_playerSpawnPoint));
		}

		private IEnumerator WaitAsyncOperation(AsyncOperation operation, Action endCallback = null)
		{
			while (operation.isDone is false)
			{
				yield return null;
			}

			endCallback?.Invoke();
		}

		private StageData GetStageData()
		{
			var currentStageIndex = GameDataSaver.Instance.Get<CurrentLevelStage>().Value;

			return _levelData.GetStageDataByIndex(currentStageIndex);
		}

		private void SetPlayerSpawnPoint(StageData currentStage) =>
			_playerSpawnPoint = Object.Instantiate(currentStage.PlayerSpawnPoint);

		private void SetEnvironment(StageData currentStage) =>
			Object.Instantiate(currentStage.EnvironmentParent);

		private void SetSpawnConfiguration(StageData currentStage)
		{
			var spawnerHandler = Object.Instantiate(currentStage.SpawnPointsHandler).gameObject.GetComponent<SpawnerHandler>();

			spawnerHandler.Spawned += RewardHandler.OnSpawned;
			spawnerHandler.Spawned += EndLevelHandler.OnSpawned;
			_waveHandler.WavesEnded += EndLevelHandler.OnWavesEnded;

			_unsubscribe = () =>
			{
				spawnerHandler.Spawned -= RewardHandler.OnSpawned;
				spawnerHandler.Spawned -= EndLevelHandler.OnSpawned;
				_waveHandler.WavesEnded -= EndLevelHandler.OnWavesEnded;
			};

			spawnerHandler.Init(_waveHandler, _hero);
		}

		private void SetNavMeshData(NavMeshData navMeshData)
		{
			TryRemoveNavMeshData();
			_instanceNavMesh = NavMesh.AddNavMeshData(navMeshData);
		}

		private void TryRemoveNavMeshData()
		{
			if (_instanceNavMesh.valid)
			{
				NavMesh.RemoveNavMeshData(_instanceNavMesh);
			}
		}
	}
}