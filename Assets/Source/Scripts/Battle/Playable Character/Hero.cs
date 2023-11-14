using ArenaHero.Battle.PlayableCharacter.Movement;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter
{
    public class Hero : MonoBehaviour
    {
        public Hero Init(LookTargetPoint lookTargetPoint)
        {
            var movers = GetComponents<HeroMover>();

            foreach (var mover in movers)
                mover.Init(lookTargetPoint);

            return this;
        }
    }
}
