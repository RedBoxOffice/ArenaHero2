using UnityEngine;

namespace ArenaHero.Fight.Enemies.BehaviorTree
{
    public interface IBotInputhandler
    {
        public Vector3 TargetPosition { get; set; }
    }
}
