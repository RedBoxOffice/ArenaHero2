using UnityEngine;

namespace ArenaHero.Battle.PlayableCharacter
{
    public class LookTargetPoint : MonoBehaviour
    {
        public Target Target { get; private set; }

        public void UpdateTarget(Target target) =>
            Target = target;
    }
}
