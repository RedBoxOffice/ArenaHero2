﻿using System;

namespace ArenaHero.Battle
{
    public interface IDamageable
    {
        public event Action<float> HealthChanged;
        
        public void TakeDamage(float damage);
    }
}