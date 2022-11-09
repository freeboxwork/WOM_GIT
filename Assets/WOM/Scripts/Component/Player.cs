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

    /// <summary> 현재 플레이어의 스탯 레벨 </summary>
    public PlayerStatLevelData curStatLevel;

    void Start()
    {
        
    }
    
  
    public  IEnumerator Init(SaveData saveData)
    {
        yield return null;
        SetPlayerDataFromSaveData(saveData);
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


    public int GetStatLevel(SaleStatType statType)
    {
        return curStatLevel.statDatas[(int)statType];
    }

    public void SetStatLevel(SaleStatType statType, int level)
    {
        curStatLevel.statDatas[(int)statType] = level;
    }
}


