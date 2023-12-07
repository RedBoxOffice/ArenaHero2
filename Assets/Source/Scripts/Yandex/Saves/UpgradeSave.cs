using System;
using ArenaHero.Yandex.Saves;
using UnityEngine;

namespace ArenaHero.Saves
{
	[Serializable]
	public abstract class UpgradeSave<TData> : SaveData<TData> where TData : UpgradeSave<TData>
	{
		[SerializeField] private float _multiply;
		[SerializeField] private int _level;
		[SerializeField] private int _price;

		public float Multiply { get => _multiply; protected set => _multiply = value; }
		
		public int Level { get => _level; protected set => _level = value; }
		
		public int Price { get => _price; protected set => _price = value; }

		public override abstract TData Clone();

		protected override void UpdateValue(TData value)
		{
			_multiply = value.Multiply;
			_level = value.Level;
			_price = value.Price;
		}

		protected override bool Equals(TData value) =>
			value.Multiply.Equals(_multiply)
			&& value.Level.Equals(_level)
			&& value.Price.Equals(_price);
	}
}