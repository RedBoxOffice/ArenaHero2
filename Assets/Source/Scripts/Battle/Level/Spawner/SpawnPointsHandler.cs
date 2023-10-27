using System.Collections.Generic;
using UnityEngine;

namespace ArenaHero.Battle.Level
{
    public class SpawnPointsHandler : MonoBehaviour
    {
        [SerializeField] private List<SpawnPoint> _spawnPoints;

        public Vector3 GetSpawnPosition() =>
            _spawnPoints[Random.Range(0, _spawnPoints.Count)].gameObject.transform.position;
    }
}
