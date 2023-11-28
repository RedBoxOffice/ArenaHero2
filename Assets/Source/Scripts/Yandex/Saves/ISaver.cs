using System;

namespace ArenaHero.Yandex.Saves
{
    public interface ISaver
    {
        public TData Get<TData>(TData value = default)
            where TData : SaveData<TData>;

        public void Set<TData>(TData value = default) 
            where TData : SaveData<TData>;

        public void SubscribeValueUpdated<TData>(Action<TData> subAction)
            where TData : SaveData<TData>;
        
        public void UnsubscribeValueUpdated<TData>(Action<TData> unsubAction)
            where TData : SaveData<TData>;
    }
}