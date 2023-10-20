using ArenaHero.Utils.Object;
using System;
using UnityEngine;

namespace ArenaHero.Data
{
    public abstract class Enemy : MonoBehaviour, IPoolingObject<EnemyInit>
    {
        public GameObject SelfGameObject => gameObject;
        public Transform Target { get; private set; }

        public abstract Type SelfType { get; }

        public event Action<IPoolingObject<EnemyInit>> Disable;

        public void Init(EnemyInit init)
        {
            Target = init.Target;
        }
    }
}