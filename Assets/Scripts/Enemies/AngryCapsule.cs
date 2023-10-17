﻿using Base.Object;
using GameData;
using System;
using UnityEngine;

namespace Game.Enemies
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