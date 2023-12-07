using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaHero.Yandex.Saves.Data
{
    public class Money : SaveData<Money>
    {
        [SerializeField] private int _value;

        public int Value => _value;

        public Money() =>
            _value = 1000;

        public Money(int value) =>
            _value = Mathf.Clamp(value, 0, int.MaxValue);

        public override Money Clone() =>
            new Money(_value);

        protected override void UpdateValue(Money value) =>
            _value = value.Value;

        protected override bool Equals(Money value) =>
            value.Value.Equals(_value);
    }

}
