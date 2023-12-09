using UnityEngine;

namespace ArenaHero.Utils.Object
{
    public class ObjectSpawner<TInit>
    {
        private ObjectFactory<TInit> _factory;
        private ObjectPool<TInit> _pool;

        public ObjectSpawner(ObjectFactory<TInit> factory, ObjectPool<TInit> pool)
        {
            _factory = factory;
            _pool = pool;
        }

        public IPoolingObject<TInit> Spawn(IPoolingObject<TInit> poolingObject, TInit init, System.Func<Vector3> getSpawnPosition = null)
        {
            IPoolingObject<TInit> spawningObject = GetObject(poolingObject);

            if (getSpawnPosition != null)
            {
                Vector3 position = getSpawnPosition();
                spawningObject.SelfGameObject.transform.position = position;
            }

            spawningObject.Disabled += _pool.Return;
            
            spawningObject.Init(init);

            spawningObject.SelfGameObject.SetActive(true);

            return spawningObject;
        }

        private IPoolingObject<TInit> GetObject(IPoolingObject<TInit> poolingObject) =>
            _pool.TryGetObjectByType(poolingObject.SelfType) ?? CreateObject(poolingObject);

        private IPoolingObject<TInit> CreateObject(IPoolingObject<TInit> poolingObject) =>
            _factory.GetNewObject(poolingObject);
    }
}
