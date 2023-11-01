using UnityEngine;

namespace ArenaHero.Battle.Skills
{
    public class SkillsHandler : MonoBehaviour
    {
        private Skill[] _skills;

        private void Awake()
        {
            _skills = GetComponentsInChildren<Skill>();

            foreach (Skill skill in _skills)
                skill.Run();
        }
    }
}
