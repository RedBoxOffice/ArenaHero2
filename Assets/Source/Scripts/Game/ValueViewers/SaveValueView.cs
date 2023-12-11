using ArenaHero.Yandex.Saves;
using Reflex.Attributes;
using TMPro;
using UnityEngine;

namespace ArenaHero.Game.ValueViewers
{
	public abstract class SaveValueView<TValue, TData> : MonoBehaviour
		where TData : SimpleValueSave<TValue, TData>
	{
		[SerializeField] private TMP_Text _textOutput;

		[Inject]
		protected void Inject(ISaver saver)
		{
			_textOutput.text = saver.Get<TData>().Value.ToString();

			saver.SubscribeValueUpdated<TData>((value) =>
			{
				_textOutput.text = GetOutputFormatted(value);
			});
		}

		protected virtual string GetOutputFormatted(TData value) =>
			value.Value.ToString();
	}
}