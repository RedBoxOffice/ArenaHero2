using Source.GameData.Characters;
using UnityEngine;

namespace ArenaHero.Battle
{
	public interface ICharacter : IDamagable
	{
		public CharacterData Data { get; }
		
		public Vector3 Position { get; }
	}
}