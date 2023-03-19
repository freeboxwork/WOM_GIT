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
        txtRewardAmount.text = string.Format("{0}", reward);//��ȭ ����
        imageCurrencyIcon.sprite = icon;//���� ��ȭ�� ������ ����
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
