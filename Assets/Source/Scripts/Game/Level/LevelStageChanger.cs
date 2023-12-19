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

		public EndLevelHandler EndLevelHandler { get; } = new EndLevelHandler();

		public RewardHandler RewardHandler { get; } = new RewardHandler();

		private Scene StageScene => SceneManager.GetSceneByName(StageSceneName);

		public LevelStageChanger(WaveHandler waveHandler, LevelData levelData, Target hero, MonoBehaviour context)
		{
			_waveHandler = waveHandler;
			_levelData = levelData;
			_hero = hero;
			_context = context;
		}

		public void Dispose()
		{
			_unsubscribe?.Invoke();
			NavMesh.RemoveNavMeshData(_instanceNavMesh);

			if (StageScene.isLoaded)
			{
				SceneManager.UnloadSceneAsync(StageScene);
			}
		}

		public void ChangeStage()
		{
			if (StageScene.isLoaded)
			{
				_context.StartCoroutine(WaitAsyncOperation(SceneManager.UnloadSceneAsync(StageScene),
					() => _context.StartCoroutine(WaitAsyncOperation(LoadScene(), InitScene))));

				return;
			}

			_context.StartCoroutine(WaitAsyncOperation(LoadScene(), InitScene));
		}

		private AsyncOperation LoadScene() =>
			SceneManager.LoadSceneAsync(StageSceneName, LoadSceneMode.Additive);

		private void InitScene()
		{
			SceneManager.SetActiveScene(StageScene);

			var currentStageIndex = GameDataSaver.Instance.Get<CurrentLevelStage>().Value;
			var currentStage = _levelData.GetStageDataByIndex(currentStageIndex);

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

			Object.Instantiate(currentStage.EnvironmentParent);

			_instanceNavMesh = NavMesh.AddNavMeshData(currentStage.NavMeshData);

			var baseSceneName = SceneLoader.Instance.GetDebugFightSceneName();
			SceneManager.SetActiveScene(SceneManager.GetSceneByName(baseSceneName));
		}

		private IEnumerator WaitAsyncOperation(AsyncOperation operation, Action endCallback)
		{
			while (operation.isDone is false)
			{
				yield return null;
			}

			endCallback();
		}
	}
}