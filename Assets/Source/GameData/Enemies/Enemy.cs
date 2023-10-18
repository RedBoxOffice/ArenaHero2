using ArenaHero.Utils.Object;
using System;
using UnityEngine;
using UnityEngine.Pool;

namespace ArenaHero.Data
{
    public abstract class Enemy : MonoBehaviour, IPoolingObject<EnemyInit>
    {
        public GameObject SelfGameObject => gameObject;

        public abstract Type SelfType { get; }

        public event Action<IPoolingObject<EnemyInit>> Disable;

        public void Init(EnemyInit init)
        {

        }
    }
}