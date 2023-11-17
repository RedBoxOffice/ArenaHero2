using ArenaHero.Battle.PlayableCharacter.Movement;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter
{
    public class Hero : MonoBehaviour, ITargetHandler
    {
        private LookTargetPoint _lookTargetPoint;
        
        public Target Target => _lookTargetPoint.Target;
        
        public Hero Init(LookTargetPoint lookTargetPoint)
        {
            _lookTargetPoint = lookTargetPoint;
            
            return this;
        }
    }
}
