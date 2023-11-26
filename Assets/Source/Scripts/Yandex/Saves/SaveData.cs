using System;

namespace ArenaHero.Yandex.Saves
{
    [Serializable]
    public abstract class SaveData
    {
        public abstract event Action<SaveData> ValueUpdated;

        public abstract void UpdateValue<TData>(TData value, Action successCallback)
            where TData : SaveData;

        public abstract SaveData Clone();
    }
}