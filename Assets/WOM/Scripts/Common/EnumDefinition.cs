
public class EnumDefinition 
{
    public enum CustomDataType
    {
        button,
        text,
        image,
        animCont,
        tr, 
        gm // gameobject
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
        boss,
        evolution
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
        monsterData_evolution,
        stageData,
        upgradeData,
        monsterSpriteData,
        unionGambleData,
        summonGradeData,
        rewardEvolutionGradeData,
        rewardDiceEvolutionData
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
        SCREEN_UI_POINT_GOLD,
        SCREEN_UI_POINT_BONE
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


    /// <summary> 재화타입 </summary>
    public enum GoodsType
    {
        gold,
        boone,
        jewel
    }


    public enum MenuPanelType
    {
        training,
        evolution,
        skill,
        union,
        dna,
        shop
    }


   
}
