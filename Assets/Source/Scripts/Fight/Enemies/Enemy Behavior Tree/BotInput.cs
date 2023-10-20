using UnityEngine;

namespace ArenaHero.Fight.Enemies.BehaviorTree
{
    public class BotInput : MonoBehaviour, IBotInputhandler
    {
        public Vector2 MovementInput { get; set; }
    }
}