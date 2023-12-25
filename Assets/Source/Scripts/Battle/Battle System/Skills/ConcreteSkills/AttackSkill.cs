using ArenaHero.Battle.CharacteristicHolders;
using ArenaHero.InputSystem;
using UnityEngine;

namespace ArenaHero.Battle.Skills
{
	public abstract class AttackSkill : Skill
	{
		[SerializeField] private Character _character;
		[SerializeField] private float _attackDistance;
		[SerializeField] private float _attackCooldown;
		
		private IActionsInputHandler _inputHandler;
		
		public float AttackDistance => _attackDistance;

		protected float AttackCooldown => _attackCooldown;

		protected Character Character => _character;

		protected ITargetHolder TargetHolder { get; private set; }

		protected IFeatureHolder FeatureHolder { get; private set; }

		protected virtual void Start()
		{
			TargetHolder = _character.GetComponent<ITargetHolder>();
			FeatureHolder = _character.GetComponent<IFeatureHolder>();
			_inputHandler = _character.GetComponent<IActionsInputHandler>();
			
			_inputHandler.Attack += OnAttack;
		}
		
		private void OnEnable()
		{
			if (_inputHandler != null)
			{
				_inputHandler.Attack += OnAttack;
			}
		}

		private void OnDisable()
		{
			if (_inputHandler != null)
			{
				_inputHandler.Attack -= OnAttack;
			}
		}

		protected abstract void OnAttack();
	}
}