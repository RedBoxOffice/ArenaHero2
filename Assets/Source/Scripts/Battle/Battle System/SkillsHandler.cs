using ArenaHero.Battle.Skills;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaHero.Battle
{
    [Serializable]
    public class SkillsHandler
    {
        [SerializeField] private List<Skill> _skills;

        public IReadOnlyCollection<Skill> SelfSkills => _skills;
    }
}
