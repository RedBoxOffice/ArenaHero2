using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter
{
    public class LookTargetPoint : MonoBehaviour
    {
        [SerializeField] private Vector3 _offsetDefaultPosition;
        
        public Target Target { get; private set; }

        public void UpdateTarget(Target target) =>
            Target = target;

        public void SetDefaultPosition(Transform heroTransform)
        {
            transform.position = heroTransform.position + (heroTransform.forward + _offsetDefaultPosition);
        }
    }
}
