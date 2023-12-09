using ArenaHero.InputSystem;
using System;

namespace ArenaHero.Battle.PlayableCharacter.EnemyDetection
{
    public readonly struct TargetChangerInject
    {
        public readonly DetectedZone DetectedZone;
        public readonly LookTargetPoint LookTargetPoint;
        public readonly IActionsInputHandlerOnlyPlayer ActionsInputHandler;

        public TargetChangerInject(Func<(DetectedZone, LookTargetPoint, IActionsInputHandlerOnlyPlayer)> inject) =>
            (DetectedZone, LookTargetPoint, ActionsInputHandler) = inject();
    }
}
