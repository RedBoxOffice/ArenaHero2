using UnityEngine;

namespace ArenaHero.Battle.Enemies.BehaviorTree
{
    public interface IBotInputhandler
    {
        public Vector3 TargetPosition { get; set; }
    }
}
