using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
    }   
    
    void RemoveEvents()
    {
        EventManager.instance.RemoveCallBackEvent<EnumDefinition.InsectType>(CallBackEventType.TYPES.OnMonsterHit, OnMonsterHit);
        EventManager.instance.RemoveCallBackEvent<EnumDefinition.MonsterType>(CallBackEventType.TYPES.OnMonsterKill, OnMonsterKill);
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
            // ���� �� ���� �� ���� ���� ??????
            // GlobalData.instance.attackController.SetAttackableState(false);

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
        //phaseCount 0 ���޽� ��� ���� ����.
        var phaseCount = globalData.player.currentStageData.phaseCount -= 1;
        if (IsPhaseCountZero(phaseCount))
        {
            Debug.Log("��� ���� ����");

        }
        else
        {
           
            // Monster In Animation
            StartCoroutine(globalData.player.currentMonster.inOutAnimator.AnimPosition());
        }
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


