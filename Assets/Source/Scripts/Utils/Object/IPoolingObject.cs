using System;
using UnityEngine;

namespace Base.Object
{
    public interface IPoolingObject<TInit>
    {
        public Type SelfType { get; }
        public GameObject SelfGameObject { get; }

        public event Action<IPoolingObject<TInit>> Disable;

        public void Init(TInit init);
    }
}