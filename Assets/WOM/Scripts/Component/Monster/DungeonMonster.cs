using ProjectGraphics;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using static EnumDefinition;

public class DungeonMonster : DungeonMonsterBase
{
    public int curLevel = 1;
    public float curMonsterHP;
    public DungeonMonsterData curData;
    public SpriteLibraryChanged spriteLibraryChanged;
    public MonsterInOutAnimator inOutAnimator;

    Dictionary<MonsterType, GoodsType> monsterToGoodsMap = new Dictionary<MonsterType, GoodsType>()
    {
        { MonsterType.dungeonGold, GoodsType.gold },
        { MonsterType.dungeonDice, GoodsType.dice },
        { MonsterType.dungeonBone, GoodsType.bone },
        { MonsterType.dungeonCoal, GoodsType.coal },
    };

    Dictionary<MonsterType, int> monsterToFaceIdMap = new Dictionary<MonsterType, int>()
    {
        { MonsterType.dungeonGold, 99 },
        { MonsterType.dungeonDice, 90 },
        { MonsterType.dungeonBone, 80 },
        { MonsterType.dungeonCoal, 70 },
    };

    void Start()
    {
      
    }

    public IEnumerator Init(MonsterType monsterType)
    {
        // SET TYPE
        SetMonsterType(monsterType);

        // SET DATA
        curData = GlobalData.instance.dataManager.GetDungeonMonsterDataByTypeLevel(monsterType, curLevel).CloneInstance();

        curMonsterHP = curData.monsterHP;

        // SET FACE
        SetMonsterFace(monsterToFaceIdMap[monsterType]);
        
        // 몬스터 hp text
        GlobalData.instance.uiController.SetTxtMonsterHp(curMonsterHP);
        
        // 몬스터 hp slider
        GlobalData.instance.uiController.SetSliderDungeonMonsterHP(curMonsterHP);

        yield return new WaitForEndOfFrame();
    }

    // 현재 던전 몬스터의 타입과 재화 타입 설정
    public void SetMonsterType(MonsterType monsterType)
    {
        this.monsterType = monsterType;
        goodsType = monsterToGoodsMap[monsterType];
    }

    void SetMonsterFace(int faceId)
    {
        var spriteData = GlobalData.instance.dataManager.GetMonsterSpriteDataById(faceId);

        // set tail
        spriteLibraryChanged.ChangedSpritePartImage("tail", spriteData.tail);
        // set hand
        spriteLibraryChanged.ChangedSpritePartImage("hand", spriteData.hand);
        // set finger
        spriteLibraryChanged.ChangedSpritePartImage("finger", spriteData.finger);
        // set foreArm
        spriteLibraryChanged.ChangedSpritePartImage("foreArm", spriteData.foreArm);
        // set upperArm
        spriteLibraryChanged.ChangedSpritePartImage("upperArm", spriteData.upperArm);
        // set head
        spriteLibraryChanged.ChangedSpritePartImage("head", spriteData.head);
        // set body
        spriteLibraryChanged.ChangedSpritePartImage("body", spriteData.body);
        // set leg_0
        spriteLibraryChanged.ChangedSpritePartImage("leg_0", spriteData.leg_0);
        // set leg_1
        spriteLibraryChanged.ChangedSpritePartImage("leg_1", spriteData.leg_1);
        // set leg_2
        spriteLibraryChanged.ChangedSpritePartImage("leg_2", spriteData.leg_2);
    }

    public void SetNextLevelData()
    {
        curLevel++;
        curData = GlobalData.instance.dataManager.GetDungeonMonsterDataByTypeLevel(monsterType, curLevel).CloneInstance();
        curMonsterHP = curData.monsterHP; 
    }

    public void DungeonMonsterOut()
    {
        curLevel = 0;
    }


}
