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
        EventManager.instance.AddCallBackEvent<EnumDefinition.InsectType>(CallBackEventType.TYPES.OnMonsterHit, EvnOnMonsterHit);
        EventManager.instance.AddCallBackEvent<EnumDefinition.MonsterType>(CallBackEventType.TYPES.OnMonsterKill, EvnOnMonsterKill);
        EventManager.instance.AddCallBackEvent(CallBackEventType.TYPES.OnMonsterUiReset, EvnOnMonsterUiReset);
        EventManager.instance.AddCallBackEvent(CallBackEventType.TYPES.OnBossMonsterChallenge, EvnOnBossMonsterChalleng);
    }   
    
    void RemoveEvents()
    {
        EventManager.instance.RemoveCallBackEvent<EnumDefinition.InsectType>(CallBackEventType.TYPES.OnMonsterHit, EvnOnMonsterHit);
        EventManager.instance.RemoveCallBackEvent<EnumDefinition.MonsterType>(CallBackEventType.TYPES.OnMonsterKill, EvnOnMonsterKill);
        EventManager.instance.RemoveCallBackEvent(CallBackEventType.TYPES.OnMonsterUiReset, EvnOnMonsterUiReset);
        EventManager.instance.RemoveCallBackEvent(CallBackEventType.TYPES.OnBossMonsterChallenge, EvnOnBossMonsterChalleng);
    }



    // MONSTER HIT EVENT

    void EvnOnMonsterHit(EnumDefinition.InsectType insectType)
    {

        // GET DAMAGE
        var damage = globalData.insectManager.GetInsectDamage(insectType);

        // GET MONSTER
        var currentMonster = globalData.player.currentMonster;

        // set monster damage
        currentMonster.hp -= damage;

        // monster hit animation 
        currentMonster.inOutAnimator.monsterAnim.SetBool("Hit",true);

        // monster hit shader effect
        currentMonster.inOutAnimator.MonsterHitAnim();

        // 몬스터 제거시 ( hp 로 판단 )
        if (IsMonseterKill(currentMonster.hp))
        {
            // hp text 0으로 표시
            globalData.uiController.SetTxtMonsterHp(0); 

            // 하프라인 위쪽 곤충들 제거
            globalData.insectManager.DisableHalfLineInsects();

            // monster kill animation
            currentMonster.inOutAnimator.MonsterKillAnimWithEvent(); // 사망 애니메이션 진행 후 kill event 실행
        }
        // 몬스터 단순 피격시
        else
        {
            // 몬스터 hp text
            globalData.uiController.SetTxtMonsterHp(currentMonster.hp);
        }
    }

    IEnumerator MonsterKill(MonsterBase currentMonster , EnumDefinition.InsectType insectType)
    {
        yield return null;

        // hp text 0으로 표시
        globalData.uiController.SetTxtMonsterHp(0);

        // 하프라인 위쪽 곤충들 제거
        globalData.insectManager.DisableHalfLineInsects();

        // monster kill animation
        yield return StartCoroutine(currentMonster.inOutAnimator.MonsterKillMatAnim());


        switch (currentMonster.monsterType)
        {

            case MonsterType.normal: 
                
                

                break;

        }
        


    }


    // MONSTER KILL EVENT

    void EvnOnMonsterKill(EnumDefinition.MonsterType monsterType)
    {
        // GET GOLD

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
        // 보스 도전 버튼 활성화 
        globalData.uiController.btnBossChallenge.gameObject.SetActive(true);
        
        // phaseCount 리셋
        PhaseCountReset();

        // 일반 몬스터 등장
        MonsterAppear(MonsterType.normal);
    }

    //보스 몬스터 사망시
    void MonsterDie_Boss()
    {
        // SET STAGE DATA ( 다음 스테이지로 변경 )

        // 몬스터 데이터 세팅

        // 
    }


    void EvnOnBossMonsterChalleng()
    {
        // 현재 몬스터 제거
        globalData.player.currentMonster.inOutAnimator.MonsterKillAnimWithOutEvent();

        // 하프 라인 위 곤충 모두 제거
        globalData.insectManager.DisableHalfLineInsects();

        // 보스 몬스터 등장
        MonsterAppear(MonsterType.boss);
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
    void EvnOnMonsterUiReset()
    {
        var monsterData  = globalData.player.currentMonster;

        // SET HP
        globalData.uiController.SetTxtMonsterHp(monsterData.hp);
        // SLIDE BAR

    }



    /* UTILITY METHOD */
    #region UTILITY MEHTOD
    // 몬스터 제거 판단
    bool IsMonseterKill(float monster_hp)
    {
        return monster_hp <= 0;
    }

    // 골드 몬스터 진입 단계 판단
    bool IsPhaseCountZero(int phaseCount)
    {
        return phaseCount <= 0;
    }
    
    // 골드 몬스터 진입 단계 카운팅
    void PhaseCounting(out int value)
    {
        value = globalData.player.currentStageData.phaseCount -= 1;
        globalData.uiController.SetTxtPhaseCount(value);
    }

    // 골드 몬스터 진입 단계 리셋
    void PhaseCountReset()
    {
        var resetValue = globalData.player.pahseCountOriginalValue;
        globalData.player.currentStageData.phaseCount = resetValue;
        globalData.uiController.SetTxtPhaseCount(resetValue);
    }

    #endregion
}


