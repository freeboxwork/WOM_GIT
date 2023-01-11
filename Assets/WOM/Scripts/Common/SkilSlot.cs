using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SkilSlot : MonoBehaviour
{
    public TextMeshProUGUI txtLevel;
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtDescription;  
    public TextMeshProUGUI txtMaxLevel;
    public TextMeshProUGUI txtCost;
    public Button btnPay;
    public EnumDefinition.SkillType skillType;

    void Start()
    {
        SetBtnEvent();
    }


    void SetBtnEvent()
    {
        btnPay.onClick.AddListener(() => { 
        
        });
    }

    public void SetTxt_Level(string value)
    {
        txtLevel.text = value;
    }

    public void SetTxt_Name(string value)
    {
        txtName.text = value;
    }

    public void SetTxt_Description(string value)
    {
        txtDescription.text = value;
    }

    public void SetTxt_MaxLevel(string value)
    {
        txtMaxLevel.text = value;
    }

    public void SetTxt_Cost(string value)
    {
        txtCost.text = value;
    }

    // 초기 데이터 세팅
    //public void SetUIBySkillData(int currentLevel,SkillData data)
    //{
    //    SetTxt_Level(data.)
    //}


}
