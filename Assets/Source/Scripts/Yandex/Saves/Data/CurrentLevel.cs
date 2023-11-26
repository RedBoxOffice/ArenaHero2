using System;
using UnityEngine;

namespace ArenaHero.Yandex.Saves.Data
{
    [Serializable]
    public class CurrentLevel : SaveData
    {
        [SerializeField] private int _index;

        public int Index => _index;
        
        public CurrentLevel() =>
            _index = 0;

        public CurrentLevel(int index) =>
            _index = Mathf.Clamp(index, 0, int.MaxValue);

        public override event Action<SaveData> ValueUpdated;

        public override SaveData Clone() =>
            new CurrentLevel(_index);

        public override void UpdateValue<TData>(TData value, Action successCallback)
        {
            if (value is CurrentLevel level)
            {
                _index = level.Index;
                successCallback();
                ValueUpdated?.Invoke(Clone());
            }
        }
    }
}