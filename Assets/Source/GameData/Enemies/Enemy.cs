using System;
using ArenaHero.Battle;
using ArenaHero.Utils.Object;
using UnityEngine;

namespace ArenaHero.Data
{
    public abstract class Enemy : MonoBehaviour, IPoolingObject<EnemyInit>, ITargetHandler
    {
        private ICharacter _character;
        
        public event Action<Enemy> Disabling;
        
        public event Action<IPoolingObject<EnemyInit>> Disabled;

        public GameObject SelfGameObject => gameObject;

        public IDamageable SelfDamageable { get; private set; }

        public Target Target { get; private set; }

        public abstract Type SelfType { get; }

        private void Awake()
        {
            SelfDamageable = GetComponent<IDamageable>();
            _character = GetComponent<ICharacter>();
        }

        private void OnEnable() =>
            _character.Died += OnDied;

        private void OnDisable()
        {
            Disabled?.Invoke(this);
            _character.Died -= OnDied;
        }
        
        public void Init(EnemyInit init) =>
            Target = init.Target;

        private void OnDied()
        {
            Disabling?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}