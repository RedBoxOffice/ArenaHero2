using BehaviorDesigner.Runtime;
using System;

namespace ArenaHero.Fight.Enemies.BehaviorTree
{
    [Serializable]
    public class SharedBotInput : SharedVariable<BotInput>
    {
        public static implicit operator SharedBotInput(BotInput input) =>
            new SharedBotInput { Value = input };
    }
}
