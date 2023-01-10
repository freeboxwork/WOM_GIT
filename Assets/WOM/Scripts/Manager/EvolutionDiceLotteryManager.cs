using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static EnumDefinition;

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
    
    // 주사위 굴리고 있는지 판단
    bool rollDice = false;

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


    public void RollEvolutionDice()
    {
        // TODO : 오픈된 슬롯 갯수만큼 뽑아야 함. 현재는 한번만 뽑음
        if(rollDice == false)
        {
            StartCoroutine(DiceRoll());
        }
    }
         


    // 주사위 굴리기
    
    IEnumerator DiceRoll()
    {
        yield return null;

        // 현재 주사위 사용 개수 판단
        // 기본 10 + (10 X unlock count)
        var usingDice = UtilityMethod.GetEvolutionDiceUsingCount();

        // 주사위 개수 충분한지 판단
        if (IsReadyDiceCount(usingDice))
        {

            rollDice = true;

            // 주사위 사용
            GlobalData.instance.player.PayDice(usingDice);

            // 남은 주사위 UI 표시
            UtilityMethod.SetTxtCustomTypeByID(64, GlobalData.instance.player.diceCount.ToString());

            // 랜덤 그레이드 뽑기
            var randomGradeData = GetRandomWeightEvolutionGradeData();


            // 랜덤 능력치 뽑기
            var statValue = GetRandomStatValue(randomGradeData);

            Debug.Log($"랜덤하게 뽑은 능력치값 :  {statValue}");

            // 능력치 적용
        }
        else
        {
            GlobalData.instance.globalPopupController.EnableGlobalPopupByMessageId("진화전 주사위 굴리기", 1);
        }


        rollDice = false;
    }

    bool IsReadyDiceCount(int usingDiceCount)
    {
        return GlobalData.instance.player.diceCount >  usingDiceCount;
    }

    RewardDiceEvolutionData GetRandomWeightEvolutionGradeData()
    {
        int grade = (int)UtilityMethod.GetWeightRandomValue(weightValues);
        return GlobalData.instance.dataManager.GetRewardDiceEvolutionDataByGradeId(grade + 1);
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        for (int i = 0; i < 1000; i++)
    //        {
    //            Debug.Log(GetRandomStatType());

    //        }
          
    //    }
    //}

    EvolutionDiceStatType GetRandomStatType()
    {
        var randomValue = Random.Range(0, 7);
        return (EvolutionDiceStatType)randomValue;
    }

    float GetRandomStatValue (RewardDiceEvolutionData data)
    {
        var statType = GetRandomStatType();
        Debug.Log($"랜덤하게 뽑은 능력치값 타입 :  {statType}");
        switch (statType)
        {
            case EvolutionDiceStatType.insectDamage: return data.insectDamage;
            case EvolutionDiceStatType.insectCriticalChance: return data.insectCriticalChance;
            case EvolutionDiceStatType.insectCriticalDamage: return data.insectCriticalDamage;
            case EvolutionDiceStatType.goldBonus: return data.goldBonus;
            case EvolutionDiceStatType.insectMoveSpeed: return data.insectMoveSpeed;
            case EvolutionDiceStatType.insectSpawnTime: return data.insectSpawnTime;
            case EvolutionDiceStatType.insectBossDamage: return data.insectBossDamage;
        }
        return 0;
    }

    //void PrintGrade()
    //{

    //    var value = $"grade : {data.grade} / probability :{data.gradeProbability}";
    //    Debug.Log(value);
    //}
}
