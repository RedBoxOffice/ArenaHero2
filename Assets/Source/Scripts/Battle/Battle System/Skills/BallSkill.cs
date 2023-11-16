using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaHero.Battle.Skills
{
    public class BallSkill : Skill, IUpdatableSkill 
    {
        [SerializeField] private Transform _centerTransform;
        [SerializeField] private GameObject _elementPrefab;
        [SerializeField, Range(1, 20)] private int _countElements;
        [SerializeField] private float _radius;
        [SerializeField] private float _speedRotation;
        [SerializeField, Range(-1, 1)] private int _directionAllRotation;

        private readonly List<GameObject> _elements = new List<GameObject>();
        
        private float _currentAngleAllRotation = 0.01f;
        private Coroutine _updateCoroutine;
        private bool _isAlive;

        private void OnValidate()
        {
            if (_directionAllRotation == 0)
                _directionAllRotation = 1;
        }
        
        private void OnEnable()
        {
            _isAlive = true;

            UpdateSkill();
        }

        private void OnDisable() =>
            DeleteElements();

        public void UpdateSkill()
        {
            if (_updateCoroutine != null)
                StopCoroutine(_updateCoroutine);

            DeleteElements();
            
            CreateElements();

            SetPositions();

            _updateCoroutine = StartCoroutine(UpdateElements());
        }
        
        private IEnumerator UpdateElements()
        {
            while (_isAlive)
            {
                _centerTransform.localRotation = Quaternion.AngleAxis(_currentAngleAllRotation, Vector3.up);
                _currentAngleAllRotation += _speedRotation * Time.deltaTime * _directionAllRotation;

                yield return null;
            }
        }

        private void CreateElements()
        {
            for (int i = 0; i < _countElements; i++)
            {
                var element = Instantiate(_elementPrefab, _centerTransform);
                _elements.Add(element);
            }
        }

        private void SetPositions()
        {
            const float maxCircleAngle = 360;
            
            var position = new Vector3
            {
                x = _radius
            };

            float angle = maxCircleAngle / _countElements;

            float currentAngle = 0;

            foreach (var element in _elements)
            {
                element.transform.localPosition = (Quaternion.AngleAxis(currentAngle, Vector3.up) * position) + _centerTransform.localPosition;

                currentAngle += angle;
            }
        }

        private void DeleteElements()
        {
            foreach (var element in _elements)
                Destroy(element);
            
            _elements.Clear();
        }
    }
}
