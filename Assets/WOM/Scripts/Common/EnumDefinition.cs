
public class EnumDefinition 
{
    public enum CustomDataType
    {
        button,
        text,
        image,
        animCont,
        tr, // game object
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
        monsterSpriteData,
        unionGambleData,
        summonGradeData,
    }

    public enum SaleStatType
    {
        trainingDamage,
        trainingCriticalChance,
        trainingCriticalDamage,
        talentDamage,
        talentCriticalChance,
        talentCriticalDamage,
        talentMoveSpeed,
        talentSpawnSpeed,
        talentGoldBonus
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
        EaseOutQuint,
        Linear
    }

    public enum GoldPosType
    {
        START_POINT_Y,
        END_POOINT_Y_MIN,
        END_POOINT_Y_MAX,
        END_POOINT_X_MIN,
        END_POOINT_X_MAX,
        SCREEN_UI_POINT
    }

    public enum UnionGradeType
    {
        normal,
        high,
        rare,
        hero,
        legend,
        unique
    }
         

}
