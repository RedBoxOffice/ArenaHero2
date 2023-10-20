using UnityEngine;

namespace ArenaHero.Fight.Enemies.BehaviorTree
{
    public interface IBotInputhandler
    {
        public Vector2 MovementInput { get; set; }
    }
}
