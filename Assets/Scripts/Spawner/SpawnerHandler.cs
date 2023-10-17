using Base.Object;
using GameData;
using UnityEngine;

namespace Game.Enemies
{
    public class SpawnerHandler : MonoBehaviour
    {
        private ObjectSpawner<EnemyInit> _spawner = new(
                new ObjectFactory<EnemyInit>(new GameObject(nameof(EnemyInit)).transform),
                new ObjectPool<EnemyInit>());
    }
}
