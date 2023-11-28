using System;
using UnityEngine;

namespace ArenaHero.Yandex.Saves.Data
{
    [Serializable]
    public class CurrentLevel : SaveData<CurrentLevel>
    {
        [SerializeField] private int _index;

        public int Index => _index;
        
        public CurrentLevel() =>
            _index = 0;

        public CurrentLevel(int index) =>
            _index = Mathf.Clamp(index, 0, int.MaxValue);
        
        public override CurrentLevel Clone() =>
            new CurrentLevel(_index);
        
        protected override void UpdateValue(CurrentLevel value) =>
            _index = value.Index;

        protected override bool Equals(CurrentLevel value) =>
            value.Index.Equals(_index);
    }
}