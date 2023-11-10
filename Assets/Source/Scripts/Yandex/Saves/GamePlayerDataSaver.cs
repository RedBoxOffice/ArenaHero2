using System;
using System.Collections;
using Agava.YandexGames;
using ArenaHero.Yandex.Saves.Data;
using ArenaHero.Yandex.Simulator;
using UnityEngine;

namespace ArenaHero.Yandex.Saves
{
    public class GamePlayerDataSaver : ISaver
    {
        private readonly PlayerData _playerData = new PlayerData();
        private readonly YandexSimulator _yandexSimulator = new YandexSimulator();
        private readonly Hashtable _accessMethodsHolders;
        private readonly Hashtable _playerDataEvents;

        public GamePlayerDataSaver()
        {
            _playerDataEvents = new Hashtable();

            _accessMethodsHolders = new Hashtable()
            {
                [typeof(CurrentLevel)] = new SaveAccessMethodsHolder<CurrentLevel>(
                    getter: (_) => _playerData.CurrentLevel,
                    setter: (value) =>
                    {
                        if (value == default(CurrentLevel))
                            throw new ArgumentNullException(nameof(value));

                        if (_playerData.CurrentLevel.Index == value.Index)
                            return;
                        
                        _playerData.CurrentLevel = value;
                        Save((Action<CurrentLevel>)_playerDataEvents[typeof(CurrentLevel)], value);
                    }
                )
            };
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
#if !UNITY_EDITOR
            PlayerAccount.GetCloudSaveData(OnSuccessCallback);
#else
            _yandexSimulator.Init(OnSuccessCallback);
#endif

            return;

            void OnSuccessCallback(string data)
            {
                var playerData = JsonUtility.FromJson<PlayerData>(data);

                Set(playerData.CurrentLevel);
            }
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