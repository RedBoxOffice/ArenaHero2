using ArenaHero.Saves;
using Reflex.Attributes;
using TMPro;
using UnityEngine;

namespace ArenaHero.Game.UpgradeSystem
{
	public abstract class UpgradeView<TUpgrade> : MonoBehaviour 
		where TUpgrade : UpgradeSave<TUpgrade>
	{
		[SerializeField] private TMP_Text _level;
		[SerializeField] private TMP_Text _price;

		private UpgradeModel<TUpgrade> _model;
		private IModelHandler _modelHandler;
		
		[Inject]
		protected void Inject(IModelHandler modelHandler)
		{
			Debug.Log("VIEW INJECT");
			_model = modelHandler.Get<TUpgrade>();

			_model.Upgraded += (upgrade) =>
			{
				_level.text = upgrade.Level.ToString();
				//_price.text = upgrade.Price.ToString();
			};
		}
	}
}