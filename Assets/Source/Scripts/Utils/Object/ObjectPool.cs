using System.Collections.Generic;
using UnityEngine;

namespace ArenaHero.Utils.Object
{
    public class ObjectPool<TInstance, TInit>
        where TInstance : MonoBehaviour
    {
        private readonly Dictionary<System.Type, Queue<IPoolingObject<TInstance, TInit>>> _objects = new Dictionary<System.Type, Queue<IPoolingObject<TInstance, TInit>>>();

        public void Return(IPoolingObject<TInstance, TInit> poolingObject)
        {
            poolingObject.Disabled -= Return;

            Add(poolingObject);
        }

        public IPoolingObject<TInstance, TInit> TryGetObjectByType(System.Type objectType)
        {
            if (_objects.TryGetValue(objectType, out Queue<IPoolingObject<TInstance, TInit>> playersData))
            {
                if (playersData.Count > 0)
                {
                    var data = playersData.Dequeue();

                    return data;
                }
            }

            return null;
        }

        private void Add(IPoolingObject<TInstance, TInit> poolingObject)
        {
            if (!_objects.ContainsKey(poolingObject.SelfType))
                AddType(poolingObject.SelfType);

            if (_objects.TryGetValue(poolingObject.SelfType, out Queue<IPoolingObject<TInstance, TInit>> playersData))
            {
                poolingObject.Instance.gameObject.SetActive(false);
                playersData.Enqueue(poolingObject);
            }
        }

        private void AddType(System.Type type) =>
            _objects.Add(type, new Queue<IPoolingObject<TInstance, TInit>>());
    }
}
