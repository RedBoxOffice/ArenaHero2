using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaHero.Battle.Skills
{
    [Serializable]
    public class BallSkill : Skill
    {
        [SerializeField] private Transform _centerTransform;
        [SerializeField] private GameObject _elementPrefab;
        [SerializeField, Range(1, 20)] private int _countElements;
        [SerializeField] private float _radius;
        [SerializeField] private float _speedRotation;
        [SerializeField, Range(-1, 1)] private int _directionAllRotation;

        private List<GameObject> _elements = new();
        private float _currentAngleAllRotation = 0.01f;
        private Coroutine _updateCoroutine;
        private MonoBehaviour _context;

        public override void Run()
        {
            if (_updateCoroutine != null)
                _context.StopCoroutine(_updateCoroutine);

            CreateElements();

            SetPositions();

            _updateCoroutine = _context.StartCoroutine(Update());
        }

        public override void OnValidate(MonoBehaviour context)
        {
            if (_directionAllRotation == 0)
                _directionAllRotation = 1;

            if (_context == null)
                _context = context;
        }

        private IEnumerator Update()
        {
            _centerTransform.rotation = Quaternion.AngleAxis(_currentAngleAllRotation, Vector3.up);
            _currentAngleAllRotation += _speedRotation * Time.deltaTime * _directionAllRotation;

            yield return null;
        }

        private void CreateElements()
        {
            for (int i = 0; i < _countElements; i++)
            {
                var element = UnityEngine.Object.Instantiate(_elementPrefab, _centerTransform);
                _elements.Add(element);
            }
        }

        private void SetPositions()
        {
            Vector3 position = new Vector3
            {
                x = _radius,
                z = _radius
            };

            float angle = 360 / _countElements;

            float currentAngle = 0;

            foreach (var element in _elements)
            {
                element.transform.localPosition = (Quaternion.AngleAxis(currentAngle, Vector3.up) * position) + _centerTransform.position;

                currentAngle += angle;
            }
        }
    }
}
