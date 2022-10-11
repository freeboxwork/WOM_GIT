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


        // 몬스터 제거시
        if (IsMonseterKill(currentMonster.hp))
        {
            // 몬스터 재 등장 후 공격 가능 ??????
            // GlobalData.instance.attackController.SetAttackableState(false);

            // 하프라인 위쪽 곤충들 제거
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
        //phaseCount 0 도달시 골드 몬스터 등장.
        var phaseCount = globalData.player.currentStageData.phaseCount -= 1;
        if (IsPhaseCountZero(phaseCount))
        {
            Debug.Log("골드 몬스터 등장");

        }
        else
        {
           
            // Monster In Animation
            StartCoroutine(globalData.player.currentMonster.inOutAnimator.AnimPosition());
        }
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


