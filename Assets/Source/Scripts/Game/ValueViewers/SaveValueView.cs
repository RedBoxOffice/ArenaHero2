using ArenaHero.Yandex.SaveSystem;
using Reflex.Attributes;
using TMPro;
using UnityEngine;

namespace ArenaHero.Game.ValueViewers
{
	public abstract class SaveValueView<TValue, TData> : MonoBehaviour
		where TData : SimpleValueSave<TValue, TData>, new()
	{
		[SerializeField] private TMP_Text _textOutput;

		private void Awake()
		{
			_textOutput.text = GameDataSaver.Instance.Get<TData>().Value.ToString();

			GameDataSaver.Instance.SubscribeValueUpdated<TData>((value) =>
			{
				_textOutput.text = GetOutputFormatted(value);
			});
		}

		protected virtual string GetOutputFormatted(TData value) =>
			value.Value.ToString();
	}
}