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

        // ���� ���Ž� ( hp �� �Ǵ� )
        if (IsMonseterKill(currentMonster.hp))
        {
            // hp text 0���� ǥ��
            globalData.uiController.SetTxtMonsterHp(0); 

            // �������� ���� ����� ����
            globalData.insectManager.DisableHalfLineInsects();

            // monster kill animation
            currentMonster.inOutAnimator.MonsterKillAnimWithEvent(); // ��� �ִϸ��̼� ���� �� kill event ����
        }
        // ���� �ܼ� �ǰݽ�
        else
        {
            // ���� hp text
            globalData.uiController.SetTxtMonsterHp(currentMonster.hp);
        }
    }

    IEnumerator MonsterKill(MonsterBase currentMonster , EnumDefinition.InsectType insectType)
    {
        yield return null;

        // hp text 0���� ǥ��
        globalData.uiController.SetTxtMonsterHp(0);

        // �������� ���� ����� ����
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

    // �Ϲ� ���� �����
    void MonsterDie_Normal()
    {
        //phaseCount 0 ���޽� ��� ���� ����.
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



    // ��� ���� �����
    /* process */
    // 0 : ��� ���� ���  
    // 2 : ���� ���� ��ư Ȱ��ȭ
    // 3 : �Ϲ� ���� ������ ����
    // 4 : ���� ���� �Ϲ� ���ͷ� ����
    // 4 : phaaseCount reset
    // 5 : ui ����
    // 6 : ���� ����
    void MonsterDie_Gold()
    {
        // ���� ���� ��ư Ȱ��ȭ 
        globalData.uiController.btnBossChallenge.gameObject.SetActive(true);
        
        // phaseCount ����
        PhaseCountReset();

        // �Ϲ� ���� ����
        MonsterAppear(MonsterType.normal);
    }

    //���� ���� �����
    void MonsterDie_Boss()
    {
        // SET STAGE DATA ( ���� ���������� ���� )

        // ���� ������ ����

        // 
    }


    void EvnOnBossMonsterChalleng()
    {
        // ���� ���� ����
        globalData.player.currentMonster.inOutAnimator.MonsterKillAnimWithOutEvent();

        // ���� ���� �� ���� ��� ����
        globalData.insectManager.DisableHalfLineInsects();

        // ���� ���� ����
        MonsterAppear(MonsterType.boss);
    }

    
    /// <summary> ���� ���� </summary>
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




    // EVENT => ���� UI ���� ( ���� ���� �ִϸ��̼� �Ϸ� �� ȣ�� )
    void EvnOnMonsterUiReset()
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


