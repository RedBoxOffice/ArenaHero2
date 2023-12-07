using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ArenaHero.Yandex.Saves.Data
{
    [Serializable]
    public class CurrentLevel : IntSave<CurrentLevel>
    {
        public CurrentLevel() =>
            Init(0);

        public CurrentLevel(int value) =>
            Init(value);
        
        public override CurrentLevel Clone() =>
            new CurrentLevel(Value);
        
        private void Init(int value) =>
            Value = Mathf.Clamp(value, 0, int.MaxValue);
    }
}