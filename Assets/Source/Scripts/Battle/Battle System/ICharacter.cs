using UnityEngine;

namespace ArenaHero.Battle
{
	public interface ICharacter : IDamagable
	{
		public CharacterType Type { get; }
		
		public Vector3 Position { get; }
	}
}