using ArenaHero.Data;

namespace ArenaHero.Game.Level
{
	public class RewardHandler
	{
		private int _totalMoney;
		private int _totalKillScore;

		public int TotalMoney => _totalMoney;

		public int TotalKillScore => _totalKillScore;

		public void OnSpawned(Enemy enemy)
		{
			enemy.Died += OnDied;
		}

		private void OnDied(Enemy enemy)
		{
			enemy.Died -= OnDied;

			_totalMoney += enemy.RewardMoney;
			_totalKillScore += enemy.RewardScore;
		}
	}
}