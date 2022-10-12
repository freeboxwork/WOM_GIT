using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static EnumDefinition;
using static UnityEngine.Rendering.DebugUI;

public class EventController : MonoBehaviour
{
    GlobalData globalData;

    void Start()
    {
        GetManagers();
        AddEvents();
    }

    private void OnDestroy()
    {
        RemoveEvents();
    }

    void GetManagers()
    {
        globalData = GlobalData.instance;
    }


    void AddEvents()
    {
        EventManager.instance.AddCallBackEvent<EnumDefinition.InsectType>(CallBackEventType.TYPES.OnMonsterHit, OnMonsterHit);
        EventManager.instance.AddCallBackEvent<EnumDefinition.MonsterType>(CallBackEventType.TYPES.OnMonsterKill, OnMonsterKill);
        EventManager.instance.AddCallBackEvent(CallBackEventType.TYPES.OnMonsterUiReset, OnMonsterUiReset);
    }   
    
    void RemoveEvents()
    {
        EventManager.instance.RemoveCallBackEvent<EnumDefinition.InsectType>(CallBackEventType.TYPES.OnMonsterHit, OnMonsterHit);
        EventManager.instance.RemoveCallBackEvent<EnumDefinition.MonsterType>(CallBackEventType.TYPES.OnMonsterKill, OnMonsterKill);
        EventManager.instance.RemoveCallBackEvent(CallBackEventType.TYPES.OnMonsterUiReset, OnMonsterUiReset);
    }



    // MONSTER HIT EVENT

    void OnMonsterHit(EnumDefinition.InsectType insectType)
    {

        // GET DAMAGE
        var damage = globalData.insectManager.GetInsectDamage(insectType);

        // GET MONSTER
        var currentMonster = globalData.player.currentMonster;

        // set monster damage
        currentMonster.hp -= damage;

        // monster hit animation 
        currentMonster.inOutAnimator.monsterAnim.SetBool("Hit",true);


        // 몬스터 제거시 ( hp 로 판단 )
        if (IsMonseterKill(currentMonster.hp))
        {
            // hp text 0으로 표시
            globalData.uiController.SetTxtMonsterHp(0); 

            // 하프라인 위쪽 곤충들 제거
            globalData.insectManager.DisableHalfLineInsects();

            // monster kill animation
            currentMonster.inOutAnimator.MonsterKillAnim(); // 사망 애니메이션 진행 후 kill event 실행
        }
        // 몬스터 단순 피격시
        else
        {
            // 몬스터 hp t
            globalData.uiController.SetTxtMonsterHp(currentMonster.hp);
        }
    }


    // MONSTER KILL EVENT

    void OnMonsterKill(EnumDefinition.MonsterType monsterType)
    {
        switch (monsterType)
        {
            case MonsterType.normal : MonsterDie_Normal(); break;
            case MonsterType.gold   : MonsterDie_Gold();   break;
            case MonsterType.boss   : MonsterDie_Boss();   break;
        }
    }

    // 일반 몬스터 사망시
    void MonsterDie_Normal()
    {
        //phaseCount 0 도달시 골드 몬스터 등장.
        PhaseCounting(out int phaseCount);
        if (IsPhaseCountZero(phaseCount)) 
        {
            MonsterAppear(MonsterType.gold);  
        }
        else
        {
            //reset monster data
            globalData.monsterManager.SetMonsterData(EnumDefinition.MonsterType.normal, globalData.player.stageIdx);
            // Monster In Animation
            StartCoroutine(globalData.player.currentMonster.inOutAnimator.AnimPosition());
        }
    }

    // 골드 몬스터 사망시
    /* process */
    // 0 : 골드 몬스터 사망  
    // 2 : 보스 도전 버튼 활성화
    // 3 : 일반 몬스터 데이터 세팅
    // 4 : 현재 몬스터 일반 몬스터로 변경
    // 4 : phaaseCount reset
    // 5 : ui 세팅
    // 6 : 몬스터 등장
    void MonsterDie_Gold()
    {
     
        globalData.uiController.btnBossChallenge.gameObject.SetActive(true);
        // phaseCount 리셋
        PhaseCountReset();

        MonsterAppear(MonsterType.normal);
    }

    //보스 몬스터 사망시
    void MonsterDie_Boss()
    {

    }

    void PhaseCounting(out int value)
    {
        value = globalData.player.currentStageData.phaseCount -= 1;
        globalData.uiController.SetTxtPhaseCount(value);
    }
         
    void PhaseCountReset()
    {
        var resetValue = 10;
        globalData.player.currentStageData.phaseCount = resetValue;
        globalData.uiController.SetTxtPhaseCount(resetValue);
    }


    /// <summary> 몬스터 등장 </summary>
    void MonsterAppear(EnumDefinition.MonsterType monsterType)
    {
        // get curret monster data
        var monsterData = globalData.monsterManager.GetMonsterData(monsterType);
        // set current monster
        globalData.player.currentMonster = monsterData;
        // set monster data
        globalData.monsterManager.SetMonsterData(monsterType, globalData.player.stageIdx);
        // Monster In Animation
        StartCoroutine(globalData.player.currentMonster.inOutAnimator.AnimPosition());
    }




    // EVENT => 몬스터 UI 리셋 ( 몬스터 등장 애니메이션 완료 후 호출 )
    void OnMonsterUiReset()
    {
        var monsterData  = globalData.player.currentMonster;

        // SET HP
        globalData.uiController.SetTxtMonsterHp(monsterData.hp);
        // SLIDE BAR

    }
    


    /* UTILITY METHOD */

    // 몬스터 제거 판단
    bool IsMonseterKill(float monster_hp)
    {
        return monster_hp <= 0;
    }

    bool IsPhaseCountZero(int phaseCount)
    {
        return phaseCount <= 0;
    }
}


