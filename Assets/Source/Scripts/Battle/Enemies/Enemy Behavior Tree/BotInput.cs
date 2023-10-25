using UnityEngine;

namespace ArenaHero.Fight.Enemies.BehaviorTree
{
    public class BotInput : MonoBehaviour, IBotInputhandler
    {
        public Vector3 TargetPosition { get; set; }
    }
}