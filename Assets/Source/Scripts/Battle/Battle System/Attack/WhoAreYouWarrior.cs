using ArenaHero.Battle.Skills;
using UnityEngine;

namespace ArenaHero.Battle
{
    public class WhoAreYouWarrior
    {
        private Character _context;

        public WhoAreYouWarrior(Character context) =>
            _context = context;

        //public float CalculateDamage()
        //{
        //    float damage = 0;

        //    foreach (var skill in _context.SkillsHandler.SelfSkills)
        //    {
        //        if (skill is IPassiveSkill)
        //        {
        //            damage += skill.GetDamage();
        //        }
        //    }
        //}
    }
}
