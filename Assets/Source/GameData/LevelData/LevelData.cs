using ArenaHero.Battle.Level;
using UnityEngine;

namespace ArenaHero.Data
{
    [CreateAssetMenu(menuName = "Level/New Level", fileName = "level")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private WaveData[] _waveData;
        [SerializeField] private GameObject _environmentParent;
        [SerializeField] private SpawnPointsHandler _spawnPointsHandler;

        public WaveData GetWaveData(int index) => _waveData[index];
        public int WaveCount => _waveData.Length;
    }
}
