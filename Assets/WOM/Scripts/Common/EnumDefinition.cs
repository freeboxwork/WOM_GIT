
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
        union,
        none
    }
    public enum MonsterType
    {
        normal,
        gold,
        boss,
        evolution,
        dungeon,
        dungeonGold,
        dungeonDice,
        dungeonBone,
        dungeonCoal,
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
        rewardDiceEvolutionData,
        skillData,
        unionData,
        dnaData,
        TrainingElementData,
        convertTextData,
        monsterDataDungeonGold,
        monsterDataDungeonDice,
        monsterDataDungeonBone,
        monsterDataDungeonCoal,
        buildingDataMine,
        buildingDataFactory,
        buindingDataCamp,
        buindingDataLab
    }

    /// <summary> 훈련 메뉴 </summary>
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
        bone,
        jewel,
        dice,
        coal
    }


    public enum MenuPanelType
    {
        training,
        evolution,
        skill,
        castle,
        dungeon,
        shop,
        union,
        dna
    }

    public enum EvolutionDiceStatType
    {
        insectDamage,
        insectCriticalChance,
        insectCriticalDamage,
        goldBonus,
        insectMoveSpeed,
        insectSpawnTime,
        insectBossDamage
    }

    public enum EvolutionRewardGrade
    {
        NONE = 0,
        S = 1,
        A = 2,
        B = 3,
        C = 4,
        D = 5,
    }

    public enum SkillType
    {
        /// <summary> 곤충 공격력 증가 </summary>
        insectDamageUp,
        /// <summary> 유니온 공격력 증가 </summary>
        unionDamageUp,
        /// <summary> 아군 전체 속도 증가 </summary>
        allUnitSpeedUp,
        /// <summary> 골드 획득량 증가 </summary>
        glodBonusUp,
        /// <summary> 괴수 몬스터 등장 </summary>
        monsterKing,
        /// <summary> 아군 전체 치명타  </summary>
        allUnitCriticalChanceUp,
    }

    public enum UnionEquipType
    {
        Equipped,
        NotEquipped

    }

    public enum DNAType
    {
        insectDamage,
        insectCriticalChance,
        insectCriticalDamage,
        unionDamage,
        glodBonus,
        insectMoveSpeed,
        unionMoveSpeed,
        unionSpawnTime,
        goldPig,
        skillDuration,
        skillCoolTime,
        bossDamage,
        monsterHpLess,
        boneBonus,
        goldMonsterBonus,
        offlineBonus
    }

    public enum ShopSlotType
    {
        UnionSummon,
        DNASummon,
        FreeGem,
        FreeUnionSummon,
        FreeDNASummon
    }

    public enum LotteryPageType
    {
        UNION,
        DNA
    }


    public enum BGM_TYPE
    {
        BGM_01,

    }
    public enum SFX_TYPE
    {
        MONSTER_HIT,
    }


}
