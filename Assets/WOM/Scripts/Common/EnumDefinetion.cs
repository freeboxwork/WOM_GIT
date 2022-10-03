using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumDefinetion 
{
    public enum InsectType
    {
        mentis,
        bee,
        beetle
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
        upgradeData
    }
}
