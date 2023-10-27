using UnityEngine;

namespace ArenaHero.Battle.Enemies.BehaviorTree
{
    public class BotInput : MonoBehaviour, IBotInputhandler
    {
        public Vector3 TargetPosition { get; set; }
    }
}