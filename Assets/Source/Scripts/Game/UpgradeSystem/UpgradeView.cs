using ArenaHero.Saves;
using ArenaHero.Yandex.Saves;
using Reflex.Attributes;
using TMPro;
using UnityEngine;

namespace ArenaHero.Game.UpgradeSystem
{
	public abstract class UpgradeView<TUpgrade> : MonoBehaviour
		where TUpgrade : UpgradeSave<TUpgrade>, new()
	{
		[SerializeField] private TMP_Text _levelOutput;
		[SerializeField] private TMP_Text _valueOutput;

		private UpgradeModel<TUpgrade> _model;
		private IModelHandler _modelHandler;

		[Inject]
		protected void Inject(IModelHandler modelHandler)
		{
			_model = modelHandler.Get<TUpgrade>();

			_levelOutput.text = GameDataSaver.Instance.Get<TUpgrade>().Level.ToString();
			_valueOutput.text = GameDataSaver.Instance.Get<TUpgrade>().Value.ToString("0.00");

			_model.Upgraded += (upgrade) =>
			{
				_levelOutput.text = upgrade.Level.ToString();
				_valueOutput.text = upgrade.Value.ToString("0.00");
			};
		}
	}
}