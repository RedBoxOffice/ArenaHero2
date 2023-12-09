using ArenaHero.Saves;

namespace ArenaHero.Game.UpgradeSystem
{
	public interface IModelHandler
	{
		public UpgradeModel<TMultiply> Get<TMultiply>()
			where TMultiply : UpgradeSave<TMultiply>;
	}
}