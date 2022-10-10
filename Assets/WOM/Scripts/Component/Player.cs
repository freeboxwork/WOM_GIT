using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int stageIdx;
    public int upgradeLevelIdx;
    public int gold;
    public DateTime playTime;
    public StageData currentStageData;

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
        currentStageData = GlobalData.instance.dataManager.GetStageDataById(stageIdx);
    }

}
