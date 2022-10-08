using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public MonsterNormal monsterNormal;
    public MonsterGold monsterGold;
    public MonsterBoss monsterBoss;

    // normal gold boss
    public List<MonsterBase> monsters = new List<MonsterBase>();

    void Start()
    {
        

    }


    public IEnumerator Init(int stageId)
    {
        StageData stageData = GlobalData.instance.dataManager.GetStageDataById(stageId);

        yield return null;

        var monNormalData = GetMonsterData(EnumDefinition.MonsterType.normal, stageData.monsterNormalId);
        var monGoldData = GetMonsterData(EnumDefinition.MonsterType.gold, stageData.monsterGoldId);
        var monBossData = GetMonsterData(EnumDefinition.MonsterType.boss, stageData.monsterBossId);

        SetMonsterData(EnumDefinition.MonsterType.normal, monNormalData);
        SetMonsterData(EnumDefinition.MonsterType.gold, monGoldData);
        SetMonsterData(EnumDefinition.MonsterType.boss, monBossData);

        // set sprite image
        /*
        SetMonsterBodyImage(monNormalData, monsterNormal);
        SetMonsterBodyImage(monGoldData, monsterGold);
        SetMonsterBodyImage(monBossData, monsterBoss);
        */

        // add monsters list 
        monsters.Add(monsterNormal);
        monsters.Add(monsterGold);
        monsters.Add(monsterBoss);
    }

    void SetMonsterBodyImage(MonsterData monData, MonsterBase monster)
    {
        var spriteData = GlobalData.instance.dataManager.GetMonsterSpriteDataById(monData.imageId);

        // set tail
        monster.spriteLibraryChanged.ChangedSpritePartImgae("tail", spriteData.tail);
        // set hand
        monster.spriteLibraryChanged.ChangedSpritePartImgae("hand", spriteData.hand);
        // set finger
        monster.spriteLibraryChanged.ChangedSpritePartImgae("finger", spriteData.finger);
        // set foreArm
        monster.spriteLibraryChanged.ChangedSpritePartImgae("foreArm", spriteData.foreArm);
        // set upperArm
        monster.spriteLibraryChanged.ChangedSpritePartImgae("upperArm", spriteData.upperArm);
        // set head
        monster.spriteLibraryChanged.ChangedSpritePartImgae("head", spriteData.head);
        // set body
        monster.spriteLibraryChanged.ChangedSpritePartImgae("body", spriteData.body);
        // set leg_0
        monster.spriteLibraryChanged.ChangedSpritePartImgae("leg_0", spriteData.leg_0);
        // set leg_1
        monster.spriteLibraryChanged.ChangedSpritePartImgae("leg_1", spriteData.leg_1);
        // set leg_2
        monster.spriteLibraryChanged.ChangedSpritePartImgae("leg_2", spriteData.leg_2);
    }

    //TODO: 애니메이션으로 제어
    public void EnableMonster(EnumDefinition.MonsterType monsterType)
    {
        for (int i = 0; i < monsters.Count; i++)
        {
            if ((int)monsterType == i)
            {
                //TODO ANIMATION 으로 변경
                monsters[i].gameObject.SetActive(true);
            }
            else
            {
                monsters[i].gameObject.SetActive(false);
            }
        }
    }

    MonsterData GetMonsterData(EnumDefinition.MonsterType monsterType, int monsterId)
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

    void SetMonsterData(EnumDefinition.MonsterType monsterType , MonsterData monsterData)
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



    MonsterBase GetMonsterData(EnumDefinition.MonsterType monsterType)
    {
        switch (monsterType)
        {
            case EnumDefinition.MonsterType.normal: return monsterNormal;
            case EnumDefinition.MonsterType.gold: return monsterGold;
            case EnumDefinition.MonsterType.boss: return monsterBoss;   
        }
        return null;
    }
   
}
