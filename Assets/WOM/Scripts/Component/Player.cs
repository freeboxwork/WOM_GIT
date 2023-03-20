using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static EnumDefinition;


public class Player : MonoBehaviour
{
    // 스테이지 레벨
    public int stageIdx;
    public int upgradeLevelIdx;
    // 훈련 레벨
    public int traningLevelIdx;

    public int gold;
    public int bone;
    public int gem;
    public int coal;

    // Dungeon Key
    public SerializableDictionary<GoodsType, int> dungeonKeys;

    public DateTime playTime;
    public float currentMonsterHp;

    // 주사위 개수
    public int diceCount;


    /// <summary> 현재 진행중인 스테이지 데이터 </summary>
    public StageData currentStageData;
    public int pahseCountOriginalValue;

    /// <summary> 현재 전투중인 몬스터 </summary>
    public MonsterBase currentMonster;

    /// <summary> 현재 전투중인 몬스터 타입 </summary>
    public MonsterType curMonsterType;

    /// <summary> 직전에 전투한 몬스터 타입 </summary>
    public MonsterType prevMonsterType;


    /// <summary> 현재 플레이어의 스탯 데이터 </summary>


    // 기본 몬스터를 제외한 몬스터 사냥중일때
    public bool isSpacialMonsterHunting;

    // 보스 몬스터 도전 가능 상태 판단
    public bool isBossMonsterChllengeEnable = false;

    

    void Start()
    {
        
    }

    private void Awake()
    {
        
    }

    public  IEnumerator Init(SaveData saveData)
    {
        SetPlayerDataFromSaveData(saveData);
        yield return null;
    }


    public void SetCurrentMonster(MonsterBase monsterBase)
    {
        currentMonster = monsterBase;
    }


    public MonsterBase GetCurrntMonster()
    {
        return currentMonster;
    }

    public void SetCurrentMonsterType(MonsterType monsterType)
    {
        curMonsterType = monsterType;
    }

    public void SetPervMonsterType(MonsterType monsterType)
    {
        prevMonsterType = monsterType;
    }


    public void SetCurrentMonsterHP(float hpValue)
    {
        currentMonsterHp = hpValue ;

        //* (1- GlobalData.instance.statManager.MonsterHpLess())
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
        gold += value;
        GlobalData.instance.traningManager.EnableBuyButtons(); // RELOAD BTN UI
        GlobalData.instance.skillManager.EnableBuyButtons();// RELOAD BTN UI
        GlobalData.instance.uiController.SetTxtGold(gold); // RELOAD UI
        GlobalData.instance.saveDataManager.SaveDataGoodsGold(gold); // set save data
    }

    public void AddBone(int value)
    {
        bone += value;
        GlobalData.instance.traningManager.EnableBuyButtons(); // RELOAD BTN UI
        GlobalData.instance.uiController.SetTxtBone(bone); // RELOAD UI
        GlobalData.instance.saveDataManager.SaveDataGoodsBone(bone); // set save data
    }
    public void AddDice(int value)
    {
        diceCount += value;
        GlobalData.instance.uiController.SetTxtDice(diceCount); // RELOAD UI
        GlobalData.instance.saveDataManager.SaveDataGoodsDice(diceCount); // set save data
    }


    public void AddCoal(int value)
    {
        coal += value;
        // set ui;
    }


    public void AddGem(int value)
    {
        gem += value;
        GlobalData.instance.uiController.SetTxtGem(gem); // RELOAD UI
        GlobalData.instance.saveDataManager.SaveDataGoodsGem(gem); // set save data
    }

    public void PayGold(int value)
    {
        gold -= value;
        if (gold < 0) gold = 0;
        GlobalData.instance.traningManager.EnableBuyButtons(); // RELOAD BTN UI
        GlobalData.instance.skillManager.EnableBuyButtons();// RELOAD BTN UI
        GlobalData.instance.uiController.SetTxtGold(gold); // RELOAD UI
        GlobalData.instance.saveDataManager.SaveDataGoodsGold(gold); // set save data
    }

    public void PayBone(int value)
    {
        bone -= value;
        if (bone < 0) bone = 0;
        GlobalData.instance.traningManager.EnableBuyButtons(); // RELOAD BTN UI
        GlobalData.instance.uiController.SetTxtBone(bone); // RELOAD UI
        GlobalData.instance.saveDataManager.SaveDataGoodsBone(bone); // set save data
    }
    public void PayDice(int value )
    {
        diceCount -= value;
        if (diceCount < 0) diceCount = 0;
        GlobalData.instance.uiController.SetTxtDice(diceCount); // RELOAD UI
        GlobalData.instance.saveDataManager.SaveDataGoodsDice(diceCount); // set save data
    }
    public void PayGem(int value)
    {
        gem -= value;
        if (gem < 0) gem = 0;
        GlobalData.instance.uiController.SetTxtGem(gem); // RELOAD UI
        GlobalData.instance.saveDataManager.SaveDataGoodsGem(gem); // set save data
    }
         
    public void PayCoal(int value)
    {
        coal -= value;
        if (coal < 0) coal = 0;

        // set ui

    }

    
    public void AddDungeonKey(GoodsType goodsType , int addKeyCount)
    {
        dungeonKeys[goodsType] += addKeyCount;

        // RELOAD UI
        // ...
    }

    public void PayDungeonKey(GoodsType goodsType, int keyCount)
    {
        dungeonKeys[goodsType] -= keyCount;
        if (dungeonKeys[goodsType] < 0) dungeonKeys[goodsType] = 0;

        // RELOAD UI
        // ...
    }




}


