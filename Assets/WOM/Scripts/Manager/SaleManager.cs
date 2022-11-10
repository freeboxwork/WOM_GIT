using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EnumDefinition;

public class SaleManager : MonoBehaviour
{
    Player player;
    Queue<SaleStatMsgData> saleStatMsgs = new Queue<SaleStatMsgData>();

    void Start()
    {
        GetPlayer();
        StartCoroutine( SandMessage());
    }
   
    void GetPlayer()
    {
        player = GlobalData.instance.player;
    }

    public void AddData(SaleStatMsgData data)
    {
        saleStatMsgs.Enqueue(data);
    }
        

    IEnumerator SandMessage()
    {
        while (true)
        {
            if(saleStatMsgs.Count > 0)
            {
                var data = saleStatMsgs.Dequeue();
                yield return StartCoroutine(PurchaseStatByType(data.saleStatType));
            }
            yield return null;
        }
    }

    
    public IEnumerator PurchaseStatByType(SaleStatType statType)
    {
        // get current level & next statData by statType
        var payData = GetPayData(statType);

        // 최대 레벨 체크
        if (payData.isValidMaximumLevel)
        {
            //재화 : GOLD
            if (IsGoldItem(statType))
            {
                // 재화 충분 한지 판단
                if (IsValidPurchaseGold(payData.statData.salePrice))
                {
                    // 구매 
                    player.PayGold(payData.statData.salePrice);

                    // 데이터 적용
                    player.SetStatLevel(statType, payData.statData.level);


                    // UI 적용
                }
                else
                {
                    // 골드가 부족 합니다.
                    Debug.Log($"{statType} - 골드가 부족 합니다.");
                }
            }
            //재화 : BONE
            else
            {
                if (IsValidPurchaseBone(payData.statData.salePrice))
                {
                    // 구매
                    player.PayGold(payData.statData.salePrice);

                    // 데이터 적용
                    player.SetStatLevel(statType, payData.statData.level);


                    // UI 적용
                }
                else
                {
                    // 뼈 조각이 부족 합니다.
                    Debug.Log($"{statType} - 뼈 조각이 부족합니다.");
                }
            }
        }
        else
        {
            // 최대 레벨 도달
            Debug.Log($"{statType} - 최대 레벨에 도달했습니다..");
        }

        yield return null;
    }

    (int level , StatSaleData statData, bool isValidMaximumLevel) GetPayData( SaleStatType statType)
    {
        (int, StatSaleData,bool) items;
        var statDatas = GlobalData.instance.dataManager.GetSaleStatDataByType(statType);
        items.Item1 = player.GetStatLevel(statType);
        // 맥시멈 체크
        items.Item3 = statDatas.data.Last().level > items.Item1;
        if (items.Item3)
        {
            items.Item2 = GetStatSaleData(statDatas, items.Item1 + 1);
        }
        else
        {
            items.Item2 = null;
        }
        
        return items;
    }


    StatSaleData GetStatSaleData(StatSaleDatas data, int level)
    {
        return data.data.FirstOrDefault(f => f.level == level);
    }

    bool IsGoldItem(SaleStatType saleStat)
    {
        return (saleStat == SaleStatType.trainingDamage || saleStat == SaleStatType.trainingCriticalChance || saleStat == SaleStatType.trainingCriticalDamage);
    }


    



    //UTILITY 

    bool IsValidPurchaseGold(int value)
    {
        return 0 < player.gold - value;
    }


    bool IsValidPurchaseBone(int value)
    {
        return 0 < player.bone - value;
    }


    bool IsValidMaximumLevel(SaleStatType statType ,int curLevel)
    {
        int maxumLevel = 100;
        return maxumLevel > curLevel;
    }
    

}
