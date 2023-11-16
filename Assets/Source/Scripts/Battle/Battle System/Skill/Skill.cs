using UnityEngine;

namespace ArenaHero.Battle.Skills
{
	public abstract class Skill : MonoBehaviour
	{
		[SerializeField] private CharacterType _triggerCharacterType;

		protected CharacterType TriggerCharacterType => _triggerCharacterType;
	}
}