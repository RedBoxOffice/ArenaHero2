using Game.Input;
using Reflex.Attributes;
using System.Collections;
using UnityEngine;

public class KeyboardMovement : MonoBehaviour
{
    [SerializeField] private float _timeToTarget;
    [SerializeField] private float _distanceMove = 5f;
    [SerializeField] private GameObject _target;

    private IInputHandler _inputHandler;
    private Coroutine _moveCoroutine;

    private void OnEnable()
    {
        if (_inputHandler != null)
        {
            _inputHandler.Vertical += OnVertical;
            _inputHandler.Horizontal += OnHorizontal;
        }
    }

    private void OnDisable()
    {
        if (_inputHandler != null)
        {
            _inputHandler.Vertical -= OnVertical;
            _inputHandler.Horizontal -= OnHorizontal;
        }
    }

    [Inject]
    private void Inject(IInputHandler handler)
    {
        _inputHandler = handler;
        _inputHandler.Vertical += OnVertical;
        _inputHandler.Horizontal += OnHorizontal;
    }

    private void OnVertical(float direction)
    {
        if (_moveCoroutine == null)
        {
            Vector3 startPosition = transform.position;

            var targetPosition = transform.position + (Vector3.right * direction * _distanceMove);

            _moveCoroutine = StartCoroutine(Move((currentTime) =>
                transform.position = Vector3.Lerp(startPosition, targetPosition, currentTime / _timeToTarget)));
        }
    }

    private void OnHorizontal(float direction)
    {
        if (_moveCoroutine == null)
        {
            var startAngleTarget = _target.transform.rotation.y;
            var targetTransform = _target.transform;
            var playerPosition = transform.position;

            var distance = Vector3.Distance(playerPosition, targetTransform.position);
            targetTransform.LookAt(playerPosition);
            var angleTarget = (_distanceMove * 360) / (2 * Mathf.PI * distance);

            Vector3 radius = new Vector3()
            {
                z = distance
            };

            _moveCoroutine = StartCoroutine(Move((currentTime) =>
            {
                var angleCurrent = Mathf.Lerp(startAngleTarget, angleTarget, currentTime / _timeToTarget);

                //var targetPosition = targetTransform.position + ((targetTransform.rotation * Vector3.forward) * distance);
                //var position = transform.position;
                //position.x = targetPosition.x;
                //position.z = targetPosition.z;

                Quaternion rotation = targetTransform.rotation;
                rotation.y += angleCurrent * direction;

                targetTransform.rotation = rotation;

                var targetPosition = targetTransform.forward * distance;

                transform.position = targetPosition + targetTransform.position;

                //Debug.Log($"angleCurrent = {((targetTransformRotation * Vector3.forward) * distance)}");
                Debug.Log($"angleCurrent = {targetTransform.rotation}");
            }));
        }
    }

    private IEnumerator Move(System.Action<float> calculatePosition)
    {
        float currentTime = 0;

        while (currentTime < _timeToTarget)
        {
            calculatePosition(currentTime);

            currentTime += Time.deltaTime;

            //transform.LookAt(_target.transform);

            yield return null;
        }

        _moveCoroutine = null;
    }
}
