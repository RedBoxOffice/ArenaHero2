using ArenaHero.Battle.Skills;
using ArenaHero.InputSystem;
using Reflex.Attributes;
using System;
using System.Collections;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class HeroMover : MonoBehaviour
    {
        [SerializeField] private float _timeToTarget;
        [SerializeField] private Transform _target;
        [SerializeField] private Rigidbody _selfRigidbody;

        protected Coroutine MoveCoroutine;
        protected IMovementInputHandler InputHandler;

        private DefaultAttackSkill _defaultAttackSkill;

        [Inject]
        private void Inject(DefaultAttackSkill defaultAttackSkill)
        {
            _defaultAttackSkill = defaultAttackSkill;
            _defaultAttackSkill.TargetReach += OnMove;
        }

        private void OnDisable()
        {
            _defaultAttackSkill.TargetReach -= OnMove;
        }

        protected Transform Target => _target;
        
        protected Rigidbody SelfRigidbody => _selfRigidbody;

        public void Init(LookTargetPoint lookTargetPoint) =>
            _target = lookTargetPoint.transform;
        
        protected virtual void OnMove(float direction) =>
            OnMove(direction, null);

        protected abstract void OnMove(float direction, Action callBack);        

        protected void LookTarget()
        {
            var offset = Target.position - SelfRigidbody.position;
            offset.Set(offset.x, 0, offset.z);
            SelfRigidbody.MoveRotation(Quaternion.Euler(0f, Vector3.SignedAngle(Vector3.forward, offset, Vector3.up), 0f));
        }

        protected IEnumerator Move(Func<bool> canMove, Func<float, Vector3> calculatePosition, Action endMoveCallBack = null, Action noCanMoveCallBack = null)
        {
            float currentTime = 0;

            while (currentTime <= _timeToTarget)
            {
                if (!canMove())
                {
                    noCanMoveCallBack?.Invoke();
                    break;
                }

                SelfRigidbody.MovePosition(calculatePosition(currentTime / _timeToTarget));

                currentTime += Time.fixedDeltaTime;

                yield return new WaitForFixedUpdate();
            }

            endMoveCallBack?.Invoke();

            MoveCoroutine = null;
        }
    }
}