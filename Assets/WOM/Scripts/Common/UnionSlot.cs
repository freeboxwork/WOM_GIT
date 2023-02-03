using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnionSlot : MonoBehaviour
{
    public EnumDefinition.UnionGradeType unionGradeType;
    public EnumDefinition.UnionEquipType unionEquipType;
    //public int unionId;
    //public int curLevel;   // 현재 레벨
    //public int unionCount; // 유니온 보수 수
    //public int LevelUpReqirementCount;         // 레벨업에 필요한 유니온 수
    public TextMeshProUGUI txtLevel;           // 현재 레벨
    public TextMeshProUGUI txtEquipState;      // 장착 여부
    public TextMeshProUGUI txtReqirementCount; // 레벨업에 필요한 유니온 수
    public Slider sliderReqirement;
    public Image imgUnionFace;
    public Button btn;
    public GameObject unlock;
    //public bool isUnlock = false;

    public UnionInGameData inGameData;
    public UnionData unionData;

 

    void Start()
    {
        
    }

   void SetBtnEvent()
    {

    }

   
    public void AddUnion(int count)
    {
        inGameData.unionCount += count;
    }
        
    public void PayUnion(int count)
    {
        inGameData.unionCount -= count;
    }

    public void LevelUp()
    {
        ++inGameData.level;
    }



    public void RelodUISet()
    {
        SetUITxtUnionCount();
        SetUITxtLevel();
        SetSliderValue();
    }

    public void SetUIImageUnion(Sprite unionFace)
    {
        imgUnionFace.sprite = unionFace;    
    }


    public void SetUITxtUnionCount()
    {
        var text = $"{inGameData.unionCount}/{inGameData.LevelUpReqirementCount}";
        txtReqirementCount.text = text;
    }

    public void SetUITxtLevel()
    {
        txtLevel.text = "Lv" + inGameData.level.ToString();
    }

    public void SetUITxtUnionEquipState()
    {
        var equipTxt = unionEquipType == EnumDefinition.UnionEquipType.Equipped ? "장착중" : "";
        txtEquipState.text = equipTxt;
    }

    public void SetSliderValue()
    {
        if (inGameData.unionCount >= inGameData.LevelUpReqirementCount) sliderReqirement.value = 1;
        else
        {
            float value = ((float)inGameData.unionCount / (float)inGameData.LevelUpReqirementCount);
            sliderReqirement.value = value;
            //Debug.Log("slider! " + value);
        }
    }

    public void EnableSlot()
    {
        inGameData.isUnlock = true;
        btn.enabled = true;
        unlock.SetActive(false);
    }

}
