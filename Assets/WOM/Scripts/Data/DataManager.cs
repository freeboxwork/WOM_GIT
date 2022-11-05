using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using static EnumDefinition;

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
        trainingCriticalDamageData,
        trainingCriticalChanceData


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
    public TrainingDamageDatas trainingDamageDatas;
    public TrainingCriticalDamageDatas trainingCriticalDamageDatas;
    public TrainingCriticalChanceDatas trainingCriticalChanceDatas;


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
        SetGambleData();

        // TRANING DATAS
        SetTraningDatas();

        yield return null;
    }

    void SetTraningDatas()
    {
        trainingDamageDatas = GetData<TrainingDamageDatas>(SheetDataType.trainingDamageData);
        trainingCriticalDamageDatas = GetData<TrainingCriticalDamageDatas>(SheetDataType.trainingDamageData);
        trainingCriticalChanceDatas = GetData<TrainingCriticalChanceDatas>(SheetDataType.trainingCriticalDamageData);
    }
   
    void SetGambleData()
    {
        unionGambleDatas = GetData<UnionGambleDatas>(SheetDataType.unionGambleData);
        summonGradeDatas = GetData<SummonGradeDatas>(SheetDataType.summonGradeData);
    }

    void SetStageData()
    {
        stageDatas = GetData<StageDatas>(SheetDataType.stageData);
    }
    void SetEvolutionData()
    {
        // 0 : mentins , 1 : bee , 2 : beetle
        var mentis = GetData<EvolutionDatas>(SheetDataType.evolutionData_mentis);
        var bee = GetData<EvolutionDatas>(SheetDataType.evolutionData_bee);
        var beetle = GetData<EvolutionDatas>(SheetDataType.evolutionData_beetle);

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
        var mentis = GetData<EvolutionOptionDatas>(SheetDataType.evolutionOptionData_mentis);
        var bee = GetData<EvolutionOptionDatas>(SheetDataType.evolutionOptionData_bee);
        var beetle = GetData<EvolutionOptionDatas>(SheetDataType.evolutionOptionData_beetle);

        evolutionOptionDatas.Add(mentis);
        evolutionOptionDatas.Add(bee);
        evolutionOptionDatas.Add(beetle);
    }
    void SetMonsterData() 
    {
        var boss = GetData<MonsterDatas>(SheetDataType.monsterData_boss);
        var gold = GetData<MonsterDatas>(SheetDataType.monsterData_gold);
        var normal = GetData<MonsterDatas>(SheetDataType.monsterData_normal);

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
        upgradeData = GetData<UpgradeDatas>(SheetDataType.upgradeData);
    }
    
    void SetMonsterSpriteData()
    {
        monsterSpriteData = GetData<MonsterSprites>(SheetDataType.monsterSpriteData);
    }

    T GetData<T>(SheetDataType sheetDataType)
    {
        var data = GetSheetData(sheetDataType);
        return JsonUtility.FromJson<T>(data.text);
    }
    
    public StageData GetStageDataById(int stageId)
    {
        return stageDatas.data.FirstOrDefault(f=> f.stageId == stageId);
    }

    public EvolutionData GetEvolutionDataById(InsectType insectType, int id)
    {
        return evolutionDatas[TypeIdx(insectType)].data.FirstOrDefault(f=> f.depthId == id);
    }

    public EvolutionOptionData GetEvolutionOptionDataById(InsectType insectType, int id)
    {
        return evolutionOptionDatas[TypeIdx(insectType)].data.FirstOrDefault(f => f.optionId == id);
    }

    public MonsterData GetMonsterDataById(MonsterType monsterType, int id)
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

    public TrainingDamageData GetTrainingDamageDataByLevel(int level)
    {
        return trainingDamageDatas.data.FirstOrDefault(f => f.level == level);
    }

    public TrainingCriticalDamageData GetTrainingCriticalDamageDataByLevel(int level)
    {
        return trainingCriticalDamageDatas.data.FirstOrDefault(f => f.level == level);
    }

    public TrainingCriticalChanceData GetTrainingCriticalChanceDataByLevel(int level)
    {
        return trainingCriticalChanceDatas.data.FirstOrDefault(f => f.level == level);
    }

    TextAsset GetSheetData(SheetDataType sheetDataType)
    {
        var idx = (int)sheetDataType;
        return sheetDatas[idx];
    }

    int TypeIdx(InsectType  insectType)
    {
        return (int)insectType;
    }
    int TypeIdx(MonsterType monsterType)
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
    public List<TrainingDamageData> data = new List<TrainingDamageData>();
}

[Serializable]
public class TrainingCriticalDamageDatas
{
    public List<TrainingCriticalDamageData> data = new List<TrainingCriticalDamageData>();
}

[Serializable]
public class TrainingCriticalChanceDatas
{
    public List<TrainingCriticalChanceData> data = new List<TrainingCriticalChanceData>();
}

