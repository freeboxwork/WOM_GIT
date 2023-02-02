using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine.Events;

public class GlobalPopupController : MonoBehaviour
{
    public GameObject popupSet;
    public TextMeshProUGUI txtTtile;
    public TextMeshProUGUI txtMessage;

    public Button btnConfirm;
    
    void Start()
    {
        SetButtonEvent();
    }


    void SetButtonEvent()
    {
        btnConfirm.onClick.AddListener(() => {
            popupSet.SetActive(false);
        });
    }

    public void EnableGlobalPopupByMessageId(string title,int messageId)
    {
        SetTxtTitle(title);
        SetTxtMessage(GetMessageById(messageId));
        popupSet.SetActive(true);
    }


    public void EnableGlobalPopup(string title, string message)
    {
        SetTxtTitle(title);
        SetTxtMessage(message);
        popupSet.SetActive(true);
    }


    public void EnableGlobalPopupEventByMesageID(string title, int messageId, UnityAction evn)
    {
        btnConfirm.onClick.RemoveAllListeners();
        btnConfirm.onClick.AddListener(()=> {
            evn.Invoke();
            ResetBtnEvent();
        });

        SetTxtTitle(title);
        SetTxtMessage(GetMessageById(messageId));
        popupSet.SetActive(true);

    }

    public void EnableGlobalPopupOutValueByMesageID(string title, int messageId, out bool value)
    {
        bool confirmValue = false;
        btnConfirm.onClick.RemoveAllListeners();
        btnConfirm.onClick.AddListener(() => {
            confirmValue = true;
            ResetBtnEvent();
        });

        SetTxtTitle(title);
        SetTxtMessage(GetMessageById(messageId));
        popupSet.SetActive(true);


        value = confirmValue; 
    }

    public IEnumerator EnableGlobalPopupCor(string title, int messageId)
    {
        bool confirmValue = false;
        btnConfirm.onClick.RemoveAllListeners();
        btnConfirm.onClick.AddListener(() => {
            confirmValue = true;
            ResetBtnEvent();
        });

        SetTxtTitle(title);
        SetTxtMessage(GetMessageById(messageId));
        popupSet.SetActive(true);

        yield return new WaitUntil(() => confirmValue);
    }

    // remove event
    void ResetBtnEvent()
    {
        btnConfirm.onClick.RemoveAllListeners();
        btnConfirm.onClick.AddListener(() => {
            popupSet.SetActive(false);
        });

        popupSet.SetActive(false);
    }

    string GetMessageById(int id)
    {
        //TODO: 영문 국문 시세템 언어에 따라 적용
        return GlobalData.instance.dataManager.GetGlobalMessageDataById(id).message_kor;
    }


    void SetTxtTitle(string value)
    {
        txtTtile.text = value;
    }
    void SetTxtMessage(string value)
    {
        txtMessage.text = value;
    }




}
