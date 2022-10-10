using System.Collections;
using System.Collections.Generic;
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
    }   
    
    void RemoveEvents()
    {
        EventManager.instance.RemoveCallBackEvent<EnumDefinition.InsectType>(CallBackEventType.TYPES.OnMonsterHit, OnMonsterHit);
    }



    // MONSTER HIT EVENT

    void OnMonsterHit(EnumDefinition.InsectType insectType)
    {
        // get damage
        var damage = globalData.insectManager.GetInsectDamage(insectType);
        //Debug.Log(insectType + "damage : " + damage);

        // set monster damage
        var currentMonster = globalData.player.currentMonster;
        currentMonster.hp -= damage;


        // 몬스터 제거시
        if (IsMonseterKill(currentMonster.hp))
        {
            //phaseCount 0 도달시 골드 몬스터 등장.
            var phaseCount = globalData.player.currentStageData.phaseCount -= 1;
            if (IsPhaseCountZero(phaseCount))
            {
            
            }
        }



        // set ui
        globalData.uiController.SetTxtMonsterHp(currentMonster.hp);

    }

    bool IsMonseterKill(float monster_hp)
    {
        return monster_hp <= 0;
    }

    bool IsPhaseCountZero(int phaseCount)
    {
        return phaseCount <= 0;
    }
}


