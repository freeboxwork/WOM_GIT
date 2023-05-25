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


    public IEnumerator Init()
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
        var oneDayData = GlobalData.instance.dataManager.questDatasOneDay.data;
        for (int i = 0; i < oneDayData.Count; i++)
        {
            var clonData = oneDayData[i].CloneInstance();
            var slot = questPopup.questSlotsOneDay[i];
            questsOneDay.Add(GetQuestTypeOneDayByTypeName(clonData.questType), clonData);
            questPopup.SetUIQusetSlot(slot, clonData);
        }
        // foreach (QuestData data in GlobalData.instance.dataManager.questDatasOneDay.data)
        // {
        //     var clonData = data.CloneInstance();

        // }
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

            var quest = questsOneDay[type];
            if (!quest.qusetComplete)
            {
                //ui update , playerprefs event 실행
                quest.curCountValue++;
                if (quest.curCountValue >= quest.targetValue)
                {
                    quest.qusetComplete = true;
                }

                var slot = questPopup.GetQuestSlotByQuestTypeOneDay(type);
                slot.UpdateUI(quest);
            }
        }
        else
        {
            Debug.LogError("퀘스트 카운트 에러 : " + type.ToString() + " 해당 퀘스트가 존재 하지 않습니다.");
        }
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
