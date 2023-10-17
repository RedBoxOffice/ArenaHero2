using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(menuName = "Level/New Wave", fileName = "wave")]
    public class WaveData : ScriptableObject
    {
        [SerializeField] private Enemy[] _enemies;


    }
}