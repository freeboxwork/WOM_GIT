
public class EnumDefinition 
{
    public enum CustomDataType
    {
        button,
        text,
        image,
        animCont
    }

    public enum InsectType
    {
        mentis,
        bee,
        beetle,
        none
    }
    public enum MonsterType
    {
        normal,
        gold,
        boss
    }

    public enum AttackType
    {
        normal,
    }

    public enum SheetDataType
    {
        evolutionData_bee,
        evolutionData_beetle,
        evolutionData_mentis,
        evolutionOptionData_bee,
        evolutionOptionData_beetle,
        evolutionOptionData_mentis,
        monsterData_boss,
        monsterData_gold,
        monsterData_normal,
        stageData,
        upgradeData,
        monsterSpriteData
    }

    public enum AnimCurveType
    {
        EaseInQuad,
        EaseOutQuad,
        EaseInOutQuad,
        EaseOutCubic,
        EaseInOutCubic,
        Spring,
        EaseInQuint,
        EaseInOutSine,
    }
}
