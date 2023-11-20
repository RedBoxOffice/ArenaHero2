using UnityEngine;

namespace Source.GameData.Characters
{
	[CreateAssetMenu(menuName = "Characters/new Character", fileName = "character")]
	public class CharacterData : ScriptableObject
	{
		[SerializeField] private float _maxHealth;
		[SerializeField] private float _baseDamage;
		[SerializeField] private float _attackDistance;
		[SerializeField] private float _attackCooldown;

		public float MaxHealth => _maxHealth;

		public float BaseDamage => _baseDamage;

		public float AttackDistance => _attackDistance;

		public float AttackCooldown => _attackCooldown;
	}
}