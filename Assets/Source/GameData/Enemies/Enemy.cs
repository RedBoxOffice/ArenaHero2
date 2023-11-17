using ArenaHero.Utils.Object;
using System;
using ArenaHero.Battle;
using UnityEngine;

namespace ArenaHero.Data
{
    public abstract class Enemy : MonoBehaviour, IPoolingObject<EnemyInit>, ITargetHandler
    {
        [SerializeField] private MonoBehaviour _damagable;
        
        public GameObject SelfGameObject => gameObject;

        public IDamagable SelfDamagable { get; private set; }

        public Target Target { get; private set; }

        public abstract Type SelfType { get; }

        public event Action<Enemy> Dead; 
        public event Action<IPoolingObject<EnemyInit>> Disable;

        private void OnValidate()
        {
            if (_damagable && _damagable is not IDamagable)
            {
                Debug.LogError(nameof(_damagable) + " needs to implement " + nameof(IDamagable));
                _damagable = null;
            }
            else
            {
                SelfDamagable = (IDamagable)_damagable;
            }

            if (_damagable == null)
                throw new NullReferenceException(nameof(_damagable));
        }
        
        public void Init(EnemyInit init) =>
            Target = init.Target;

        private void OnDisable()
        {
            Dead?.Invoke(this);
            Disable?.Invoke(this);
        }
    }
}