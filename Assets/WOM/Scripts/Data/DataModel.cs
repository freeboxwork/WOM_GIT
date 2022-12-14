using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro.EditorUtilities;
using JetBrains.Annotations;

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
    public int monsterGoldId;
    public int phaseCount;
    public int bgId;
    public string rewardEvent;
    public string unlock;

    public StageData CopyInstance()
    {
        return (StageData)this.MemberwiseClone();
    }
}

[Serializable]
public class MonsterSprite
{
    public int id ;
    public int tail ;
    public int hand ;
    public int finger ;
    public int foreArm ;
    public int upperArm ;
    public int head ;
    public int body ;
    public int leg_0 ;
    public int leg_1 ;
    public int leg_2 ;
}


[Serializable]
public class EvolutionData
{
    public int depthId;
    public string name;
    public EnumDefinition.InsectType insectType;
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
public class NewData
{
    public int monsterId;
}

[Serializable]
public class MonsterData
{
    public int monsterId;
    public float hp;
    public float exp;
    public int gold;
    public int goldCount;
    public int imageId;
    public int bgId;
    public EnumDefinition.MonsterType monsterType;
    public EnumDefinition.AttackType attackType;
}

[Serializable]
public class InsectData
{
    public string name;
    public EnumDefinition.InsectType insectType;
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
    public float damage;
    public float passiveDamage;
    public float spawnTime;
    public float moveSpeed;
    public string name;
    public int reqirementLevelUp;
    public int increaseReqirement;
    public float increaseAbilityRate;
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
    public float gradeProbability;
}

[Serializable]
public class TalentData
{
    public int talentIndex;
    public float increaseValue;
    public int maxLevel;
    public string abilityType;
    public string explanation;
}

[Serializable]
public class DNAData
{
    public int dnaIndex;
    public float increaseValue;
    public float decreaseProbability;
    public int maxLevel;
    public string abilityType;
    public string explanation;
}

[Serializable]
public class SkillData
{
    public int skillIndex;
    public float duration;
    public int power;
    public string name;
    public float coolTime;
    public float addDurationTime;
    public float addPowerRate;
    public int maxLevel;
}

[Serializable]
public class UnionGambleData
{
    public int summonGrade;
    public float normal;
    public float high;
    public float rare;
    public float hero;
    public float legend;
    public float unique;
}

[Serializable]
public class SummonGradeData
{
    public int level;
    public int count;
    public string reward;
}

[Serializable]
public class TrainingDamageData
{
    public int level;
    public int salePrice;
    public float damage; 
}

[Serializable]
public class TrainingCriticalDamageData
{
    public int level;
    public int salePrice;
    public float criticalDamage;
}

[Serializable]
public class TrainingCriticalChanceData
{
    public int level;
    public int salePrice;
    public float criticalChance;
}



