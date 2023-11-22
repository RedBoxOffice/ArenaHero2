using ArenaHero.Utils.Object;
using System;
using System.Collections.Generic;
using ArenaHero.Battle;
using UnityEngine;

namespace ArenaHero.Data
{
    public abstract class Enemy : MonoBehaviour, IPoolingObject<EnemyInit>, ITargetHandler
    {
        [SerializeField] private MonoBehaviour _damagable;

        private IMover[] _movers;

        public IReadOnlyCollection<IMover> Movers => _movers;
         
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

            if (_damagable == null)
                throw new NullReferenceException(nameof(_damagable));

            _movers = GetComponents<IMover>();
        }

        private void Start() =>
            SelfDamagable = (IDamagable)_damagable;

        private void OnDisable()
        {
            Dead?.Invoke(this);
            Disable?.Invoke(this);
        }
        
        public void Init(EnemyInit init) =>
            Target = init.Target;
    }
}