using Agava.YandexGames;
using ArenaHero.Yandex.Saves.Data;
using ArenaHero.Yandex.Simulator;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace ArenaHero.Yandex.Saves
{
    public class GamePlayerDataSaver : ISaver
    {
        private PlayerData _playerData = new();
        private YandexSimulator _yandexSimulator = new();
        private Hashtable _accessMethodsHolders;
        private Hashtable _playerDataEvents;

        public GamePlayerDataSaver()
        {
            _playerDataEvents = new Hashtable();

            _accessMethodsHolders = new Hashtable()
            {
                [typeof(CurrentLevel)] = new SaveAccessMethodsHolder<CurrentLevel>(
                    getter: (_) => _playerData.CurrentLevel,
                    setter: (value) =>
                    {
                        if (value == default)
                            throw new ArgumentNullException(nameof(value));

                        if (_playerData.CurrentLevel.Index != value.Index)
                        {
                            _playerData.CurrentLevel = value;
                            Save((Action<CurrentLevel>)_playerDataEvents[typeof(CurrentLevel)], value);
                        }
                    })
            };
        }

        [Serializable]
        private class PlayerData
        {
            public CurrentLevel CurrentLevel = new(4);
        }

        public TData Get<TData>(TData value = default) where TData : class, IPlayerData
        {
            if (_accessMethodsHolders.ContainsKey(typeof(TData)))
            {
                var holder = (SaveAccessMethodsHolder<TData>)_accessMethodsHolders[typeof(TData)];
                return holder.Getter(value);
            }
            else
            {
                throw new ArgumentNullException($"{nameof(GamePlayerDataSaver)} GET DATA");
            }
        }

        public void Set<TData>(TData value = default) where TData : class, IPlayerData
        {
            if (_accessMethodsHolders.ContainsKey(typeof(TData)))
            {
                var holder = (SaveAccessMethodsHolder<TData>)_accessMethodsHolders[typeof(TData)];
                holder.Setter(value);
            }
            else
            {
                throw new ArgumentNullException($"{nameof(GamePlayerDataSaver)} SET DATA");
            }
        }

        public void SubscribeValueUpdated<TData>(Action<TData> subAction) where TData : class, IPlayerData
        {
            if (_playerDataEvents.ContainsKey(typeof(TData)))
            {
                var action = (Action<TData>)_playerDataEvents[typeof(TData)];

                action += subAction;
                _playerDataEvents[typeof(TData)] = action;
            }
            else
            {
                _playerDataEvents.Add(typeof(TData), subAction);
            }
        }

        public void UnsubscribeValueUpdated<TData>(Action<TData> subAction) where TData : class, IPlayerData
        {
            if (_playerDataEvents.ContainsKey(typeof(TData)))
            {
                var action = (Action<TData>)_playerDataEvents[typeof(TData)];

                action -= subAction;
                _playerDataEvents[typeof(TData)] = action;
            }
            else
            {
                throw new ArgumentNullException($"{nameof(GamePlayerDataSaver)} VALUE UPDATED");
            }
        }

        public void Init()
        {
            void onSuccessCallback(string data)
            {
                var playerData = JsonUtility.FromJson<PlayerData>(data);

                Set(playerData.CurrentLevel);
            }

#if !UNITY_EDITOR
            PlayerAccount.GetCloudSaveData(onSuccessCallback);
#else
            _yandexSimulator.Init(onSuccessCallback);
#endif
        }

        private void Save()
        {
            string save = JsonUtility.ToJson(_playerData);
#if !UNITY_EDITOR
            PlayerAccount.SetCloudSaveData(save);
#else
            _yandexSimulator.Save(save);
#endif
        }

        private void Save<T>(Action<T> saved, T valueCallback)
        {
            Save();
            saved?.Invoke(valueCallback);
        }
    }
}