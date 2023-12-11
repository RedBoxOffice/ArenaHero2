using ArenaHero.Saves;
using ArenaHero.Yandex.Saves;
using Reflex.Attributes;
using TMPro;
using UnityEngine;

namespace ArenaHero.Game.UpgradeSystem
{
	public abstract class UpgradeView<TUpgrade> : MonoBehaviour 
		where TUpgrade : UpgradeSave<TUpgrade>
	{
		[SerializeField] private TMP_Text _level;

		private UpgradeModel<TUpgrade> _model;
		private IModelHandler _modelHandler;
		
		[Inject]
		protected void Inject(IModelHandler modelHandler, ISaver saver)
		{
			_model = modelHandler.Get<TUpgrade>();
			
			_level.text = saver.Get<TUpgrade>().Level.ToString();
			
			_model.Upgraded += (upgrade) => _level.text = upgrade.Level.ToString();
		}
	}
}