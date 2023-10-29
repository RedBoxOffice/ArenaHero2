using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaHero.Battle.Skills
{
    public class SkillsHandler : MonoBehaviour
    {
        [SerializeField] private Dictionary<MonoBehaviour, MonoBehaviour> _skills = new()
        {

        };

        public void OnValidate()
        {
            //foreach (var skill in _skills)
            //    skill.OnValidate(this);
        }
    }
}
