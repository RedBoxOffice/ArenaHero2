using ArenaHero.Yandex.SaveSystem;

namespace ArenaHero.Game.UpgradeSystem
{
	public interface IModelHandler
	{
		public UpgradeModel<TMultiply> Get<TMultiply>()
			where TMultiply : UpgradeSave<TMultiply>, new();
	}
}