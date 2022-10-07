using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public MonsterNormal monsterNormal;
    public MonsterGold monsterGold;
    public MonsterBoss monsterBold;
    
    // normal gold boss

    void Start()
    {
        

    }


    public IEnumerator Init(int stageId)
    {
        StageData stageData = GlobalData.instance.dataManager.GetStageDataById(stageId);

        yield return null;

        var monNormalData = GetMonsterData(EnumDefinetion.MonsterType.normal, stageData.monsterNormalId);
        var monGoldData = GetMonsterData(EnumDefinetion.MonsterType.gold, stageData.monsterGoldId);
        var monBossData = GetMonsterData(EnumDefinetion.MonsterType.boss, stageData.monsterBossId);

        SetMonsterData(EnumDefinetion.MonsterType.normal, monNormalData);
        SetMonsterData(EnumDefinetion.MonsterType.gold, monGoldData);
        SetMonsterData(EnumDefinetion.MonsterType.boss, monBossData);
    }

    MonsterData GetMonsterData(EnumDefinetion.MonsterType monsterType, int monsterId)
    {
        var data = GlobalData.instance.dataManager.GetMonsterDataById(monsterType,monsterId); 
        if(data == null)
        {
            //TODO : ERROR PRINT 관리자 만들기
            Debug.LogError("");
            return null;
        }
        else
        {
            return data;    
        }
    }

    void SetMonsterData(EnumDefinetion.MonsterType monsterType , MonsterData monsterData)
    {
        var monster = GetMonsterData(monsterType);
        
        monster.monsterId = monsterData.monsterId;
        monster.hp = monsterData.hp;
        monster.exp = monsterData.exp;
        monster.gold = monsterData.gold;
        monster.goldCount = monsterData.goldCount;
        monster.imageId = monsterData.imageId;
        monster.bgId = monsterData.bgId;
        monster.monsterType = monsterData.monsterType;
        monster.attackType = monsterData.attackType;

    }



    MonsterBase GetMonsterData(EnumDefinetion.MonsterType monsterType)
    {
        switch (monsterType)
        {
            case EnumDefinetion.MonsterType.normal: return monsterNormal;
            case EnumDefinetion.MonsterType.gold: return monsterGold;
            case EnumDefinetion.MonsterType.boss: return monsterBold;   
        }
        return null;
    }
   
}
