using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static EnumDefinition;

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


        // ���� ���Ž�
        if (IsMonseterKill(currentMonster.hp))
        {
          
            // �������� ���� ����� ����
            globalData.insectManager.DisableHalfLineInsects();

            // monster kill animation
            currentMonster.inOutAnimator.MonsterKillAnim();
            globalData.uiController.SetTxtMonsterHp(0);
        
        }
        else
        {
            // set ui
            globalData.uiController.SetTxtMonsterHp(currentMonster.hp);
        }



        
    }


    // MONSTER KILL EVENT

    void OnMonsterKill(EnumDefinition.MonsterType monsterType)
    {
        if(monsterType == EnumDefinition.MonsterType.normal)
        {
            //phaseCount 0 ���޽� ��� ���� ����.
            var phaseCount = globalData.player.currentStageData.phaseCount -= 1;
            if (IsPhaseCountZero(phaseCount))
            {
                Debug.Log("��� ���� ����");
                MonsterAppearGold();
            }
            else
            {
                //reset monster data
                globalData.monsterManager.SetMonsterData(monsterType, globalData.player.stageIdx);

                // Monster In Animation
                StartCoroutine(globalData.player.currentMonster.inOutAnimator.AnimPosition());
            }
        }


       
    }

    // ��� ���� ����
    void MonsterAppearGold()
    {
        globalData.player.currentMonster = globalData.monsterManager.monsterGold;
        //reset monster data
        globalData.monsterManager.SetMonsterData(EnumDefinition.MonsterType.gold, globalData.player.stageIdx);

        // Monster In Animation
        StartCoroutine(globalData.player.currentMonster.inOutAnimator.AnimPosition());


    }


    // ���� UI ����
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


