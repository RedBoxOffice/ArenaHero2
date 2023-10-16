using Base.Object;
using System;
using UnityEngine;

namespace Game.Enemy
{
    public class Enemy : MonoBehaviour, IPoolingObject<EnemyInit>
    {
        public Type SelfType => typeof(Enemy);

        public GameObject SelfGameObject => gameObject;

        public event Action<IPoolingObject<EnemyInit>> Disable;

        public void Init(EnemyInit init)
        {

        }
    }
}