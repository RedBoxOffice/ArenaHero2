using UnityEngine;

namespace ArenaHero.Utils.UnityTool
{
    public sealed class VisibleToConditionAttribute : PropertyAttribute
    {
        public VisibleToConditionAttribute(string propertyName, bool condition = true)
        {
            PropertyName = propertyName;
            Condition = condition;
        }

        public string PropertyName;

        public bool Condition;
    }
}