using ArenaHero.Battle.Skills;
using UnityEngine;

namespace ArenaHero.Battle
{
    public class Character : MonoBehaviour, IAttackable, IDamagable
    {
        public void Attack() => throw new System.NotImplementedException();
        public void TakeDamage(float damage) => throw new System.NotImplementedException();
    }
}
