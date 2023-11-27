using ArenaHero.Utils.Object;
using System;
using System.Collections.Generic;
using ArenaHero.Battle;
using UnityEngine;

namespace ArenaHero.Data
{
    public abstract class Enemy : MonoBehaviour, IPoolingObject<EnemyInit>, ITargetHandler
    {
        private ICharacter _character;
        
        public GameObject SelfGameObject => gameObject;

        public ICharacter SelfCharacter => _character;
        
        public IDamagable SelfDamagable { get; private set; }

        public Target Target { get; private set; }

        public abstract Type SelfType { get; }
        
        public event Action<IPoolingObject<EnemyInit>> Disable;

        private void Awake()
        {
            SelfDamagable = GetComponent<IDamagable>();
            _character = GetComponent<ICharacter>();
        }

        private void OnEnable() =>
            _character.Died += OnDied;

        private void OnDisable()
        {
            Disable?.Invoke(this);
            _character.Died -= OnDied;
        }

        private void OnDied() =>
            gameObject.SetActive(false);

        public void Init(EnemyInit init) =>
            Target = init.Target;
    }
}