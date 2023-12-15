using UnityEngine;

namespace ArenaHero.Utils.Object
{
    public class ObjectFactory<TInstance, TInit>
        where TInstance : MonoBehaviour
    {
        private readonly Transform _parent;

        public ObjectFactory(Transform parent) =>
            _parent = parent;

        public IPoolingObject<TInstance, TInit> GetNewObject(IPoolingObject<TInstance, TInit> prefab)
        {
            var newObject = UnityEngine.Object.Instantiate(prefab.Instance.gameObject, _parent);
            newObject.SetActive(false);
            return newObject.GetComponent<IPoolingObject<TInstance, TInit>>();
        }
    }
}
