using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro.EditorUtilities;
using JetBrains.Annotations;
using UnityEngine.Experimental.GlobalIllumination;
using Unity.VisualScripting;
using System.Security.Cryptography.X509Certificates;

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
    public int monsterEvolId;
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
    public int id;
    public float hp;
    public float exp;
    public int bone;
    public int gold;
    public int boneCount;
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
    public string gradeType;
    public int grade;
    public string name;
    public string gradeName;
    public float damage;
    public float passiveDamage;
    public float spawnTime;
    public float moveSpeed;
    public string spriteName;
    public int reqirementCount;
    public int addReqirementCount;
    public float addPassiveDamage;
    public float addDamage;
    public int maxLevel;
}

[Serializable]
public class UnionInGameData
{
    public int unionIndex;
    public int level;
    public int unionCount;
    public int LevelUpReqirementCount;
    public bool isUnlock;
    public EnumDefinition.UnionGradeType unionGradeType;

    // stat
    public float damage;
    public float spawnTime;
    public float moveSpeed;
    public float passiveDamage;

   

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
    public int dnaIndex ;
    public float power ;
    public int maxLevel ;
    public string spriteName ;
    public string infoFront ;
    public string infoBack ;
    public string dnaName ;
    public string dnaType ;
}

public class DNAInGameData
{
    public int level;
    public int maxLevel;
    public float power;
    public float dataPower;
    public string name;

    public void LevelUp()
    {
        ++level;
        power = (level * dataPower);
    }
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
/// <summary> 판매 가능한 스탯 데이터</summary>
public class StatSaleData
{
    public int level;
    public int salePrice;

    // gold 로 획득
    public float value;
    public string unitName;
}



[Serializable]
// 훈련 스탯 인게임 데이터 ( 리펙토링으로 추가됨, StatSaleData 를 대체 함 )
public class TraningInGameData
{
    public int level;
    public int salePrice;
    public float value; 
    public string unitName;
    public string trainingName;
}

//[Serializable]
/// <summary> 현재 유저의 스탯 레벨 인덱스 데이터</summary>
//public class PlayerStatData
//{
//    int levelDamage;
//    int levelCriticalChance;
//    int levelCriticalDamage;
//    int levelTalentDamage;
//    int levelTalentCriticalChance;
//    int levelTalentCriticalDamage;
//    int levelTalentMoveSpeed;
//    int levelTalentSpawnSpeed;
//    int levelTalentGoldBonus;

//    float valueDamage;
//    float valueCriticalChance;
//    float valueCriticalDamage;
//    float valueTalentDamage;
//    float valueTalentCriticalChance;
//    float valueTalentCriticalDamage;
//    float valueTalentMoveSpeed;
//    float valueTalentSpawnSpeed;
//    float valueTalentGoldBonus;


//    public List<int> statLevelDatas;
//    public List<float> statValueDatas;
    
    
//    public PlayerStatData()
//    {
//        statLevelDatas = new List<int>();
//        statLevelDatas.Add(levelDamage);
//        statLevelDatas.Add(levelCriticalChance);
//        statLevelDatas.Add(levelCriticalDamage);
//        statLevelDatas.Add(levelTalentDamage);
//        statLevelDatas.Add(levelTalentCriticalChance);
//        statLevelDatas.Add(levelTalentCriticalDamage);
//        statLevelDatas.Add(levelTalentMoveSpeed);
//        statLevelDatas.Add(levelTalentSpawnSpeed);
//        statLevelDatas.Add(levelTalentGoldBonus);

//        statValueDatas = new List<float>();
//        statValueDatas.Add(valueDamage);
//        statValueDatas.Add(valueCriticalChance);
//        statValueDatas.Add(valueCriticalDamage);
//        statValueDatas.Add(valueTalentDamage);
//        statValueDatas.Add(valueTalentCriticalChance);
//        statValueDatas.Add(valueTalentCriticalDamage);
//        statValueDatas.Add(valueTalentMoveSpeed);
//        statValueDatas.Add(valueTalentSpawnSpeed);
//        statValueDatas.Add(valueTalentGoldBonus);
//    }
//}


[Serializable]
public class SaleStatMsgData
{
    public EnumDefinition.SaleStatType saleStatType;

    public SaleStatMsgData(EnumDefinition.SaleStatType saleStatType)
    {
        this.saleStatType = saleStatType;
    }
}


[Serializable]
public class RewardEvolutionGradeData
{
    public int id;
    public string evolutionGradeType;
    public int damageRate;
    public int slotCount;
    public string nameKR;
    public string nameEN;
}

[Serializable]
public class GlobalMessageData
{
    public int id;
    public string message_kor;
    public string message_eng;
 }

[Serializable]
public class RewardDiceEvolutionData
{
    public int grade;
    public string gradeColor;
    public float insectDamage;
    public float insectCriticalChance;
    public float insectCriticalDamage;
    public float goldBonus;
    public float insectMoveSpeed;
    public float insectSpawnTime;
    public float insectBossDamage;
    public int unLockCount;
    public float gradeProbability;
}

/// <summary>  진화 주사위 획득 값 저장 </summary>
[Serializable]
public class DiceEvolutionInGameData
{ 
    public float insectDamage;
    public float insectCriticalChance;
    public float insectCriticalDamage;
    public float insectMoveSpeed;
    public float insectSpawnTime;
    public float insectBossDamage;
    public float goldBonus;
}

[Serializable]
public class SkillData
{
    public int id;
    public string skillType;
    public float duration;
    public float power;
    public string name;
    public float coolTime;
    public float addDurationTime;
    public float addPowerRate;
    public int maxLevel;
    public string desctiption;
    public float defaultCost;
    public float addCostAmount;
    public int unLockLevel;
}

[Serializable]
public class Skill_InGameData
{
    public EnumDefinition.SkillType skillType;
    public int level;
    public float duaration;
    public float power;
    public float damage;
    public float coolTime;
    public string skilName;

}

[Serializable]
public class TrainingElementData
{
    public int id;
    public string buttonSprite;
    public string currencySprite;
    public string trainingName;
    public string trainingType;
    public string goodsType;

}

[Serializable]
public class ConvertTextData
{
    public string en_EN;
    public string ko_KR;

    public string en_Front;
    public string en_Back;
    public string kr_Front;
    public string kr_Back;

    public string type;
}
