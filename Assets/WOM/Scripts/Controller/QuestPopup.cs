using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPopup : MonoBehaviour
{
    public QuestManager QuestManager;

    public Button btn_close;
    public Button btn_showListOneDay;
    public Button btn_showListRepeat;

    public GameObject questListOneDay;
    public GameObject questListRepeat;


    public List<QuestSlot> questSlotsOneDay;
    public List<QuestSlot> questSlotsRepeat;

    void Start()
    {
        SetBtnEvents();
    }

    void SetBtnEvents()
    {
        btn_showListOneDay.onClick.AddListener(ShowQuestListOneDay);
        btn_showListRepeat.onClick.AddListener(ShowQuestListRepeat);
        btn_close.onClick.AddListener(ClosePopup);
    }

    void ClosePopup()
    {
        QuestManager.btn_showQuestPopup.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    void ShowQuestListOneDay()
    {
        questListOneDay.SetActive(true);
        questListRepeat.SetActive(false);
    }

    void ShowQuestListRepeat()
    {
        questListOneDay.SetActive(false);
        questListRepeat.SetActive(true);
    }

    void InitOneDayQuestUI(List<QuestData> questDatas)
    {

        for (int i = 0; i < questDatas.Count; i++)
        {

            var data = questDatas[i];
            var slot = questSlotsOneDay[i];

            slot.SetQuestName(data.questName);

        }

    }

    void SetUIQusetSlot(QuestSlot slot, QuestData data)
    {
        slot.SetQuestName(data.questName);
        slot.SetQuestProgress(data);
        slot.SetTxtRewardValue(data.rewardValue.ToString());
        //slot.SetRewardIcon(data.rewardIcon);
        //slot.SetNotifyIcon(data.notifyIcon);
        slot.EnableNotifyIcon(data.usingReward);
    }

    void InitRepeatQuestUI(List<QuestData> questDatas)
    {


    }
}
