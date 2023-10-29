using ArenaHero.Battle.Skills;
using UnityEngine;

namespace ArenaHero.Battle
{
    [RequireComponent(typeof(SkillsHandler))]
    public class Character : MonoBehaviour, IAttackable, IDamagable
    {
        [SerializeField] private SkillsHandler _skillsHandler;

        private void OnValidate()
        {
            if (_skillsHandler == null)
                _skillsHandler = GetComponent<SkillsHandler>();
        }

        public void Attack() => throw new System.NotImplementedException();
        public void TakeDamage(float damage) => throw new System.NotImplementedException();
    }
}
