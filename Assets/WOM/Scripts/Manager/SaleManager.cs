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
        
        //��ȭ : GOLD
        if (IsGoldItem(statType))
        {
            // ��ȭ ��� ���� �Ǵ�
            if (IsValidPurchaseGold(payData.statData.salePrice))
            {
                // ���� 
                player.PayGold(payData.statData.salePrice);
                player.SetStatLevel(statType, payData.statData.level) ;

                // ���� ������ ����

                // UI ����
            }
            else
            {
                // ��尡 ���� �մϴ�.
                Debug.Log($"{statType} - ��尡 ���� �մϴ�.");
            }
        }
        //��ȭ : BONE
        else
        {
             
        }

        yield return null;
    }

    (int level , StatSaleData statData) GetPayData( SaleStatType statType)
    {
        (int, StatSaleData) items;
        var statDatas = GlobalData.instance.dataManager.GetSaleStatDataByType(statType);
        items.Item1 = player.GetStatLevel(statType);
        items.Item2 = GetStatSaleData(statDatas, items.Item1 + 1);
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

}
