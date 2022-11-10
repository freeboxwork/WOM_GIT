using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumDefinition;


public class Player : MonoBehaviour
{
    public int stageIdx;
    public int upgradeLevelIdx;
    public int gold;
    public int bone;
    public int gem;
    public DateTime playTime;
    
    /// <summary> 현재 진행중인 스테이지 데이터 </summary>
    public StageData currentStageData;
    public int pahseCountOriginalValue;

    /// <summary> 현재 전투중인 몬스터 </summary>
    public MonsterBase currentMonster;

    /// <summary> 현재 플레이어의 스탯 데이터 </summary>
    public PlayerStatData curStatData;



    void Start()
    {
        
    }

    private void Awake()
    {
        curStatData = new PlayerStatData();
    }

    public  IEnumerator Init(SaveData saveData)
    {
        SetPlayerDataFromSaveData(saveData);

        // Set stat data 
        SetStatData();

        yield return null;
    }

    void SetStatData()
    {
        foreach (SaleStatType stat in Enum.GetValues(typeof(SaleStatType)))
        {
            var level = GetStatLevelByStatType(stat);
            var data = GlobalData.instance.dataManager.GetSaleStatDataByTypeId(stat, level);
            SetStatValue(stat, data.value);
        }
    }

    // TODO: 저장된 플레이어 데이터에서 레벨값읽어 오도록 수정 현재 초기 값은 0
    int GetStatLevelByStatType(SaleStatType statType)
    {
        return 0;
    }

    public void SetCurrentMonster(MonsterBase monsterBase)
    {
        currentMonster = monsterBase;
    }

    public void SetPlayerDataFromSaveData(SaveData saveData)
    {
        stageIdx = saveData.stageIdx;
        upgradeLevelIdx = saveData.upgradeLevelIdx;
        gold = saveData.gold;
        SetCurrentStageData(stageIdx);

    }

    public void SetCurrentStageData(int stageIdx)
    {
        var stageData = GlobalData.instance.dataManager.GetStageDataById(stageIdx); ;
        currentStageData = stageData;
        pahseCountOriginalValue = stageData.phaseCount;
    }

    public void AddGold(int value)
    {
        gold = gold + value;
    }

    public void AddBone(int value)
    {
        bone = bone + value;
    }

    public void PayGold(int value)
    {
        gold = gold - value;
    }

    public void PayBone(int value)
    {
        bone = bone - value;
    }

    public int GetStatLevel(SaleStatType statType)
    {
        return curStatData.statLevelDatas[(int)statType];
    }

    public void SetStatLevel(SaleStatType statType, int level)
    {
        curStatData.statLevelDatas[(int)statType] = level;
    }
    
    public float GetStatValue(SaleStatType statType)
    {
        return curStatData.statValueDatas[(int)statType];
    }

    public void SetStatValue(SaleStatType statType, float value)
    {
        curStatData.statValueDatas[(int)statType] = value;
    }
}


