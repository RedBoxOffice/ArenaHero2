using System;
using ArenaHero.Saves;

namespace ArenaHero.Game.UpgradeSystem
{
	public struct UpgradeNode
	{
		public Improvement Model;
		public Action Update;
	}
}