using ArenaHero.Battle.PlayableCharacter.Movement;
using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter
{
    public class Hero : MonoBehaviour, ITargetHandler
    {
        private LookTargetPoint _lookTargetPoint;

        public Transform Target => _lookTargetPoint.transform.parent.transform;
        
        public Hero Init(LookTargetPoint lookTargetPoint)
        {
            _lookTargetPoint = lookTargetPoint;
            
            return this;
        }
    }
}
