using ArenaHero.Data;
using ArenaHero.Utils.Object;
using System;
using UnityEngine;

namespace ArenaHero.Fight.Enemies.EnemyTypes
{
    public class AngryCapsule : Enemy, IPoolingObject<EnemyInit>
    {
        public Type SelfType => typeof(Enemy);

        public GameObject SelfGameObject => gameObject;

        public event Action<IPoolingObject<EnemyInit>> Disable;

        public void Init(EnemyInit init)
        {

        }
    }
}
