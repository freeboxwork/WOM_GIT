using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{

    public Button btn_showQuestPopup;
    public QuestPopup questPopup;

    
    void Start()
    {
        SetBtnEvent();
    }

    void SetBtnEvent()
    {
        btn_showQuestPopup.onClick.AddListener(() => {
            questPopup.gameObject.SetActive(true);
            btn_showQuestPopup.gameObject.SetActive(false);
        });
    }

   
}
