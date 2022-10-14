using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterChallengeTimer : MonoBehaviour
{
    public AnimData animData;
    bool isTimerCalc = false;
  
    void Start()
    {
        
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    StartCoroutine(CalcTimer());    
        //}
    }


    public IEnumerator CalcTimer()
    {
        isTimerCalc = true;
        animData.ResetAnimData();
        
        // Ÿ�̸� ���� �̺�Ʈ ?

        while (animData.animTime < 0.999f)
        {
            int timeSecond = (int)animData.animDuration - (int)(Time.time - animData.animStartTime);
            animData.animTime = (Time.time - animData.animStartTime) / animData.animDuration;
            //Debug.Log((int)(Time.time - animData.animStartTime));
            animData.animValue = EaseValues.instance.GetAnimCurve(animData.animCurveType, animData.animTime);
        
            GlobalData.instance.uiController.SetTxtBossChallengeTimer(timeSecond);
            GlobalData.instance.uiController.SetImgFilledRaidal(animData.animValue);
            
            yield return null;
        }

        // Ÿ�̸� ���� �̺�Ʈ
        // EventManager.instance.RunEvent(CallBackEventType.TYPES.OnBossMonsterChallengeTimerEnd);
   
    }


}
