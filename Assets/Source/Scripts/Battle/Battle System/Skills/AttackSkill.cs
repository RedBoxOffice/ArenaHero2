using ArenaHero.InputSystem;
using Source.GameData.Characters;
using UnityEngine;

namespace ArenaHero.Battle.Skills
{
	public abstract class AttackSkill : Skill
	{
		[SerializeField] private Character _character;
		
		private IActionsInputHandler _inputHandler;
		private ITargetHandler _targetHandler;
		
		protected Target Target => _targetHandler.Target;

		protected Character Character => _character;
		
		protected CharacterData CharacterData => _character.Data;

		protected virtual void Start()
		{
			_targetHandler = _character.GetComponent<ITargetHandler>();
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