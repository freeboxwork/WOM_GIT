using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public List<TextAsset> sheetDatas = new List<TextAsset>();

    public StageDatas stageDatas;
    public List<EvolutionDatas> evolutionDatas;
    public List<EvolutionOptionDatas> evolutionOptionDatas;
    public List<MonsterDatas> monsterDatas;
    public UpgradeDatas upgradeData;
    
    void Start()
    {
        
    }

    public IEnumerator SetDatas()
    {
        yield return null;

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

    }
   
    void SetStageData()
    {
        stageDatas = GetData<StageDatas>(EnumDefinetion.SheetDataType.stageData);
    }
    void SetEvolutionData()
    {
        // 0 : mentins , 1 : bee , 2 : beetle
        var mentis = GetData<EvolutionDatas>(EnumDefinetion.SheetDataType.evolutionData_mentis);
        var bee = GetData<EvolutionDatas>(EnumDefinetion.SheetDataType.evolutionData_bee);
        var beetle = GetData<EvolutionDatas>(EnumDefinetion.SheetDataType.evolutionData_beetle);

        evolutionDatas.Add(mentis);
        evolutionDatas.Add(bee);
        evolutionDatas.Add(beetle);
    }
    void SetEvolutionOptionData()
    {
        // 0 : mentins , 1 : bee , 2 : beetle
        var mentis = GetData<EvolutionOptionDatas>(EnumDefinetion.SheetDataType.evolutionOptionData_mentis);
        var bee = GetData<EvolutionOptionDatas>(EnumDefinetion.SheetDataType.evolutionOptionData_bee);
        var beetle = GetData<EvolutionOptionDatas>(EnumDefinetion.SheetDataType.evolutionOptionData_beetle);

        evolutionOptionDatas.Add(mentis);
        evolutionOptionDatas.Add(bee);
        evolutionOptionDatas.Add(beetle);
    }
    void SetMonsterData()
    {
        // 0 : boss , 1 : gold , 2 : normal
        var boss = GetData<MonsterDatas>(EnumDefinetion.SheetDataType.monsterData_boss);
        var gold = GetData<MonsterDatas>(EnumDefinetion.SheetDataType.monsterData_gold);
        var normal = GetData<MonsterDatas>(EnumDefinetion.SheetDataType.monsterData_normal);

        monsterDatas.Add(boss);
        monsterDatas.Add(gold);
        monsterDatas.Add(normal);
    }
    void SetUpgradeData()
    {
        upgradeData = GetData<UpgradeDatas>(EnumDefinetion.SheetDataType.upgradeData);
    }
    

    T GetData<T>(EnumDefinetion.SheetDataType sheetDataType)
    {
        var data = GetSheetData(sheetDataType);
        return JsonUtility.FromJson<T>(data.text);
    }
    
    
    public StageData GetStageDataById(int stageId)
    {
        return null;
    }

    public EvolutionData GetEvolutionDataById(EnumDefinetion.InsectType insectType, int id)
    {
        return null;
    }

    public EvolutionOptionData GetEvolutionOptionDataById(EnumDefinetion.InsectType insectType, int id)
    {
        return null;
    }

    public MonsterData GetMonsterDataById(EnumDefinetion.MonsterType monsterType, int id)
    {
        return null;
    }

    public UpgradeData GetUpgradeDataById(int id)
    {
        return null;
    }

    TextAsset GetSheetData(EnumDefinetion.SheetDataType sheetDataType)
    {
        var idx = (int)sheetDataType;
        return sheetDatas[idx];
    }

}


[System.Serializable]
public class StageDatas
{
    public List<StageData> data = new List<StageData>();
}

[System.Serializable]
public class EvolutionDatas // insect Datas
{
    public List<EvolutionData> data = new List<EvolutionData>();
}

[System.Serializable]
public class EvolutionOptionDatas
{
    public List<EvolutionOptionData> data = new List<EvolutionOptionData>();
}

[System.Serializable]
public class MonsterDatas
{
    public List<MonsterData> data = new List<MonsterData>();
}

[System.Serializable]
public class UpgradeDatas
{
    public List<UpgradeData> data = new List<UpgradeData>(); 
}
