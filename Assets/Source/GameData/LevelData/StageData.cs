using System;
using ArenaHero.Battle.Level;
using UnityEngine;
using UnityEngine.AI;

namespace ArenaHero.Data
{
	[Serializable]
	public class StageData
	{
		[SerializeField] private WaveData[] _waveData;
		[SerializeField] private GameObject _environmentParent;
		[SerializeField] private SpawnPointsHolder _spawnPointsHolder;
		[SerializeField] private NavMeshData _navMeshData;
		[SerializeField] private PlayerSpawnPoint _playerSpawnPoint;
		[SerializeField] private float _startDelay;
		[SerializeField] private float _delayBetweenWave;

		public float StartDelay => _startDelay;

		public float DelayBetweenWave => _delayBetweenWave;

		public int WaveCount => _waveData.Length;

		public GameObject EnvironmentParent => _environmentParent;

		public SpawnPointsHolder SpawnPointsHolder => _spawnPointsHolder;

		public NavMeshData NavMeshData => _navMeshData;

		public PlayerSpawnPoint PlayerSpawnPoint => _playerSpawnPoint;

		public WaveData GetWaveDataByIndex(int index) =>
			_waveData[index];
	}
}