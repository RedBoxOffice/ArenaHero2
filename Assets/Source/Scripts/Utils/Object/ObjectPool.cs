using System.Collections.Generic;

namespace ArenaHero.Utils.Object
{
    public class ObjectPool<TInit>
    {
        private readonly Dictionary<System.Type, Queue<IPoolingObject<TInit>>> _objects = new Dictionary<System.Type, Queue<IPoolingObject<TInit>>>();

        public void Return(IPoolingObject<TInit> poolingObject)
        {
            poolingObject.Disabled -= Return;

            Add(poolingObject);
        }

        public IPoolingObject<TInit> TryGetObjectByType(System.Type objectType)
        {
            if (_objects.TryGetValue(objectType, out Queue<IPoolingObject<TInit>> playersData))
            {
                if (playersData.Count > 0)
                {
                    var data = playersData.Dequeue();

                    return data;
                }
            }

            return null;
        }

        private void Add(IPoolingObject<TInit> poolingObject)
        {
            if (!_objects.ContainsKey(poolingObject.SelfType))
                AddType(poolingObject.SelfType);

            if (_objects.TryGetValue(poolingObject.SelfType, out Queue<IPoolingObject<TInit>> playersData))
            {
                poolingObject.SelfGameObject.SetActive(false);
                playersData.Enqueue(poolingObject);
            }
        }

        private void AddType(System.Type type) =>
            _objects.Add(type, new Queue<IPoolingObject<TInit>>());
    }
}
