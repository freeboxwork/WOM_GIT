using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungenMonsterGold : DungeonMonsterBase
{
    public int curLevel = 0;
    public DungeonMonsterData curData;

    void Start()
    {
       
    }

    public void SetNextLevelData() 
    {
        curLevel++;
        curData = GlobalData.instance.dataManager.GetDungeonMonsterDataByTypeLevel(monsterType,curLevel).CloneInstance();
    }



    
}
