using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;


public class DungeonPopup : MonoBehaviour
{
    public TextMeshProUGUI txtRewardAmount;
    public Button btnApply;
    public Image imageCurrencyIcon;



    public void SetDungeonPopup(Sprite icon,float reward)
    {
        txtRewardAmount.text = string.Format("{0}", reward);//재화 보상량
        imageCurrencyIcon.sprite = icon;//보상 재화의 아이콘 종류
    }

    public void SetBtnApplyEvent(UnityAction action)
    {
        btnApply.onClick.RemoveAllListeners();
        btnApply.onClick.AddListener(() => {
            action.Invoke();
            gameObject.SetActive(false);
        });
    }

}
