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
    
    /// <summary> ���� �������� �������� ������ </summary>
    public StageData currentStageData;
    public int pahseCountOriginalValue;

    /// <summary> ���� �������� ���� </summary>
    public MonsterBase currentMonster;

    /// <summary> ���� �÷��̾��� ���� ���� </summary>
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


