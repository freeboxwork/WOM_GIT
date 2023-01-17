using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnionInfoPopupController : MonoBehaviour
{
    public GameObject popup;
    public Image imgUnionFace;
    public TextMeshProUGUI txtUinonName;
    public TextMeshProUGUI txtUinonGrade;
    public TextMeshProUGUI txtDamage;
    public TextMeshProUGUI txtSpawnTime;
    public TextMeshProUGUI txtMoveSpeed;
    public TextMeshProUGUI txtPassiveDamage;
    public TextMeshProUGUI txtReqirementCount;
    public Slider slider;
    public Button btnEquip;
    public Button btnLevelUp;
    public Button btnClose;
    UnionInGameData unionInGameData;

    void Start()
    {
        SetBtnEvent();
    }

    void SetBtnEvent()
    {
        btnEquip.onClick.AddListener(() =>
        {

        });

        btnLevelUp.onClick.AddListener(() =>
        {

        });

        btnClose.onClick.AddListener(() =>
        {
            popup.SetActive(false); 
        });
    }
    public void EnablePopup(UnionSlot slot, UnionData data,  UnionInGameData inGameData)
    {
        // SET UI
        SetImgFace(slot.imgUnionFace.sprite);
        SetTxtUnionName(data.name);
        SetTxtUinonGrade(data.gradeName);

        unionInGameData = inGameData;

        // SET STAT UI
        SetTxtDamage(inGameData.damage.ToString());
        SetTxtSpawnTime(inGameData.spawnTime.ToString());
        SetTxtMoveSpeed(inGameData.moveSpeed.ToString());
        SetTxtPassiveDamage(inGameData.passiveDamage.ToString());
        SetSlider(slot.sliderReqirement.value);
        SetTxtReqirementCount(slot.txtReqirementCount.text);

        popup.SetActive(true);
    }
  
    public void SetTxtUnionName(string value)
    {
        txtUinonName.text = value;
    }

    public void SetTxtUinonGrade(string value)
    {
        txtUinonGrade.text = value;
    }

    public void SetTxtDamage(string value)
    {
        txtDamage.text = value; 
    }

    public void SetTxtSpawnTime(string value)
    {
        txtSpawnTime.text = value;  
    }

    public void SetTxtMoveSpeed(string value)
    {
        txtMoveSpeed.text = value;
    }

    public void SetTxtPassiveDamage(string value)
    {
        txtPassiveDamage.text = value+"%";
    }

    public void SetTxtReqirementCount(string value)
    {
        txtReqirementCount.text = value;
    }

    public void SetImgFace(Sprite sprite)
    {
        imgUnionFace.sprite = sprite;
    }
   
    public void SetSlider(float value)
    {
        slider.value = value;
    }


}
