using ProjectGraphics;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using static EnumDefinition;

public class DungeonMonster : DungeonMonsterBase
{
    [HideInInspector]
    public DungenMonsterFileData curMonsterData;

    public int curLevel = 1;
    public float curMonsterHP;
    public DungeonMonsterData curData;
    public SpriteLibraryChanged spriteLibraryChanged;
    public MonsterInOutAnimator inOutAnimator;
    
 
    public  DungenMonsterFileData[] dungenMonsterFileDatas;

    public Dictionary<MonsterType, DungenMonsterFileData> monsterToDataMap;


    void Start()
    {
        SetDataMap();
    }

    void SetDataMap()
    {
        monsterToDataMap = new Dictionary<MonsterType, DungenMonsterFileData>()
        {
            { MonsterType.dungeonGold, dungenMonsterFileDatas[0] },
            { MonsterType.dungeonDice, dungenMonsterFileDatas[1] },
            { MonsterType.dungeonBone, dungenMonsterFileDatas[2] },
            { MonsterType.dungeonCoal, dungenMonsterFileDatas[3] }
        };
    }

    public IEnumerator Init(MonsterType monsterType)
    {
        // SET TYPE
        SetMonsterType(monsterType);

        // SET DATA
        curData = GlobalData.instance.dataManager.GetDungeonMonsterDataByTypeLevel(monsterType, curLevel).CloneInstance();

        curMonsterHP = curData.monsterHP;

        yield return new WaitForEndOfFrame();

        // SET FACE
        SetMonsterFace(monsterToDataMap[monsterType].monsterFaceId);
        
        // ���� hp text
        GlobalData.instance.uiController.SetTxtMonsterHp(curMonsterHP);
        
        // ���� hp slider
        GlobalData.instance.uiController.SetSliderDungeonMonsterHP(curMonsterHP);

        yield return new WaitForEndOfFrame();
    }

    // ���� ���� ������ Ÿ�԰� ��ȭ Ÿ�� ����
    public void SetMonsterType(MonsterType monsterType)
    {
        curMonsterData = monsterToDataMap[monsterType];
        //this.monsterType = monsterType;
        //goodsType = monsterToDataMap[monsterType].goodsType;
    }

    void SetMonsterFace(int faceId)
    {
        Debug.Log("face id " + faceId);
        spriteLibraryChanged.ChangedSpriteAllImage(faceId);
    }

    public void SetNextLevelData()
    {
        curLevel++;
        curData = GlobalData.instance.dataManager.GetDungeonMonsterDataByTypeLevel(curMonsterData.monsterType, curLevel).CloneInstance();
        curMonsterHP = curData.monsterHP; 
    }

    public void DungeonMonsterOut()
    {
        curLevel = 0;
    }


}
