using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInOutAnimator : MonoBehaviour
{
    public AnimData animDataIn;
    public AnimData animDataDissolve;
    public Material dissolveMat;
    public Transform startPoint_MonsterIn;
    public Transform endPoint_MonsterIn;
    public Transform monsterTr;
    public Animator monsterAnim;

    bool isAnimPlay;
    void Start()
    {
        
    }


    public IEnumerator AnimPosition()
    {
        var startPos = startPoint_MonsterIn.position;
        var enaPos = endPoint_MonsterIn.position;
        
        monsterAnim.SetBool("Run", isAnimPlay = true);

        animDataIn.ResetAnimData();
        while (animDataIn.animTime < 0.999f)
        {
            animDataIn.animTime = (Time.time - animDataIn.animStartTime) / animDataIn.animDuration;
            Debug.Log(animDataIn.animTime);
            animDataIn.animValue = EaseValues.instance.GetAnimCurve(animDataIn.animCurveType, animDataIn.animTime);
            monsterTr.position = Vector3.Lerp(startPos, enaPos, animDataIn.animValue);
            yield return null;
        }

        monsterAnim.SetBool("Run", isAnimPlay = false);
    }


    public IEnumerator MaterialAnimMinMax(Material mat, string property, (float, float) minMax)
    {
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
    }

    //입맛에 맞게 수정하세요.
    public void GetHitAnimation()
    {
        monsterAnim.SetTrigger("Hit");
    }

}
