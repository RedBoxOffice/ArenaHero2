using System;
using UnityEngine;

namespace ArenaHero.Utils.Object
{
    public interface IPoolingObject<TInit>
    {
        public abstract Type SelfType { get; }
        public GameObject SelfGameObject { get; }

        public event Action<IPoolingObject<TInit>> Disable;

        public void Init(TInit init);
    }
}