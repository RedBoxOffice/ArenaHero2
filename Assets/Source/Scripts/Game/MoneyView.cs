using ArenaHero.Yandex.Saves;
using ArenaHero.Yandex.Saves.Data;
using Reflex.Attributes;
using TMPro;
using UnityEngine;

namespace ArenaHero.Game
{
	public class MoneyView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _textOutput;

		[Inject]
		private void Inject(ISaver saver)
		{
			_textOutput.text = saver.Get<Money>().Value.ToString();

			saver.SubscribeValueUpdated<Money>((money) =>
			{
				_textOutput.text = money.Value.ToString();
			});
		}
	}
}