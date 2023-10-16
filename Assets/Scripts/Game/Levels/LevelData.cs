using UnityEngine;

namespace Game.Level
{
    [CreateAssetMenu(menuName = "Level/New Level", fileName = "level")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private WaveData[] _waveData;
    }
}
