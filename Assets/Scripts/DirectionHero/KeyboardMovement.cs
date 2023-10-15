using Game.Input;
using Reflex.Attributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeyboardMovement : MonoBehaviour
{
    [SerializeField] private float _timeToTarget;
    [SerializeField] private float _distanceMove = 5f;
    [SerializeField] private Enemy _enemy;

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
            _moveCoroutine = StartCoroutine(Move(transform.position + (Vector3.forward * direction * _distanceMove)));
        }
    }

    private void OnHorizontal(float direction)
    {
        if (_moveCoroutine == null)
        {
            _moveCoroutine = StartCoroutine(Move(transform.position + (Vector3.right * direction * _distanceMove)));
        }
    }

    private IEnumerator Move(Vector3 target)
    {
        float currentTime = 0;
        Vector3 startPosition = transform.position;

        while (currentTime < _timeToTarget)
        {
            transform.position = Vector3.Lerp(startPosition, target, currentTime / _timeToTarget);

            currentTime += Time.deltaTime;

            yield return null;
        }

        _moveCoroutine = null;
    }
}
