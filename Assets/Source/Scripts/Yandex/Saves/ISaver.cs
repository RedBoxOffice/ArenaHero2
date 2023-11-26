using System;

namespace ArenaHero.Yandex.Saves
{
    public interface ISaver
    {
        public TData Get<TData>(TData value = default)
            where TData : SaveData;

        public void Set<TData>(TData value = default) 
            where TData : SaveData;

        public void SubscribeValueUpdated<TData>(Action<SaveData> subAction)
            where TData : SaveData;
        
        public void UnsubscribeValueUpdated<TData>(Action<SaveData> unsubAction)
            where TData : SaveData;
    }
}