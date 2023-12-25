namespace ArenaHero.Battle.CharacteristicHolders
{
	public interface IFeatureHolder
	{
		public float Get<TFeature>();

		public void Set<TFeature>(float value);
	}
}