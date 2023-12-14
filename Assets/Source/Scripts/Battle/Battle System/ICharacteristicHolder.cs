namespace ArenaHero.Battle
{
	public interface ICharacteristicHolder
	{
		public float Health { get; }

		public float Armor { get; }

		public float Damage { get; }

		public float Durability { get; }

		public float Aura { get; }
		
		public float Luck { get; }
	}
}