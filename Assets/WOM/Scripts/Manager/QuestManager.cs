using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EnumDefinition;

public class QuestManager : MonoBehaviour
{

    public Button btn_showQuestPopup;
    public QuestPopup questPopup;

    private Dictionary<QuestTypeOneDay, QuestData> questsOneDay = new Dictionary<QuestTypeOneDay, QuestData>();

    void Start()
    {

    }


    IEnumerator Init()
    {
        SetBtnEvent();
        AddQuestData();
        yield return null;
    }

    void SetBtnEvent()
    {
        btn_showQuestPopup.onClick.AddListener(() =>
        {
            questPopup.gameObject.SetActive(true);
            btn_showQuestPopup.gameObject.SetActive(false);
        });
    }



    void AddQuestData()
    {
        foreach (QuestData data in GlobalData.instance.dataManager.questDatasOneDay.data)
        {
            questsOneDay.Add(GetQuestTypeOneDayByTypeName(data.questType), data);
        }
    }

    QuestTypeOneDay GetQuestTypeOneDayByTypeName(string typeName)
    {
        foreach (QuestTypeOneDay type in System.Enum.GetValues(typeof(QuestTypeOneDay)))
            if (type.ToString() == typeName)
                return type;
        return QuestTypeOneDay.none;
    }

    public void IncreaseCountOneDayQuest(QuestTypeOneDay type)
    {
        if (questsOneDay.ContainsKey(type))
        {
            if (questsOneDay[type].qusetComplete == false)
                questsOneDay[type].curCountValue++;

            if (IsReachedTargetCount(type))
            {
                questsOneDay[type].qusetComplete = true;
                // EVENT 실행
            }


        }
        else
        {
            Debug.LogError("IncreaseCount Error : " + type.ToString() + " is not exist in questsOneDay");
        }
    }

    bool IsCountValid(QuestTypeOneDay type)
    {
        return questsOneDay[type].curCountValue < questsOneDay[type].targetValue;
    }

    bool IsReachedTargetCount(QuestTypeOneDay type)
    {
        return questsOneDay[type].curCountValue <= questsOneDay[type].targetValue && questsOneDay[type].qusetComplete == false;
    }






    // public void IncreaseCount(QuestTypeOneDay type)
    // {
    //     if (!questsOneDay.ContainsKey(type))
    //     {
    //         questsOneDay[type] = 0;
    //     }

    //     questsOneDay[type]++;
    // }

    // public void ResetCount(QuestTypeOneDay type)
    // {
    //     questsOneDay[type] = 0;
    // }

    // public int GetCount(QuestTypeOneDay type)
    // {
    //     return questsOneDay.ContainsKey(type) ? questsOneDay[type] : 0;
    // }

}
