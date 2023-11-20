using ArenaHero.InputSystem;
using System;

namespace ArenaHero.Battle.PlayableCharacter.EnemyDetection
{
    public readonly struct TargetChangerInject
    {
        public readonly DetectedZone TriggerZone;
        public readonly LookTargetPoint LookTargetPoint;
        public readonly IActionsInputHandlerOnlyPlayer ActionsInputHandler;

        public TargetChangerInject(Func<(DetectedZone, LookTargetPoint, IActionsInputHandlerOnlyPlayer)> inject) =>
            (TriggerZone, LookTargetPoint, ActionsInputHandler) = inject();
    }
}
