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

        // �ִ� ���� üũ
        if (payData.isValidMaximumLevel)
        {
            //��ȭ : GOLD
            if (IsGoldItem(statType))
            {
                // ��ȭ ��� ���� �Ǵ�
                if (IsValidPurchaseGold(payData.statData.salePrice))
                {
                    // ���� 
                    player.PayGold(payData.statData.salePrice);

                    // ������ ����
                    player.SetStatLevel(statType, payData.statData.level);


                    // UI ����
                    if (payData.nextStatSaleData != null)
                    {
                        GlobalData.instance.uiController.SetTxtTraningValues(statType, new float[] { payData.statData.value, payData.nextStatSaleData.value });
                        //Debug.Log($"{statType}�� ���� ���� ���Ȱ� : { payData.statData.value} - ���� ���� ���Ȱ� : {payData.nextStatSaleData.value} ");
                    }
                    else // �������� ����
                    {
                        Debug.Log("�������� ����");
                    }
                    

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
                if (IsValidPurchaseBone(payData.statData.salePrice))
                {
                    // ����
                    player.PayBone(payData.statData.salePrice);

                    // ������ ����
                    player.SetStatLevel(statType, payData.statData.level);

                    // UI ����
                    if (payData.nextStatSaleData != null)
                    {
                        GlobalData.instance.uiController.SetTxtTraningValues(statType, new float[] { payData.statData.value, payData.nextStatSaleData.value });
                        //Debug.Log($"{statType}�� ���� ���� : {payData.nextStatSaleData.level} , ���� ���� ���Ȱ� : {payData.nextStatSaleData.value} ");
                    }
                    else // �������� ����
                    {
                        Debug.Log("�������� ����");
                    }
                }
                else
                {
                    // �� ������ ���� �մϴ�.
                    Debug.Log($"{statType} - �� ������ �����մϴ�.");
                }
            }
        }
        else
        {
            // �ִ� ���� ����
            Debug.Log($"{statType} - �ִ� ������ �����߽��ϴ�..");
        }

        yield return null;
    }

    (int level , StatSaleData statData, bool isValidMaximumLevel, StatSaleData nextStatSaleData) GetPayData( SaleStatType statType)
    {
        (int m_level, StatSaleData m_statData,bool m_isValidMaxLevel, StatSaleData m_nextStatData) items;
        var statDatas = GlobalData.instance.dataManager.GetSaleStatDataByType(statType);
        items.m_level = player.GetStatLevel(statType);
        // �ƽø� üũ
        var lastData = statDatas.data.Last();
        items.m_isValidMaxLevel =  lastData.level > items.m_level;
        if (items.m_isValidMaxLevel)
        {
            items.m_statData = GetStatSaleData(statDatas, items.m_level + 1);
        }
        else
        {
            items.m_statData = null;
        }

        

        // ���� ���� ������ ���� �ϴ��� üũ
        if(lastData.level > items.m_level+2)
        {
            items.m_nextStatData = GetStatSaleData(statDatas, items.m_level + 2);
        }
        else
        {
            items.m_nextStatData = null;
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
