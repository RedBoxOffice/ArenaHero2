using System;
using UnityEngine;

namespace ArenaHero.Utils.Other
{
    [Serializable]
    public struct FloatRange
    {
        [SerializeField] private float _min;
        [SerializeField] private float _max;

        public float Min { get => _min; set { if (value < _max) _min = value; } }
        public float Max { get => _max; set { if (value > _min) _max = value; } }

        public FloatRange(float min, float max)
        {
            _min = min;
            _max = max;
        }
    }
}
