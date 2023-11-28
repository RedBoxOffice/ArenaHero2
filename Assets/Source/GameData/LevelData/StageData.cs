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
		[SerializeField] private SpawnPointsHandler _spawnPointsHandler;
		[SerializeField] private NavMeshData _navMeshData;

		public int WaveCount => _waveData.Length;
		
		public GameObject EnvironmentParent => _environmentParent;
		
		public SpawnPointsHandler SpawnPointsHandler => _spawnPointsHandler;
		
		public NavMeshData NavMeshData => _navMeshData;
		
		public WaveData GetWaveDataByIndex(int index) => 
			_waveData[index];
	}
}