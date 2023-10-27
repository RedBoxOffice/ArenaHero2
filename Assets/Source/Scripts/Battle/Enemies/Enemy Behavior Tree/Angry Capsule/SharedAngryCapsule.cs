using ArenaHero.Battle.Enemies.EnemyTypes;
using BehaviorDesigner.Runtime;
using System;

namespace ArenaHero.Battle.Enemies.BehaviorTree.Angry_Capsule
{
    [Serializable]
    public class SharedAngryCapsule : SharedVariable<AngryCapsule>
    {
        public static implicit operator SharedAngryCapsule(AngryCapsule value) =>
            new SharedAngryCapsule { Value = value };
    }
}
