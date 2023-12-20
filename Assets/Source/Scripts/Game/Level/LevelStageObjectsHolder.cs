using ArenaHero.Battle.Level;

namespace ArenaHero.Game.Level
{
	public readonly struct LevelStageObjectsHolder
	{
		public readonly PlayerSpawnPoint PlayerSpawnPoint;

		public LevelStageObjectsHolder(PlayerSpawnPoint playerSpawnPoint) =>
			PlayerSpawnPoint = playerSpawnPoint;
	}
}