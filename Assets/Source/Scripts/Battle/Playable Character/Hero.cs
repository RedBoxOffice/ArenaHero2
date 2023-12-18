using ArenaHero.Battle.CharacteristicHolders;
using ArenaHero.Saves;
using ArenaHero.Yandex.Saves;
using ArenaHero.Yandex.Saves.Data;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter
{
    public class Hero : MonoBehaviour, ITargetHolder, IHealthHolder, IArmorHolder, ILuckHolder, IDamageHolder, IDurabilityHolder, IAuraHolder
    {
        private LookTargetPoint _lookTargetPoint;

        public Target Target => _lookTargetPoint.Target;

        public float Health => GetValue<Health>();

        public float Armor => GetValue<Armor>();

        public float Damage => GetValue<Damage>();

        public float Durability => GetValue<Durability>();

        public float Aura => GetValue<Aura>();

        public float Luck => GetValue<Luck>();

        public Hero Init(LookTargetPoint lookTargetPoint)
        {
            _lookTargetPoint = lookTargetPoint;

            return this;
        }

        private float GetValue<TData>()
            where TData : UpgradeSave<TData>, new() =>
            GameDataSaver.Instance.Get<TData>().Value;
    }
}
