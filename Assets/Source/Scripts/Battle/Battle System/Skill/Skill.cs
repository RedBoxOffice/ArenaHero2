using System;
using UnityEngine;

namespace ArenaHero.Battle.Skills
{
    public abstract class Skill
    {
        public abstract void Run();
        public abstract void OnValidate(MonoBehaviour context);
    }
}
