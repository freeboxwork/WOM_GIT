using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;


public class DungeonPopup : MonoBehaviour
{
    public TextMeshProUGUI txtRewardAmount;
    public Button btnApply;
    public Image imageCurrencyIcon;

    public SerializableDictionary<EnumDefinition.GoodsType, Sprite> goodsToIconMap;
    public event Action OnButtonClick;

    private void Start()
    {
        
    }
    private void Awake()
    {
        btnApply.onClick.AddListener(ButtonClickEvent);
    }


    public void SetDungeonPopup(EnumDefinition.GoodsType goodsType,int reward)
    {
        txtRewardAmount.text = string.Format("{0}", reward);//재화 보상량
        imageCurrencyIcon.sprite = goodsToIconMap[goodsType];//보상 재화의 아이콘 종류
    }

    public void ButtonClickEvent()
    {
        OnButtonClick.Invoke();
        gameObject.SetActive(false);
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
