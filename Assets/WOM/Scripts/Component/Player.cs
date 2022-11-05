using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int stageIdx;
    public int upgradeLevelIdx;
    public int gold;
    
    // UPGRADE DATA
    public int traningDamageLevel;
    public int traningCriticalDamageLevel;
    public int traningCriticalChanceLevel;

    public int gem;
    public DateTime playTime;
    
    /// <summary> ���� �������� �������� ������ </summary>
    public StageData currentStageData;
    public int pahseCountOriginalValue;

    /// <summary> ���� �������� ���� </summary>
    public MonsterBase currentMonster;

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

}
