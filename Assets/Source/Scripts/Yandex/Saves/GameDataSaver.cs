using System;
using System.Collections;
using Agava.YandexGames;
using ArenaHero.Yandex.Saves.Data;
using ArenaHero.Yandex.Simulator;
using UnityEngine;

namespace ArenaHero.Yandex.Saves
{
	public class GameDataSaver : ISaver
	{
		private readonly YandexSimulator _yandexSimulator = new YandexSimulator();
		private readonly Hashtable _saves;

		private GameSaves _gameSaves = new GameSaves();

		public GameDataSaver()
		{
			_saves = new Hashtable
			{
				[typeof(CurrentLevel)] = new Func<CurrentLevel>(() => _gameSaves.CurrentLevel),
				[typeof(CurrentLevelStage)] = new Func<CurrentLevelStage>(() => _gameSaves.CurrentLevelStage),
				[typeof(Money)] = new Func<Money>(() => _gameSaves.Money),
				[typeof(Crystals)] = new Func<Crystals>(() => _gameSaves.Crystals),
				[typeof(Armor)] = new Func<Armor>(() => _gameSaves.Armor),
				[typeof(Aura)] = new Func<Aura>(() => _gameSaves.Aura),
				[typeof(Damage)] = new Func<Damage>(() => _gameSaves.Damage),
				[typeof(Durability)] = new Func<Durability>(() => _gameSaves.Durability),
				[typeof(Health)] = new Func<Health>(() => _gameSaves.Health),
				[typeof(Luck)] = new Func<Luck>(() => _gameSaves.Luck),
				[typeof(CurrentUpgradePrice)] = new Func<CurrentUpgradePrice>(() => _gameSaves.CurrentUpgradePrice),
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
				var saves = JsonUtility.FromJson<GameSaves>(data);
				_gameSaves = saves;
				
			}
		}

		private void Save()
		{
			string save = JsonUtility.ToJson(_gameSaves);

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
			
			throw new ArgumentNullException(typeof(TData).Name, "not contains key");
		}
	}
}