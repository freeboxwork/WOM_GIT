using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnionSlot : MonoBehaviour
{
    public EnumDefinition.UnionGradeType unionGradeType;
    public EnumDefinition.UnionEquipType unionEquipType;
    public int unionId;
    public int curLevel;   // 현재 레벨
    public int unionCount; // 유니온 보수 수
    public int LevelUpReqirementCount;         // 레벨업에 필요한 유니온 수
    public TextMeshProUGUI txtLevel;           // 현재 레벨
    public TextMeshProUGUI txtEquipState;      // 장착 여부
    public TextMeshProUGUI txtReqirementCount; // 레벨업에 필요한 유니온 수
    public Slider sliderReqirement;
    public Image imgUnionFace;
    public Button btn;
    public GameObject unlock;
    public bool isUnlock = false;

    void Start()
    {
        
    }

   void SetBtnEvent()
    {

    }

   
    public void AddUnion(int count)
    {
        unionCount += count;
    }
        
    public void PayUnion(int count)
    {
        unionCount -= count;
    }


    public void SetUIImageUnion(Sprite unionFace)
    {
        imgUnionFace.sprite = unionFace;    
    }


    public void SetUITxtUnionCount()
    {
        var text = $"{unionCount}/{LevelUpReqirementCount}";
        txtReqirementCount.text = text;
    }

    public void SetUITxtLevel()
    {
        txtLevel.text = "Lv" + curLevel.ToString();
    }

    public void SetUITxtUnionEquipState()
    {
        var equipTxt = unionEquipType == EnumDefinition.UnionEquipType.Equipped ? "장착중" : "";
        txtEquipState.text = equipTxt;
    }

    public void SetSliderValue()
    {
        if (unionCount >= LevelUpReqirementCount) sliderReqirement.value = 1;
        else
        {
            float value = ((float)unionCount / (float)LevelUpReqirementCount);
            sliderReqirement.value = value;
            Debug.Log("slider! " + value);
        }
    }

    public void EnableSlot()
    {
        isUnlock = true;
        unlock.SetActive(false);
    }

}
