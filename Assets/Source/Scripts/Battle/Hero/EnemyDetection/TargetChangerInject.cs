using ArenaHero.InputSystem;
using System;

namespace ArenaHero.Fight.Player.EnemyDetection
{
    public readonly struct TargetChangerInject
    {
        public readonly TriggerZone TriggerZone;
        public readonly LookTargetPoint LookTargetPoint;
        public readonly IActionsInputHandler ActionsInputHandler;

        public TargetChangerInject(Func<(TriggerZone, LookTargetPoint, IActionsInputHandler)> inject) =>
            (TriggerZone, LookTargetPoint, ActionsInputHandler) = inject();
    }
}
