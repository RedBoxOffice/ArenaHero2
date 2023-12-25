using System;
using System.Collections.Generic;
using ArenaHero.Battle.CharacteristicHolders;
using ArenaHero.Utils.StateMachine;
using ArenaHero.Yandex.SaveSystem;
using ArenaHero.Yandex.SaveSystem.Data;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter
{
    [RequireComponent(typeof(Character))]
    public class Hero : MonoBehaviour, ITargetHolder, IFeatureHolder, ISubject
    {
        private LookTargetPoint _lookTargetPoint;
        private Character _character;
        private Dictionary<Type, Feature> _features;
        
        public event Action ActionEnded;

        public bool IsDied { get; private set; }

        public Target Target => _lookTargetPoint.Target;

        private void Awake()
        {
            _features = new Dictionary<Type, Feature>
            {
                [typeof(HealthFeature)] = new HealthFeature(GetValue<Health>()),
                [typeof(ArmorFeature)] = new ArmorFeature(GetValue<Armor>()),
                [typeof(DamageFeature)] = new DamageFeature(GetValue<Damage>()),
                [typeof(DurabilityFeature)] = new DurabilityFeature(GetValue<Durability>()),
                [typeof(AuraFeature)] = new AuraFeature(GetValue<Aura>()),
                [typeof(LuckFeature)] = new LuckFeature(GetValue<Luck>()),
            };
        }

        private void OnDisable() =>
            _character.Died -= OnDied;

        public float Get<TFeature>()
        {
            if (_features.ContainsKey(typeof(TFeature)))
            {
                return _features[typeof(TFeature)].Value;
            }

            throw new KeyNotFoundException(nameof(TFeature));
        }

        public void Set<TFeature>(float value)
        {
            if (_features.ContainsKey(typeof(TFeature)))
            {
                _features[typeof(TFeature)].Value = value;
            }
            
            throw new KeyNotFoundException(nameof(TFeature));
        }
        
        public Hero Init(LookTargetPoint lookTargetPoint)
        {
            _lookTargetPoint = lookTargetPoint;

            _character = GetComponent<Character>();
            _character.Died += OnDied;
            
            return this;
        }

        private float GetValue<TData>()
            where TData : UpgradeSave<TData>, new() =>
            GameDataSaver.Instance.Get<TData>().Value;

        private void OnDied()
        {
            IsDied = true;
            ActionEnded?.Invoke();
        }
    }
}
