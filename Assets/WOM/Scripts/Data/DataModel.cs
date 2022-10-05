using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro.EditorUtilities;

public class DataModel : MonoBehaviour
{
   
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

    public EvolutionData CopyInstance()
    {
        return (EvolutionData)this.MemberwiseClone();
    }
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
    public int id;
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

[Serializable]
public class UnionData
{
    public int unionIndex;
    public string grade;
    public int damage;
    public int passiveDamage;
    public float spawnTime;
    public float moveSpeed;
    public string name;
    public int reqirementLevelUp;
    public int increaseReqirement;
    public int increaseAbilityRate;
}

[Serializable]
public class TrainingData
{
    public int trainingIndex;
    public float increaseValue;
    public int maxLevel;
    public string abilityType;
    public string explanation;
}

[Serializable]
public class EvolutionGradeData
{
    public string evolutionGrade;
    public int damageRate;
    public int slotCount;
    public int penaltyStoneAmount;
}

[Serializable]
public class PolishEvolutionData
{
    public string grade;
    public float insectDamage;
    public float insectCriticalChance;
    public float insectCriticalDamage;
    public float goldBonus;
    public float insectMoveSpeed;
    public float insectSpawnTime;
    public float InsectBossDamage;
    public int gradeProbability;
}

[Serializable]
public class TalentData
{
    public int talentIndex;
    public int increaseValue;
    public int maxLevel;
    public string abilityType;
    public string explanation;
}

[Serializable]
public class DNAData
{
    public int dnaIndex;
    public int increaseValue;
    public int decreaseProbability;
    public int maxLevel;
    public string abilityType;
    public string explanation;
}

[Serializable]
public class SkillData
{
    public int skillIndex;
    public int duration;
    public int power;
    public string name;
    public int coolTime;
    public int addDurationTime;
    public int addPowerRate;
    public int maxLevel;
}