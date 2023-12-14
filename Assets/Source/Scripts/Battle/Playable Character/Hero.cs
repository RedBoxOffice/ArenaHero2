using ArenaHero.Yandex.Saves;
using ArenaHero.Yandex.Saves.Data;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter
{
    public class Hero : MonoBehaviour, ITargetHolder, ICharacteristicHolder
    {
        private LookTargetPoint _lookTargetPoint;
        
        public Target Target => _lookTargetPoint.Target;

        public float Health => GameDataSaver.Instance.Get<Health>().Value;

        public float Armor => GameDataSaver.Instance.Get<Armor>().Value;

        public float Damage => GameDataSaver.Instance.Get<Damage>().Value;

        public float Durability => GameDataSaver.Instance.Get<Durability>().Value;

        public float Aura => GameDataSaver.Instance.Get<Aura>().Value;

        public float Luck => GameDataSaver.Instance.Get<Luck>().Value;

        public Hero Init(LookTargetPoint lookTargetPoint)
        {
            _lookTargetPoint = lookTargetPoint;
            
            return this;
        }
    }
}
