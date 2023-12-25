using System.Collections;
using System.Collections.Generic;
using ArenaHero.Utils.Other;
using UnityEngine;

namespace ArenaHero.Battle.Skills
{
    public class BallSkill : Skill, IUpdatableSkill 
    {
        [SerializeField] private Transform _centerTransform;
        [SerializeField] private Ball _ballPrefab;
        [SerializeField, Range(1, 20)] private int _countBalls;
        [SerializeField] private float _radius;
        [SerializeField] private float _speedRotation;
        [SerializeField, Range(-1, 1)] private int _directionAllRotation;

        private readonly List<Ball> _balls = new List<Ball>();
        
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
            DeleteBalls();

        public void UpdateSkill()
        {
            if (_updateCoroutine != null)
                StopCoroutine(_updateCoroutine);

            DeleteBalls();
            
            CreateBalls();

            SetPositions();

            _updateCoroutine = StartCoroutine(UpdateBalls());
        }
        
        private IEnumerator UpdateBalls()
        {
            while (_isAlive)
            {
                _centerTransform.localRotation = Quaternion.AngleAxis(_currentAngleAllRotation, Vector3.up);
                _currentAngleAllRotation += _speedRotation * Time.deltaTime * _directionAllRotation;

                yield return null;
            }
        }

        private void CreateBalls()
        {
            for (int i = 0; i < _countBalls; i++)
            {
                var ball = Instantiate(_ballPrefab, _centerTransform);
                _balls.Add(ball);
            }
        }

        private void SetPositions()
        {
            const float maxCircleAngle = 360;
            
            var position = new Vector3
            {
                x = _radius
            };

            float angle = maxCircleAngle / _countBalls;

            float currentAngle = 0;

            foreach (var ball in _balls)
            {
                ball.transform.localPosition = (Quaternion.AngleAxis(currentAngle, Vector3.up) * position) + _centerTransform.localPosition;

                currentAngle += angle;
            }
        }

        private void DeleteBalls()
        {
            foreach (var element in _balls)
                Destroy(element);
            
            _balls.Clear();
        }
    }
}
