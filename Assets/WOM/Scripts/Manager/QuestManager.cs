using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EnumDefinition;
using System.Linq;


public class QuestManager : MonoBehaviour
{

    public Button btn_showQuestPopup;
    public QuestPopup questPopup;

    private Dictionary<QuestTypeOneDay, QuestData> questsOneDay = new Dictionary<QuestTypeOneDay, QuestData>();

    const string keyUsingReward = "_usingReward";
    const string keyQuestComplete = "_questComplete";



    void Start()
    {
        AddEvents();
    }

    void OnDestroy()
    {
        RemoveEvents();
    }

    void AddEvents()
    {
        EventManager.instance.AddCallBackEvent<QuestTypeOneDay>(CallBackEventType.TYPES.OnQusetClearOneDayCounting, IncreaseCountOneDayQuest);
        EventManager.instance.AddCallBackEvent<QuestData>(CallBackEventType.TYPES.OnQusetUsingRewardOneDay, EvnUsingReward);
    }

    void RemoveEvents()
    {
        EventManager.instance.RemoveCallBackEvent<QuestTypeOneDay>(CallBackEventType.TYPES.OnQusetClearOneDayCounting, IncreaseCountOneDayQuest);
        EventManager.instance.RemoveCallBackEvent<QuestData>(CallBackEventType.TYPES.OnQusetUsingRewardOneDay, EvnUsingReward);
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
                ++quest.curCountValue;
                if (quest.curCountValue >= quest.targetValue)
                {
                    quest.qusetComplete = true;
                }

                //ui update , playerprefs save event 실행
                var slot = questPopup.GetQuestSlotByQuestTypeOneDay(type);
                slot.UpdateUI(quest);

                // quest 의 변동 사항을 로그로 출력
                Debug.Log("퀘스트 카운트 증가 : " + type.ToString() + " 현재 카운트 : " + quest.curCountValue + " / " + quest.targetValue);
                // save data
                SaveQuestData(quest);

                if (AllQuestComplete()) // 일일 퀘스트 전체 완료 체크
                {
                    // 일일 퀘스트 완료 : 모든 일일 퀘스트 완료
                    EventManager.instance.RunEvent<EnumDefinition.QuestTypeOneDay>(CallBackEventType.TYPES.OnQusetClearOneDayCounting, EnumDefinition.QuestTypeOneDay.allComplete);
                }
            }
        }
        else
        {
            Debug.LogError("퀘스트 카운트 에러 : " + type.ToString() + " 해당 퀘스트가 존재 하지 않습니다.");
        }
    }


    bool AllQuestComplete()
    {
        return questsOneDay.Values.All(quest => quest.qusetComplete);
    }


    // user quest data save
    void SaveQuestData(QuestData data)
    {
        PlayerPrefs.SetInt(data.questType, data.curCountValue);
        PlayerPrefs.SetInt(data.questType + keyQuestComplete, data.qusetComplete ? 1 : 0);
    }

    void EvnUsingReward(QuestData data)
    {
        data.usingReward = true;
        PlayerPrefs.SetInt(data.questType + keyUsingReward, data.usingReward ? 1 : 0);
        // 리워드 지급
        GlobalData.instance.rewardManager.RewardByType(GetRewardTypeByTypeName(data.rewardType), data.rewardValue);
    }

    EnumDefinition.RewardType GetRewardTypeByTypeName(string typeName)
    {
        foreach (RewardType type in System.Enum.GetValues(typeof(RewardType)))
            if (type.ToString() == typeName)
                return type;
        return RewardType.none;
    }

}
