using ArenaHero.Battle.Level;
using UnityEngine;
using UnityEngine.AI;

namespace ArenaHero.Data
{
    [CreateAssetMenu(menuName = "Level/New Level", fileName = "level")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private StageData[] _stageData;

        public StageData GetStageDataByIndex(int index) =>
            _stageData[index];
    }
}