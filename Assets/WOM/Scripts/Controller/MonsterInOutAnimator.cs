using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterInOutAnimator : MonoBehaviour
{
    public AnimData animDataIn;
    public AnimData animDataDissolve;
    public AnimData animDataHit;
    public Material dissolveMat;
    public Transform startPoint_MonsterIn;
    public Transform endPoint_MonsterIn;
    //public Transform monsterTr;
    public Animator monsterAnim;
    public Collider2D monsterCol;
    public MonsterBase monster;


    bool isAnimPlay;
    void Start()
    {
        
    }

    private void OnDestroy()
    {
        ResetMat();
    }

    void ResetMat()
    {
        dissolveMat.SetFloat("_fade", 1);
    } 


    public IEnumerator AnimPosition()
    {
        // collider disable
        monsterCol.enabled = false;
        ResetMat();

        var startPos = startPoint_MonsterIn.position;
        var enaPos = endPoint_MonsterIn.position;
        
        monsterAnim.SetBool("Run", isAnimPlay = true);

        animDataIn.ResetAnimData();
        while (animDataIn.animTime < 0.999f)
        {
            animDataIn.animTime = (Time.time - animDataIn.animStartTime) / animDataIn.animDuration;
            animDataIn.animValue = EaseValues.instance.GetAnimCurve(animDataIn.animCurveType, animDataIn.animTime);
            transform.position = Vector3.Lerp(startPos, enaPos, animDataIn.animValue);
            yield return null;
        }

        monsterAnim.SetBool("Run", isAnimPlay = false);
        // collider enable
        monsterCol.enabled = true;

        // set hp event
        // TODO : EVENT CONTROLLER 에서 코루틴으로 제어
        EventManager.instance.RunEvent(CallBackEventType.TYPES.OnMonsterUiReset);
    }

    // TODO : EVENT CONTROLLER 에서 코루틴으로 제어
    public void MonsterKillAnimWithEvent()
    {
        StartCoroutine(MonsterDieMatAnim(dissolveMat, "_fade", (1, 0), true));
    }
    // TODO : EVENT CONTROLLER 에서 코루틴으로 제어
    public void MonsterKillAnimWithOutEvent()
    {
        StartCoroutine(MonsterDieMatAnim(dissolveMat, "_fade", (1, 0), false));
    }

    public IEnumerator MonsterKillMatAnim()
    {
        // collider disable
        monsterCol.enabled = false;

        isAnimPlay = true;
        animDataDissolve.ResetAnimData();
        while (animDataDissolve.animTime < 0.999f)
        {
            animDataDissolve.animTime = (Time.time - animDataDissolve.animStartTime) / animDataDissolve.animDuration;
            //Debug.Log(animDataDissolve.animTime);
            animDataDissolve.animValue = EaseValues.instance.GetAnimCurve(animDataDissolve.animCurveType, animDataDissolve.animTime);
            var value = Mathf.Lerp(1f, 0f, animDataDissolve.animValue);
            dissolveMat.SetFloat("_fade", value);
            yield return null;
        }
        isAnimPlay = false;
        transform.position = startPoint_MonsterIn.position;

        // TODO : EVENT CONTROLLER 에서 코루틴으로 제어
        
        //if (runKillEvnet)
        //    EventManager.instance.RunEvent<EnumDefinition.MonsterType>(CallBackEventType.TYPES.OnMonsterKill, monster.monsterType);

    }
    public void MonsterHitAnim()
    {
        StartCoroutine(MonsterHitMatAnim(dissolveMat, "_hit", (0.4f, 0)));
    }
         

    public IEnumerator MonsterHitMatAnim(Material mat, string property, (float, float) minMax)
    {
        // collider disable
        animDataHit.ResetAnimData();
        while (animDataHit.animTime < 0.999f)
        {
            animDataHit.animTime = (Time.time - animDataHit.animStartTime) / animDataHit.animDuration;
            animDataHit.animValue = EaseValues.instance.GetAnimCurve(animDataHit.animCurveType, animDataHit.animTime);
            var value = Mathf.Lerp(minMax.Item1, minMax.Item2, animDataHit.animValue);
            mat.SetFloat(property, value);
            yield return null;
        }
        
    }


    public IEnumerator MonsterDieMatAnim(Material mat, string property, (float, float) minMax, bool runKillEvnet)
    {
        // collider disable
        monsterCol.enabled = false;

        isAnimPlay = true;
        animDataDissolve.ResetAnimData();
        while (animDataDissolve.animTime < 0.999f)
        {
            animDataDissolve.animTime = (Time.time - animDataDissolve.animStartTime) / animDataDissolve.animDuration;
            //Debug.Log(animDataDissolve.animTime);
            animDataDissolve.animValue = EaseValues.instance.GetAnimCurve(animDataDissolve.animCurveType, animDataDissolve.animTime);
            var value = Mathf.Lerp(minMax.Item1, minMax.Item2, animDataDissolve.animValue);
            mat.SetFloat(property, value);
            yield return null;
        }
        isAnimPlay = false;
        transform.position = startPoint_MonsterIn.position;

        // TODO : EVENT CONTROLLER 에서 코루틴으로 제어
        if (runKillEvnet)
            EventManager.instance.RunEvent<EnumDefinition.MonsterType>(CallBackEventType.TYPES.OnMonsterKill,monster.monsterType);

    }

    //입맛에 맞게 수정하세요. -> 감사합니다!
    public void GetHitAnimation()
    {
        monsterAnim.SetTrigger("Hit");
    }

}
