using System;
using Source.GameData.Characters;

namespace ArenaHero.Battle
{
	public interface ICharacter : IDamageable
	{
		public event Action Died;
		
		public CharacterData Data { get; }
	}
}