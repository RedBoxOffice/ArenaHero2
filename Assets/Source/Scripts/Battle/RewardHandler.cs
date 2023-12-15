using ArenaHero.Battle.Level;
using ArenaHero.Data;

namespace ArenaHero.Battle
{
	public class RewardHandler
	{
		public void OnSpawned(Enemy enemy)
		{
			enemy.Died += OnDied;
		}

		private void OnDied(Enemy enemy)
		{
			enemy.Died -= OnDied;
		}
	}
}