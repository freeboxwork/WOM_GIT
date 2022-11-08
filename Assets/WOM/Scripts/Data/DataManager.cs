using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static EnumDefinition;
using System;

public class DataManager : MonoBehaviour
{

    /*
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
        unionGambleData,
        summonGradeData,
        trainingDamageData,
        trainingCriticalChanceData,
        trainingCriticalDamageData,
        talentDamageData,
        talentCriticalChanceData,
        talentCriticalDamageData,
        talentMoveSpeedData,
        talentSpawnSpeed,
        talentGoldBonusData

     */
    public List<TextAsset> sheetDatas = new List<TextAsset>();

    public StageDatas stageDatas;
    public List<EvolutionDatas> evolutionDatas;
    public List<EvolutionOptionDatas> evolutionOptionDatas;
    public List<MonsterDatas> monsterDatas;
    public UnionGambleDatas unionGambleDatas;
    public SummonGradeDatas summonGradeDatas;
    public UpgradeDatas upgradeData;
    public MonsterSprites monsterSpriteData;

    void Start()
    {
        
    }

    public IEnumerator SetDatas()
    {
        // STAGE
        SetStageData();

        // EVOLUTION
        SetEvolutionData();

        // EVOLUTION OPTION
        SetEvolutionOptionData();

        // MONSTER
        SetMonsterData();

        // UPGRADE
        SetUpgradeData();

        // MONSTER SPRETE
        SetMonsterSpriteData();

        // SET UNION GAMBLE DATA ( 뽑기 데이터 , 뽑기 그레이드 데이터 )
        SetGamleData();

        yield return null;
    }
   
    void SetGamleData()
    {
        unionGambleDatas = GetData<UnionGambleDatas>(EnumDefinition.SheetDataType.unionGambleData);
        summonGradeDatas = GetData<SummonGradeDatas>(EnumDefinition.SheetDataType.summonGradeData);
    }

    void SetStageData()
    {
        stageDatas = GetData<StageDatas>(EnumDefinition.SheetDataType.stageData);
    }
    void SetEvolutionData()
    {
        // 0 : mentins , 1 : bee , 2 : beetle
        var mentis = GetData<EvolutionDatas>(EnumDefinition.SheetDataType.evolutionData_mentis);
        var bee = GetData<EvolutionDatas>(EnumDefinition.SheetDataType.evolutionData_bee);
        var beetle = GetData<EvolutionDatas>(EnumDefinition.SheetDataType.evolutionData_beetle);

        // set type
        mentis.data.ForEach(f => f.insectType = InsectType.mentis);
        bee.data.ForEach(f => f.insectType = InsectType.bee);
        beetle.data.ForEach(f => f.insectType = InsectType.beetle);

        evolutionDatas.Add(mentis);
        evolutionDatas.Add(bee);
        evolutionDatas.Add(beetle);
    }
    void SetEvolutionOptionData()
    {
        // 0 : mentins , 1 : bee , 2 : beetle
        var mentis = GetData<EvolutionOptionDatas>(EnumDefinition.SheetDataType.evolutionOptionData_mentis);
        var bee = GetData<EvolutionOptionDatas>(EnumDefinition.SheetDataType.evolutionOptionData_bee);
        var beetle = GetData<EvolutionOptionDatas>(EnumDefinition.SheetDataType.evolutionOptionData_beetle);

        evolutionOptionDatas.Add(mentis);
        evolutionOptionDatas.Add(bee);
        evolutionOptionDatas.Add(beetle);
    }
    void SetMonsterData() 
    {
        var boss = GetData<MonsterDatas>(EnumDefinition.SheetDataType.monsterData_boss);
        var gold = GetData<MonsterDatas>(EnumDefinition.SheetDataType.monsterData_gold);
        var normal = GetData<MonsterDatas>(EnumDefinition.SheetDataType.monsterData_normal);

        // set type
        boss.data.ForEach(f => f.monsterType = MonsterType.boss);
        gold.data.ForEach(f => f.monsterType = MonsterType.gold);
        normal.data.ForEach(f => f.monsterType = MonsterType.normal);

        // 0 : normal , 1: gold , 2 : boss
        monsterDatas.Add(normal);
        monsterDatas.Add(gold);
        monsterDatas.Add(boss);
    }
    void SetUpgradeData()
    {
        upgradeData = GetData<UpgradeDatas>(EnumDefinition.SheetDataType.upgradeData);
    }
    
    void SetMonsterSpriteData()
    {
        monsterSpriteData = GetData<MonsterSprites>(EnumDefinition.SheetDataType.monsterSpriteData);
    }

    T GetData<T>(EnumDefinition.SheetDataType sheetDataType)
    {
        var data = GetSheetData(sheetDataType);
        return JsonUtility.FromJson<T>(data.text);
    }
    
    public StageData GetStageDataById(int stageId)
    {
        return stageDatas.data.FirstOrDefault(f=> f.stageId == stageId);
    }

    public EvolutionData GetEvolutionDataById(EnumDefinition.InsectType insectType, int id)
    {
        return evolutionDatas[TypeIdx(insectType)].data.FirstOrDefault(f=> f.depthId == id);
    }

    public EvolutionOptionData GetEvolutionOptionDataById(EnumDefinition.InsectType insectType, int id)
    {
        return evolutionOptionDatas[TypeIdx(insectType)].data.FirstOrDefault(f => f.optionId == id);
    }

    public MonsterData GetMonsterDataById(EnumDefinition.MonsterType monsterType, int id)
    {

        return monsterDatas[TypeIdx(monsterType)].data.FirstOrDefault(f => f.monsterId == id);
    }

    public UpgradeData GetUpgradeDataById(int id)
    {
        return upgradeData.data.FirstOrDefault(f => f.id == id);
    }

    public MonsterSprite GetMonsterSpriteDataById(int id)
    {
        return monsterSpriteData.data.FirstOrDefault(f => f.id == id);
    }

    public UnionGambleData GetUnionGambleDataBySummonGrade(int summonGrade)
    {
        return unionGambleDatas.data.FirstOrDefault(f => f.summonGrade == summonGrade);
    }

    public SummonGradeData GetSummonGradeDataByLevel(int level)
    {
        return summonGradeDatas.data.FirstOrDefault(f => f.level == level); 
    }


    TextAsset GetSheetData(EnumDefinition.SheetDataType sheetDataType)
    {
        var idx = (int)sheetDataType;
        return sheetDatas[idx];
    }

    int TypeIdx(EnumDefinition.InsectType  insectType)
    {
        return (int)insectType;
    }
    int TypeIdx(EnumDefinition.MonsterType monsterType)
    {
        return (int)monsterType;
    }
}


[Serializable]
public class StageDatas
{
    public List<StageData> data = new List<StageData>();
}

[Serializable]
public class EvolutionDatas // insect Datas
{
    public List<EvolutionData> data = new List<EvolutionData>();
}

[Serializable]
public class EvolutionOptionDatas
{
    public List<EvolutionOptionData> data = new List<EvolutionOptionData>();
}

[Serializable]
public class MonsterDatas
{
    public List<MonsterData> data = new List<MonsterData>();
}

[Serializable]
public class UpgradeDatas
{
    public List<UpgradeData> data = new List<UpgradeData>(); 
}


[Serializable]
public class MonsterSprites
{
    public List<MonsterSprite> data = new List<MonsterSprite>();
}

[Serializable]
public class UnionGambleDatas
{
    public List<UnionGambleData> data = new List<UnionGambleData>();
}

[Serializable]
public class SummonGradeDatas
{
    public List<SummonGradeData> data = new List<SummonGradeData>();
}

[Serializable]
public class TrainingDamageDatas
{

}

