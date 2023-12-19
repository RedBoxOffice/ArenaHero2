using System;
using ArenaHero.Battle.CharacteristicHolders;
using ArenaHero.Utils.StateMachine;
using ArenaHero.Yandex.SaveSystem;
using ArenaHero.Yandex.SaveSystem.Data;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter
{
    [RequireComponent(typeof(Character))]
    public class Hero : MonoBehaviour, ITargetHolder, IHealthHolder, IArmorHolder, ILuckHolder, IDamageHolder, IDurabilityHolder, IAuraHolder, ISubject
    {
        private LookTargetPoint _lookTargetPoint;
        private Character _character;
        
        public event Action ActionEnded;

        public Target Target => _lookTargetPoint.Target;

        public float Health => GetValue<Health>();

        public float Armor => GetValue<Armor>();

        public float Damage => GetValue<Damage>();

        public float Durability => GetValue<Durability>();

        public float Aura => GetValue<Aura>();

        public float Luck => GetValue<Luck>();

        private void OnDisable() =>
            _character.Died -= OnDied;

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

        private void OnDied() =>
            ActionEnded?.Invoke();
    }
}
