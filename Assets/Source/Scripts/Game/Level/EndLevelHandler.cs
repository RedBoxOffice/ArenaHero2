using System;
using ArenaHero.Data;
using ArenaHero.Utils.StateMachine;

namespace ArenaHero.Game.Level
{
	public class EndLevelHandler : ISubject
	{
		private int _countAliveEnemies;
		private bool _isWavesEnded;

		public event Action ActionEnded;

		public void OnSpawned(Enemy enemy)
		{
			_countAliveEnemies++;
			enemy.Died += OnDied;
		}

		public void OnWavesEnded()
		{
			_isWavesEnded = true;

			CheckEndLevel();
		}

		private void OnDied(Enemy enemy)
		{
			enemy.Died -= OnDied;
			_countAliveEnemies--;

			CheckEndLevel();
		}

		private void CheckEndLevel()
		{
			if (_isWavesEnded && _countAliveEnemies is 0)
			{
				ActionEnded?.Invoke();
			}
		}
	}
}