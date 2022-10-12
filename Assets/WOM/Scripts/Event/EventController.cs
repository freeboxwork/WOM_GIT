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


        // ���� ���Ž� ( hp �� �Ǵ� )
        if (IsMonseterKill(currentMonster.hp))
        {
            // hp text 0���� ǥ��
            globalData.uiController.SetTxtMonsterHp(0); 

            // �������� ���� ����� ����
            globalData.insectManager.DisableHalfLineInsects();

            // monster kill animation
            currentMonster.inOutAnimator.MonsterKillAnim(); // ��� �ִϸ��̼� ���� �� kill event ����
        }
        // ���� �ܼ� �ǰݽ�
        else
        {
            // ���� hp t
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
     
        globalData.uiController.btnBossChallenge.gameObject.SetActive(true);
        // phaseCount ����
        PhaseCountReset();

        MonsterAppear(MonsterType.normal);
    }

    //���� ���� �����
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
    void OnMonsterUiReset()
    {
        var monsterData  = globalData.player.currentMonster;

        // SET HP
        globalData.uiController.SetTxtMonsterHp(monsterData.hp);
        // SLIDE BAR

    }
    


    /* UTILITY METHOD */

    // ���� ���� �Ǵ�
    bool IsMonseterKill(float monster_hp)
    {
        return monster_hp <= 0;
    }

    bool IsPhaseCountZero(int phaseCount)
    {
        return phaseCount <= 0;
    }
}


