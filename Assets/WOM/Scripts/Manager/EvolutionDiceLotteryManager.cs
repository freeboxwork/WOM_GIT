using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionDiceLotteryManager : MonoBehaviour
{

    /* 진화 주사위 뽑기 프로세스
    0. 수사위 Lock 풀려 있는 개수대로 x 10 + (1회당 주사위 10개)
    1. grede 확률 뽑기
    2. grade 데이터 불러 오기
    3. 데이터에서 랜덤하게 능력치 뽑기
    4. 뽑아온 능력치 적용
     */

    // 랜덤 가중치.
    float[] weightValues;
    RewardDiceEvolutionDatas rewardData;


    void Start()
    {
        
    }

    /// <summary> 뽑기 필요 데이터 세팅</summary>
    public IEnumerator Init()
    {
        rewardData = GlobalData.instance.dataManager.rewardDiceEvolutionDatas;
        
        // 랜덤 가중치 설정
        SetWeightValues();
        yield return null;  
    }

    void SetWeightValues()
    {
        var dataCount = rewardData.data.Count;
        weightValues = new float[dataCount];
        for (int i = 0; i < dataCount; i++)
        {
            weightValues[i] = rewardData.data[i].gradeProbability;
        }
    }
}
