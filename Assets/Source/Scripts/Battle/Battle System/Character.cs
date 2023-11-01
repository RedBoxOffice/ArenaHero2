using ArenaHero.Battle.PlayableCharacter.EnemyDetection;
using ArenaHero.Battle.Skills;
using ArenaHero.InputSystem;
using Reflex.Attributes;
using UnityEngine;

namespace ArenaHero.Battle
{
    public class Character : MonoBehaviour, IAttackable, IDamagable
    {
        private IActionsInputHandler _actionsInputHandler;
        private DetectedZone _detectedZone;

        private void Awake()
        {
            _actionsInputHandler = GetComponent<IActionsInputHandler>();
        }

        public void Attack()
        {
            //if (_detectedZone.IsContainEnemy == true)
            //{
            //    _verticalMover.Move(_speedAttack);
            //}
        }

        public void TakeDamage(float damage) => throw new System.NotImplementedException();

        [Inject]
        private void Inject(DetectedZone detectedZone)
        {
            _detectedZone = detectedZone;
            //_inputHandler = targetChangerInject.ActionsInputHandler;
            //_inputHandler.Attack += Attack;
        }
    }
}
