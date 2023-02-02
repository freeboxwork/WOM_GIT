using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static EnumDefinition;
using static UnityEngine.Rendering.DebugUI;
/// <summary>
/// 몬스터전 이벤트 관리
/// </summary>
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
        EventManager.instance.AddCallBackEvent(CallBackEventType.TYPES.OnBossMonsterChallengeTimeOut, EvnBossMonsterTimeOut);
        EventManager.instance.AddCallBackEvent(CallBackEventType.TYPES.OnBossMonsterChallenge, EvnOnBossMonsterChalleng);
        EventManager.instance.AddCallBackEvent(CallBackEventType.TYPES.OnEvolutionMonsterChallenge, EvnOnEvolutionGradeChallenge);
        
    }

    void RemoveEvents()
    {
        EventManager.instance.RemoveCallBackEvent<EnumDefinition.InsectType>(CallBackEventType.TYPES.OnMonsterHit, EvnOnMonsterHit);
        EventManager.instance.RemoveCallBackEvent(CallBackEventType.TYPES.OnBossMonsterChallengeTimeOut, EvnBossMonsterTimeOut);
        EventManager.instance.RemoveCallBackEvent(CallBackEventType.TYPES.OnBossMonsterChallenge, EvnOnBossMonsterChalleng);
        EventManager.instance.RemoveCallBackEvent(CallBackEventType.TYPES.OnEvolutionMonsterChallenge, EvnOnEvolutionGradeChallenge);
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
            StartCoroutine(MonsterKill(currentMonster));
        }
        // 몬스터 단순 피격시
        else
        {
            // 몬스터 hp text
            globalData.uiController.SetTxtMonsterHp(currentMonster.hp);

            // 몬스터 hp slider
            globalData.uiController.SetSliderMonsterHp(currentMonster.hp); 

        }
    }

    IEnumerator MonsterKill(MonsterBase currentMonster)
    {
        // 공격 막기
        globalData.attackController.SetAttackableState(false);

        yield return null;

        // GOLD 획득 애니메이션
        StartCoroutine(globalData.effectManager.goldPoolingCont.EnableGoldEffects(currentMonster.goldCount));
        // 골드 획득
        GainGold(currentMonster);


        // 보스일경우 뼈조각 추가 획득
        if (currentMonster.monsterType == MonsterType.boss)
        {
            StartCoroutine(globalData.effectManager.bonePoolingCont.EnableGoldEffects(currentMonster.boneCount));
            // 뼈 조각 획득
            GainBone(currentMonster);
        }


        //// set glod text ui
        //globalData.uiController.SetTxtGold(globalData.player.gold);

        //// set bone text ui
        //globalData.uiController.SetTxtBone(globalData.player.bone);

        // hp text 0으로 표시
        globalData.uiController.SetTxtMonsterHp(0);

        // hp slider
        globalData.uiController.SetSliderMonsterHp(0);

        // 하프라인 위쪽 곤충들 제거
        globalData.insectManager.DisableHalfLineInsects();

        // monster kill animation 사망 애니메이션 대기
        yield return StartCoroutine(currentMonster.inOutAnimator.MonsterKillMatAnim());

        switch (currentMonster.monsterType)
        {
            case MonsterType.normal: StartCoroutine(MonsterDie_Normal()); break;
            case MonsterType.gold: StartCoroutine(MonsterDie_Gold()); break;
            case MonsterType.boss: StartCoroutine(MonsterDie_Boss()); break;
            case MonsterType.evolution: StartCoroutine(MonsterDie_Evolution()); break;
        }

    }

    void GainGold(MonsterBase monster)
    {
        var gold = monster.gold;
        globalData.player.AddGold(gold);
    }
    void GainBone(MonsterBase monster)
    {
        var bone = monster.bone;
        globalData.player.AddBone(bone);
    }


    // 일반 몬스터 사망시
    IEnumerator MonsterDie_Normal()
    {
        // BG Scroll Animation
        globalData.stageManager.PlayAnimBgScroll();
        
        //phaseCount 0 도달시 골드 몬스터 등장.
        PhaseCounting(out int phaseCount);
        if (IsPhaseCountZero(phaseCount)) 
        {
            // 보스 도전 버튼 숨김 ( 보스 도전 버튼 있다면 숨김 - 무조건 숨김 )
            globalData.uiController.btnBossChallenge.gameObject.SetActive(false);
            yield return StartCoroutine( MonsterAppearCor(MonsterType.gold));  
        }
        else
        {
            yield return StartCoroutine(MonsterAppearCor(MonsterType.normal));
        }
    }


    #region 골드 몬스터 사망시 프로세스
    // 골드 몬스터 사망시
    /* process */
    // 0 : 골드 몬스터 사망  
    // 2 : 보스 도전 버튼 활성화
    // 3 : 일반 몬스터 데이터 세팅
    // 4 : 현재 몬스터 일반 몬스터로 변경
    // 4 : phaaseCount reset
    // 5 : ui 세팅
    // 6 : 몬스터 등장
    #endregion
    IEnumerator MonsterDie_Gold()
    {
        // BG Scroll Animation
        globalData.stageManager.PlayAnimBgScroll();

        // 보스 도전 버튼 활성화 
        globalData.uiController.btnBossChallenge.gameObject.SetActive(true);
        
        // 보스 도전 가능 상태 설정
        globalData.player.isBossMonsterChllengeEnable = true;   

        // phaseCount 리셋
        PhaseCountReset();

        // 일반 몬스터 등장
        yield return StartCoroutine(MonsterAppearCor(MonsterType.normal));
    }

    //보스 몬스터 사망시
    IEnumerator MonsterDie_Boss()
    {
        // 타이머 종료
        globalData.bossChallengeTimer.StopAllCoroutines();

        // 타이머 UI 리셋
        globalData.uiController.SetImgTimerFilledRaidal(0);
        globalData.uiController.SetTxtBossChallengeTimer(0);

        // 타이머 UI Disable
        globalData.uiController.imgBossMonTimerParent.gameObject.SetActive(false);

        // 보스 도전 가능 상태 설정
        globalData.player.isBossMonsterChllengeEnable = false;

        // SET STAGE DATA ( 다음 스테이지로 변경 )
        globalData.player.stageIdx++;

        // current stage setting
        globalData.player.SetCurrentStageData(globalData.player.stageIdx);

        // stage setting - stage manager 스테이지 데이터와 배경 이미지 전환 애니메이션
        yield return StartCoroutine( globalData.stageManager.SetStageById(globalData.player.stageIdx));

        // set monster data and monster skin
        yield return StartCoroutine(globalData.monsterManager.Init(globalData.player.stageIdx));

        yield return StartCoroutine(MonsterAppearCor(MonsterType.normal));
    }

    //진화 몬스터 사망시
    IEnumerator MonsterDie_Evolution()
    {
        // 보스 도전 버튼 활성화 
        if (globalData.player.isBossMonsterChllengeEnable)
            globalData.uiController.btnBossChallenge.gameObject.SetActive(true);

        // 타이머 종료
        globalData.bossChallengeTimer.StopAllCoroutines();

        // 타이머 UI 리셋
        globalData.uiController.SetImgTimerFilledRaidal(0);
        globalData.uiController.SetTxtBossChallengeTimer(0);

        // 타이머 UI Disable
        globalData.uiController.imgBossMonTimerParent.gameObject.SetActive(false);

        // 진화 보상 지급 및 UI 세팅
        globalData.evolutionManager.SetUI_Pannel_Evolution(globalData.evolutionManager.evalutionLeveldx + 1);

        // 능력치 슬롯 오픈
        globalData.evolutionManager.SetUI_EvolutionSlots(globalData.evolutionManager.evalutionLeveldx + 1);

        // 유니온 장착 슬롯 오픈
        globalData.unionManager.UnlockEquipSlots(globalData.evolutionManager.evalutionLeveldx + 1);


        // 기존 UI Canvas 비활성화
        UtilityMethod.GetCustomTypeGMById(6).SetActive(false);

        // TODO : 진화 UI 리셋

        // 등급 업그레이드 연출 등장
        globalData.gradeAnimCont.gradeIndex = globalData.evolutionManager.evalutionLeveldx+1;
        globalData.gradeAnimCont.gameObject.SetActive(true);

        // 진화 idx 레벨업
        globalData.evolutionManager.evalutionLeveldx++;

        // 일반 몬스터 등장
        yield return StartCoroutine(MonsterAppearCor(MonsterType.normal));

        //진화 몬스터 도전 버튼 활성화
        globalData.evolutionManager.EnableBtnEvolutionMonsterChange(true);

        //진화 메뉴 활성화
        globalData.uiController.EnableMenuPanel(MenuPanelType.evolution);
    }

    // 몬스터 등장
    IEnumerator MonsterAppearCor(EnumDefinition.MonsterType monsterType)
    {
        // 골드 OUT EFFECT ( 골드 화면에 뿌려진 경우에만 )
        StartCoroutine(globalData.effectManager.goldPoolingCont.DisableGoldEffects());

        // 보스의 경우 뼈조각 OUT EFF 추가 ( 뼈조각 화면에 뿌려진 경우에만 )
        StartCoroutine(globalData.effectManager.bonePoolingCont.DisableGoldEffects());
        
        // get curret monster data
        var monsterData = globalData.monsterManager.GetMonsterData(monsterType);


        
        // set current monster
        globalData.player.SetCurrentMonster(monsterData);

        // set prev monster type
        globalData.player.SetPervMonsterType(monsterType);

        // set current monster type
        globalData.player.SetCurrentMonsterType(monsterType);

        // set monster data
        if (monsterType == MonsterType.evolution)
        {
            globalData.monsterManager.SetMonsterDataOther(monsterType, globalData.evolutionManager.evalutionLeveldx);
        }
        else
        {
            globalData.monsterManager.SetMonsterData(monsterType, globalData.player.stageIdx);
        }

        // Monster In Animation
        yield return StartCoroutine(globalData.player.currentMonster.inOutAnimator.AnimPositionIn());

        // set current monster hp
        // TODO: 이펙트 연출 추가
        globalData.player.SetCurrentMonsterHP(monsterData.hp);
        

        // 몬스터 UI 리셋 
        MonsterUiReset();

        // 공격 가능 상태 변경
        globalData.attackController.SetAttackableState(true);
    }


    #region 진화전 프로세스
    /*
     0 : 트랜지션 인
     1 : 몬스터 및 스테이지 세팅
     2 : 트랜지션 아웃
     3 : 진화전 몬스터 등장
     4 : 몬스터 사냥
     5 : 몬스터 사냥 성공 -> 진화
     6 : 몬스터 사냥 실패 -> 이전 몬스터 등장  
    */
    #endregion

    // 진화전 도전 버튼 눌렀을때 ( 진화 몬스터 사냥 )
    void EvnOnEvolutionGradeChallenge()
    {
        StartCoroutine(ProcessEvolutionGradeChallenge());
    }

    IEnumerator ProcessEvolutionGradeChallenge()
    {

        // 진화전 화면전환 이펙트
        yield return StartCoroutine(globalData.effectManager.EffTransitioEvolutionUpgrade(() => {
            
            // 보스 도전 버튼 숨김
            globalData.uiController.btnBossChallenge.gameObject.SetActive(false);
            
            // 하프 라인 위 곤충 모두 제거
            globalData.insectManager.DisableHalfLineInsects();

            // 일반 몬스터 OUT
            StartCoroutine(globalData.player.currentMonster.inOutAnimator.MonsterKillMatAnim());

            // 보스 도전 타이머 활성화
            globalData.uiController.imgBossMonTimerParent.gameObject.SetActive(true);

            // 타이머 시간 설정
            globalData.bossChallengeTimer.SetTimeValue(30f);

            // 타이머 계산 시작
            globalData.bossChallengeTimer.StartTimer();

        }));

        // 진화 몬스터 등장
        StartCoroutine(MonsterAppearCor(MonsterType.evolution));
    }

    // 보스 몬스터 도전 버튼 눌렀을때 이벤트
    void EvnOnBossMonsterChalleng()
    {
        StartCoroutine(ProcessBossMonsterChallenge());
    }

    IEnumerator ProcessBossMonsterChallenge()
    {

        // 하프 라인 위 곤충 모두 제거
        globalData.insectManager.DisableHalfLineInsects();

        // 일반 몬스터 OUT
        yield return StartCoroutine(globalData.player.currentMonster.inOutAnimator.MonsterKillMatAnim());

        // 보스 도전 버튼 숨김
        globalData.uiController.btnBossChallenge.gameObject.SetActive(false);

        // 보스 도전 타이머 활성화
        globalData.uiController.imgBossMonTimerParent.gameObject.SetActive(true);

        // 타이머 시간 설정
        globalData.bossChallengeTimer.SetTimeValue(30f);

        // 보스 몬스터 등장
        StartCoroutine(MonsterAppearCor(MonsterType.boss));

        // 타이머 계산 시작
        globalData.bossChallengeTimer.StartTimer();
    }
    
    // 보스 몬스터 시간내에 잡지 못했을때.
    void EvnBossMonsterTimeOut()
    {
        // 현재(보스 몬스터 도전 전) phaseCount 몬스터 재등장 ?? -> 노멀 몬스터 등장하면 됨 phase count 는 따로 카운팅 되고 있으며 하나의 스테이지에 노멀 몬스터 데이터는 모두 동일함.
        switch (globalData.player.curMonsterType)
        {
            case MonsterType.boss: StartCoroutine(ProcessBossMonsterTimeOut()); break;
            case MonsterType.evolution: StartCoroutine(ProcessEvolutionMonsterTimeOut()); break;
        }
    }
         
    IEnumerator ProcessBossMonsterTimeOut()
    {
        // 하프 라인 위 곤충 모두 제거
        globalData.insectManager.DisableHalfLineInsects();

        // 보스 몬스터 OUT
        yield return StartCoroutine(globalData.player.currentMonster.inOutAnimator.AnimPositionOut());

        // 보스 도전 버튼 숨김
        globalData.uiController.btnBossChallenge.gameObject.SetActive(true);

        // 보스 도전 타이머 비활성화
        globalData.uiController.imgBossMonTimerParent.gameObject.SetActive(false);

        // 일반 몬스터 등장
        StartCoroutine(MonsterAppearCor(MonsterType.normal));

    }

    IEnumerator ProcessEvolutionMonsterTimeOut()
    {

        // 하프 라인 위 곤충 모두 제거
        globalData.insectManager.DisableHalfLineInsects();

        yield return StartCoroutine(globalData.globalPopupController.EnableGlobalPopupCor("message", 0));

        // 화면전환 이펙트
        yield return StartCoroutine(globalData.effectManager.EffTransitioEvolutionUpgrade(() =>
          {
              // 보스 몬스터 OUT
              StartCoroutine(globalData.player.currentMonster.inOutAnimator.AnimPositionOut());

              // 보스 도전 타이머 비활성화
              globalData.uiController.imgBossMonTimerParent.gameObject.SetActive(false);

          }));

        // 일반 몬스터 등장
        StartCoroutine(MonsterAppearCor(MonsterType.normal));

        // 진화 몬스터 도전 버튼 활성화
        globalData.evolutionManager.EnableBtnEvolutionMonsterChange(true);

        //진화 메뉴 활성화
        globalData.uiController.EnableMenuPanel(MenuPanelType.evolution);
    }


    // 진화전 포기 했을때
    public IEnumerator ProcessEvolutionMonsterGiveUp()
    {
       
        // 진화전 포기 버튼 비활성화
        UtilityMethod.GetCustomTypeBtnByID(30).gameObject.SetActive(false);

        // 하프 라인 위 곤충 모두 제거
        globalData.insectManager.DisableHalfLineInsects();

        // 화면전환 이펙트
        yield return StartCoroutine(globalData.effectManager.EffTransitioEvolutionUpgrade(() =>
        {
            // 보스 몬스터 OUT
            StartCoroutine(globalData.player.currentMonster.inOutAnimator.AnimPositionOut());

            // 보스 도전 타이머 비활성화
            globalData.uiController.imgBossMonTimerParent.gameObject.SetActive(false);

        }));

        // 일반 몬스터 등장
        StartCoroutine(MonsterAppearCor(MonsterType.normal));

        // 진화 몬스터 도전 버튼 활성화
        globalData.evolutionManager.EnableBtnEvolutionMonsterChange(true);

        //진화 메뉴 활성화
        globalData.uiController.EnableMenuPanel(MenuPanelType.evolution);

    }


    void MonsterUiReset()
    {
        var monsterData  = globalData.player.currentMonster;

        // SET HP
        globalData.uiController.SetTxtMonsterHp(monsterData.hp);
        // SLIDE BAR
        globalData.uiController.SetSliderMonsterHp(monsterData.hp);
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
        globalData.uiController.SetSliderPhaseValue(value);
    }

    // 골드 몬스터 진입 단계 리셋
    void PhaseCountReset()
    {
        var resetValue = globalData.player.pahseCountOriginalValue;
        globalData.player.currentStageData.phaseCount = resetValue;
        globalData.uiController.SetTxtPhaseCount(resetValue);
        globalData.uiController.SetSliderPhaseValue(resetValue);    
    }

    #endregion
}


