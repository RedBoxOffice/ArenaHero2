using System;
using Agava.YandexGames;
using ArenaHero.Yandex.Simulator;
using UnityEngine;

namespace ArenaHero.Yandex.SaveSystem
{
	public class GameDataSaver : ISaver
	{
		public static ISaver Instance { get; private set; }

		private readonly YandexSimulator _yandexSimulator = new YandexSimulator();

		private GameSaves _gameSaves = new GameSaves();

		public GameDataSaver() =>
			Instance ??= this;

		public TData Get<TData>()
			where TData : SaveData<TData>, new() =>
			_gameSaves.Get<TData>().Clone();

		public void Set<TData>(TData value)
			where TData : SaveData<TData>, new() =>
			_gameSaves.Get<TData>().TryUpdateValue(value, Save);

		public void SubscribeValueUpdated<TData>(Action<TData> observer)
			where TData : SaveData<TData>, new() =>
			_gameSaves.Get<TData>().ValueUpdated += observer;

		public void UnsubscribeValueUpdated<TData>(Action<TData> observer)
			where TData : SaveData<TData>, new() =>
			_gameSaves.Get<TData>().ValueUpdated -= observer;

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
	}
}