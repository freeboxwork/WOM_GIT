using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class GlobalMessageTwoBtnPopup : MonoBehaviour
{

    public TextMeshProUGUI txtMessage;
    public Button btnApply;
    public Button btnCancel;
    
    void Start()
    {
        
    }



    public void SetTxtMessage(string message)
    {
        txtMessage.text = message;
    }

    public void SetBtnApplyEvent(UnityAction<bool> action)
    {
        btnApply.onClick.RemoveAllListeners();
        btnApply.onClick.AddListener(()=> { 
            action.Invoke(true); 
            gameObject.SetActive(false); 
        });   
    }
    public void SetBtnCancelEvent(UnityAction<bool> action)
    {
        btnCancel.onClick.RemoveAllListeners();
        btnCancel.onClick.AddListener(()=> { 
            action.Invoke(true); 
            gameObject.SetActive(false); 
        });
    }

}
