using ArenaHero.InputSystem;
using System;

namespace ArenaHero.Battle.PlayableCharacter.EnemyDetection
{
    public readonly struct TargetChangerInject
    {
        public readonly DetectedZone TriggerZone;
        public readonly LookTargetPoint LookTargetPoint;
        public readonly IActionsInputHandler ActionsInputHandler;

        public TargetChangerInject(Func<(DetectedZone, LookTargetPoint, IActionsInputHandler)> inject) =>
            (TriggerZone, LookTargetPoint, ActionsInputHandler) = inject();
    }
}
