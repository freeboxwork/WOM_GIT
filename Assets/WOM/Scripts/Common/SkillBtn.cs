using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillBtn : MonoBehaviour
{

    public Image imgSkillBack;
    public Image imgSkillFront;
    public TextMeshProUGUI txtTime;
    public TextMeshProUGUI txtTimeAnim;
    public Button btnSkill;

    public Color colorDeem;
    public Color colorWhite;

    public AnimData animDataUsingSkill;
    public AnimData animDataReloadSkill;
    public AnimationController animCont;
    
    public EnumDefinition.SkillType skillType;

    /* PROCESS
    
    1 : BACK COLOR DEEM
    2 : FRONT FILL ANIM ( 0~1 ) usin skill
    3 : FRONT COLOR DEEM
    4 : BACK COLOR WHITE
    5 : FRONT FILL ANIM ( 0~1 ) reload skill
    6 : BACK COLOR DEEM
    7 : FRONT COLOR WHIRE

    */

    public bool skillReady = false;

    void Start()
    {
        SetBtnEvent();
    }

    void SetBtnEvent()
    {
        btnSkill.onClick.AddListener(() => { UsingSkill(); });

    }

    public void UsingSkill()
    {
        StartCoroutine(UsingKill_Cor());
    }

   
    public bool skillAddValue = false;

    IEnumerator UsingKill_Cor()
    {
        var data = GlobalData.instance.skillManager.GetSkillInGameDataByType(skillType);
        animDataUsingSkill.animDuration = data.duaration;
        animDataReloadSkill.animDuration = data.coolTime;
        if (skillReady == true)
        {
            btnSkill.enabled = false;
            skillReady = false;

            imgSkillBack.color = colorWhite; 
            imgSkillFront.color = colorDeem;


           
            animCont.animData = animDataUsingSkill;
            
            //TODO : 스킬 추가 스탯 사용 이벤트 추가
            skillAddValue = true;
            txtTimeAnim.enabled = true;
            StartCoroutine(animCont.UI_TextAnim(txtTimeAnim, data.duaration,0));
            yield return StartCoroutine(animCont.UI_ImageFillAmountAnim(imgSkillFront, 0, 1));
            txtTimeAnim.enabled = false;
            skillAddValue = false;

            imgSkillBack.color = colorDeem; 
            imgSkillFront.color = colorWhite;
            animCont.animData = animDataReloadSkill;

            yield return StartCoroutine(animCont.UI_ImageFillAmountAnim(imgSkillFront, 0, 1));

            btnSkill.enabled = true;
            skillReady = true;

        }
        yield return null;
    }

    


    
}
