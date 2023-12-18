using ArenaHero.Data;

namespace ArenaHero.Game.Level
{
	public class RewardHandler
	{
		private int _totalMoney;

		public int TotalMoney => _totalMoney;
		
		public void OnSpawned(Enemy enemy)
		{
			enemy.Died += OnDied;
		}

		private void OnDied(Enemy enemy)
		{
			enemy.Died -= OnDied;

			_totalMoney += enemy.RewardMoney;
		}
	}
}