using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
}
