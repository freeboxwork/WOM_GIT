using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro.EditorUtilities;

public class DataModel : MonoBehaviour
{
   
}


[Serializable]
public class SaveData
{
    public bool isFirstConnect;
    public int stageIdx;
    public InsectSaveData beeSaveData;
    public InsectSaveData beetleSaveData;
    public InsectSaveData mentisSaveData;
    public int upgradeLevelIdx;
    public int gold;
    public string offlineTime;
    public string playingTime;
}

[Serializable]
public class InsectSaveData
{
    public int evolutionIdx;
    public int upgradeLevel;
    public string evolutionTechTree;
    public EvolutionData evolutionLastData;
}


[Serializable]
public class StageData
{
    public int stageId;
    public string stageName;
    public int monsterNormalId;
    public int monsterBossId;
    public int mosterGlodId;
    public int phaseCount;
    public int bgId;
    public string rewardEvent;
    public string unlock;
}


[Serializable]
public class EvolutionData
{
    public int depthId;
    public string name;
    public EnumDefinetion.InsectType insectType;
    public float damage;
    public float damageRate;
    public float criticalChance;
    public float criticalDamage;
    public float speed;
    public float goldBonus;
    public float bossDamage;
}

[Serializable]
public class EvolutionOptionData
{
    public int optionId;
    public int optionA;
    public int optionB;
    public int requiredLevel;
}

[Serializable]
public class UpgradeData
{
    public int level;
    public float upgradeCost;
    public float damage;
}

[Serializable]
public class MonsterData
{
    public int monsterId;
    public float hp;
    public float exp;
    public float gold;
    public int goldCount;
    public int imageId;
    public EnumDefinetion.MonsterType monsterType;
    public int bgId;
    public EnumDefinetion.AttackType attackType;
}

[Serializable]
public class InsectData
{
    public string name;
    public EnumDefinetion.InsectType insectType;
    public float damage;
    public float damageRate;
    public float criticalChance;
    public float ciriticalDamage;
    public float speed;
    public float goldDamage;
    public float bossDamage;
}