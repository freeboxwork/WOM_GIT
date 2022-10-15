using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static EnumDefinition;
using static UnityEngine.Rendering.DebugUI;

public class EventController : MonoBehaviour
{
    GlobalData globalData;
    List<IEnumerator> processMonsterDieList = new List<IEnumerator>();
    void Start()
    {
        GetManagers();
        AddEvents();
        SetProcessFunctionList();
    }

    private void OnDestroy()
    {
        RemoveEvents();
    }

    void GetManagers()
    {
        globalData = GlobalData.instance;
    }

    void SetProcessFunctionList()
    {
        // 0 : normal , 1 : gold , 2 : boss
        processMonsterDieList.Add(MonsterDie_Normal());
        processMonsterDieList.Add(MonsterDie_Gold());
        processMonsterDieList.Add(MonsterDie_Boss());
    }

    void AddEvents()
    {
        EventManager.instance.AddCallBackEvent<EnumDefinition.InsectType>(CallBackEventType.TYPES.OnMonsterHit, EvnOnMonsterHit);
        EventManager.instance.AddCallBackEvent(CallBackEventType.TYPES.OnBossMonsterChallenge, EvnOnBossMonsterChalleng);
    }   
    
    void RemoveEvents()
    {
        EventManager.instance.RemoveCallBackEvent<EnumDefinition.InsectType>(CallBackEventType.TYPES.OnMonsterHit, EvnOnMonsterHit);
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

        // ���� ���Ž� ( hp �� �Ǵ� )
        if (IsMonseterKill(currentMonster.hp))
        {
            // hp text 0���� ǥ��
            globalData.uiController.SetTxtMonsterHp(0); 

            // �������� ���� ����� ����
            globalData.insectManager.DisableHalfLineInsects();

            StartCoroutine(MonsterKill(currentMonster));
        }
        // ���� �ܼ� �ǰݽ�
        else
        {
            // ���� hp text
            globalData.uiController.SetTxtMonsterHp(currentMonster.hp);
        }
    }

    IEnumerator MonsterKill(MonsterBase currentMonster )
    {
        yield return null;

        // hp text 0���� ǥ��
        globalData.uiController.SetTxtMonsterHp(0);

        // �������� ���� ����� ����
        globalData.insectManager.DisableHalfLineInsects();

        // monster kill animation ��� �ִϸ��̼� ���
        yield return StartCoroutine(currentMonster.inOutAnimator.MonsterKillMatAnim());

        switch (currentMonster.monsterType)
        {
            case MonsterType.normal: StartCoroutine(MonsterDie_Normal()); break;
            case MonsterType.gold: StartCoroutine(MonsterDie_Gold()); break;
            case MonsterType.boss: StartCoroutine(MonsterDie_Gold()); break;
        }

    }

    // �Ϲ� ���� �����
    IEnumerator MonsterDie_Normal()
    {
        //phaseCount 0 ���޽� ��� ���� ����.
        PhaseCounting(out int phaseCount);
        if (IsPhaseCountZero(phaseCount)) 
        {
           yield return StartCoroutine( MonsterAppearCor(MonsterType.gold));  
        }
        else
        {
            yield return StartCoroutine(MonsterAppearCor(MonsterType.normal));
        }
    }


    #region ��� ���� ����� ���μ���
    // ��� ���� �����
    /* process */
    // 0 : ��� ���� ���  
    // 2 : ���� ���� ��ư Ȱ��ȭ
    // 3 : �Ϲ� ���� ������ ����
    // 4 : ���� ���� �Ϲ� ���ͷ� ����
    // 4 : phaaseCount reset
    // 5 : ui ����
    // 6 : ���� ����
    #endregion
    IEnumerator MonsterDie_Gold()
    {
        // ���� ���� ��ư Ȱ��ȭ 
        globalData.uiController.btnBossChallenge.gameObject.SetActive(true);
        
        // phaseCount ����
        PhaseCountReset();

        // �Ϲ� ���� ����
        yield return StartCoroutine(MonsterAppearCor(MonsterType.normal));
    }

    //���� ���� �����
    IEnumerator MonsterDie_Boss()
    {
        // SET STAGE DATA ( ���� ���������� ���� )

        // ���� ������ ����

        yield return null;
    }

    // ���� ����
    IEnumerator MonsterAppearCor(EnumDefinition.MonsterType monsterType)
    {
        // get curret monster data
        var monsterData = globalData.monsterManager.GetMonsterData(monsterType);
        // set current monster
        globalData.player.currentMonster = monsterData;
        // set monster data
        globalData.monsterManager.SetMonsterData(monsterType, globalData.player.stageIdx);
        // Monster In Animation
        yield return StartCoroutine(globalData.player.currentMonster.inOutAnimator.AnimPosition());
        // ���� UI ���� 
        MonsterUiReset();
    }


    // ���� ���� ���� ��ư �������� �̺�Ʈ
    void EvnOnBossMonsterChalleng()
    {
        StartCoroutine(ProcessBossMonsterChallenge());
    }

    IEnumerator ProcessBossMonsterChallenge()
    {
        // ���� ���� ���� ( ��� �ִϸ��̼� ��� )
        yield return StartCoroutine(globalData.player.currentMonster.inOutAnimator.MonsterKillMatAnim());

        // ���� ���� �� ���� ��� ����
        globalData.insectManager.DisableHalfLineInsects();

        // ���� ���� ��ư ����
        globalData.uiController.btnBossChallenge.gameObject.SetActive(false);

        // ���� ���� Ÿ�̸� Ȱ��ȭ
        globalData.uiController.imgBossMonTimerParent.gameObject.SetActive(true);

        // ���� ���� ����
        StartCoroutine(MonsterAppearCor(MonsterType.boss));

        // Ÿ�̸� ��� ����
        globalData.bossChallengeTimer.StartTimer();
    }
    
    // ���� ���� �ð����� ���� ��������.
    void EvnBossMonsterTimeOut()
    {
        // ����(���� ���� ���� ��) phaseCount ���� ����� ?? -> ��� ���� �����ϸ� �� phase count �� ���� ī���� �ǰ� ������ �ϳ��� ���������� ��� ���� �����ʹ� ��� ������.
    }
         

    void MonsterUiReset()
    {
        var monsterData  = globalData.player.currentMonster;

        // SET HP
        globalData.uiController.SetTxtMonsterHp(monsterData.hp);
        // SLIDE BAR

    }



    /* UTILITY METHOD */
    #region UTILITY MEHTOD
    // ���� ���� �Ǵ�
    bool IsMonseterKill(float monster_hp)
    {
        return monster_hp <= 0;
    }

    // ��� ���� ���� �ܰ� �Ǵ�
    bool IsPhaseCountZero(int phaseCount)
    {
        return phaseCount <= 0;
    }
    
    // ��� ���� ���� �ܰ� ī����
    void PhaseCounting(out int value)
    {
        value = globalData.player.currentStageData.phaseCount -= 1;
        globalData.uiController.SetTxtPhaseCount(value);
    }

    // ��� ���� ���� �ܰ� ����
    void PhaseCountReset()
    {
        var resetValue = globalData.player.pahseCountOriginalValue;
        globalData.player.currentStageData.phaseCount = resetValue;
        globalData.uiController.SetTxtPhaseCount(resetValue);
    }

    #endregion
}


