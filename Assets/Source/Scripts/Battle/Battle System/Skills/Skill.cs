namespace ArenaHero.Battle.Skills
{
    public enum SkillType
    {
        Damage,
        Defence
    }

    public abstract class Skill
    {
        public readonly SkillType SkillType = SkillType.Damage | SkillType.Defence;
    }
}
