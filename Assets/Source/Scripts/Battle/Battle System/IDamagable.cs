using System;

namespace ArenaHero.Battle
{
    public interface IDamagable
    {
        public event Action<float> HealthChanged;
        
        public void TakeDamage(float damage);
    }
}
