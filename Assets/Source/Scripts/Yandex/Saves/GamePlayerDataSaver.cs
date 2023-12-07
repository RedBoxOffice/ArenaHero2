using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using ArenaHero.Yandex.Saves.Data;
using ArenaHero.Yandex.Simulator;
using Newtonsoft.Json;
using UnityEngine;

namespace ArenaHero.Yandex.Saves
{
	public class GamePlayerDataSaver : ISaver
	{
		private readonly YandexSimulator _yandexSimulator = new YandexSimulator();
		private readonly Hashtable _saves;

		private GameSavesData _gameSavesData = new GameSavesData();

		public GamePlayerDataSaver()
		{
			_saves = new Hashtable
			{
				[typeof(CurrentLevel)] = new Func<CurrentLevel>(() => _gameSavesData.CurrentLevel),
				[typeof(CurrentLevelStage)] = new Func<CurrentLevelStage>(() => _gameSavesData.CurrentLevelStage),
				[typeof(Money)] = new Func<Money>(() => _gameSavesData.Money)
			};
		}

		public TData Get<TData>(TData value = default)
			where TData : SaveData<TData>
		{
			if (CanDoIt<TData>() is false)
				return null;

			return ((Func<TData>)_saves[typeof(TData)])().Clone();
		}

		public void Set<TData>(TData value)
			where TData : SaveData<TData>
		{
			if (CanDoIt<TData>() is false)
				return;

			((Func<TData>)_saves[typeof(TData)])().TryUpdateValue(value, Save);
		}

		public void SubscribeValueUpdated<TData>(Action<TData> observer)
			where TData : SaveData<TData>
		{
			if (CanDoIt<TData>())
			{
				((Func<TData>)_saves[typeof(TData)])().ValueUpdated += observer;
			}
		}

		public void UnsubscribeValueUpdated<TData>(Action<TData> observer)
			where TData : SaveData<TData>
		{
			if (CanDoIt<TData>())
			{
				((Func<TData>)_saves[typeof(TData)])().ValueUpdated -= observer;
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
				var saves = JsonUtility.FromJson<GameSavesData>(data);
				_gameSavesData = saves;
				
			}
		}

		private void Save()
		{
			string save = JsonUtility.ToJson(_gameSavesData);

#if !UNITY_EDITOR
            PlayerAccount.SetCloudSaveData(save);
#else
			_yandexSimulator.Save(save);
#endif
		}

		private bool CanDoIt<TData>()
			where TData : SaveData<TData>
		{
			if (_saves.ContainsKey(typeof(TData)))
			{
				return true;
			}
			else
			{
				throw new ArgumentNullException(typeof(TData).Name, "not contains key");
			}
		}
	}
}