using System;
using Source.GameData.Characters;
using UnityEngine;

namespace ArenaHero.Battle
{
	public interface ICharacter : IDamageable
	{
		public event Action Died;
		
		public CharacterData Data { get; }
		
		public Vector3 Position { get; }
	}
}