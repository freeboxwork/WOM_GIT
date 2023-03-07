using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnionSpwanTimer : MonoBehaviour
{
    public UnionSpwanManager spwanManager;
    public float spwanTime;
    public bool isTimerReady;
    public int timerIndex;

    UnionSlot unionSlot;
    

    void Start()
    {
        
    }

    public void SetSpwanTime(float time)
    {
        spwanTime = time;   
    }
    
    public void TimerStart( UnionSlot unionSlot )
    {
        this.unionSlot = unionSlot;
        isTimerReady = true;
        SetSpwanTime(unionSlot.inGameData.spawnTime);
        StartCoroutine(SpwanTimer());
    }

    public void TimerStop()
    {
        isTimerReady = false;
        unionSlot = null;
        StopAllCoroutines();
    }

    IEnumerator SpwanTimer()
    {
        while (isTimerReady)
        {

            // yield return new WaitUntil(() => GlobalData.instance.attackController.GetAttackableState() == true);

            // set union data
            var union = GlobalData.instance.insectManager.GetDisableUnion();
            union.inGameData = unionSlot.inGameData;
            
            // set union face
            var sprite = spwanManager.spriteFileData.GetSpriteData(unionSlot.inGameData.unionIndex);
            union.SetInsectFace(sprite);
            
            // set position
            var randomPos = spwanManager.GetRandomPos();
            union.gameObject.transform.position = randomPos;
            
            // spwan time ���
            yield return new WaitForSeconds(spwanTime);

            // enable insect
            union.gameObject.SetActive(true);

            Debug.Log($"Ÿ�̸� �ε��� : {timerIndex} _ ���� ���Ͽ� : {sprite[0].name} _ ���� Ÿ�� : {spwanTime}");

        
        }
    }



}
