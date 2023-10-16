using UnityEngine;
using Game.Entity;

namespace Game.Level
{
    [CreateAssetMenu(menuName = "Level/New Wave", fileName = "wave")]
    public class WaveData : ScriptableObject
    {
        [SerializeField] private Enemy[] _enemies;


    }
}