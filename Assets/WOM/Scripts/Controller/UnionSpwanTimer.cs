using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnionSpwanTimer : MonoBehaviour
{
    public UnionSpwanManager spwanManager;
    public float spwanTime;
    public bool isTimerReady;
    public int timerIndex;

    void Start()
    {
        
    }

    public void SetSpwanTime(float time)
    {
        spwanTime = time;   
    }
    
    public void TimerStart(InsectBullet insectBullet)
    {
        isTimerReady = true;
        StartCoroutine(SpwanTimer(insectBullet));
    }

    public void TimerStop()
    {
        isTimerReady= false;
        StopAllCoroutines();
    }

    IEnumerator SpwanTimer(InsectBullet insectBullet)
    {
        while (isTimerReady)
        {
            //yield return new WaitForSeconds(spwanTime);
            // TODO: insect bullet 불러 오는 방식 수정
            yield return new WaitForSeconds(3f);

            var randomPos = spwanManager.GetRandomPos();
            insectBullet.gameObject.transform.position = randomPos;
            insectBullet.gameObject.SetActive(true);
        }
    }



}
