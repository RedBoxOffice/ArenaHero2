using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(menuName = "Level/New Level", fileName = "level")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private WaveData[] _waveData;

        public WaveData GetWaveData(int index) => _waveData[index];
        public int WaveCount => _waveData.Length;
    }
}
