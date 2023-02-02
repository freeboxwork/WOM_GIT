using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using static EnumDefinition;
using System.Linq;

public class UiController : MonoBehaviour
{
    public CustomTypeDataManager customTypeDataManager;
    
    [Header("Monster 관련 UI 항목")]
    public TextMeshProUGUI txtMonsterHp;
    public TextMeshProUGUI txtPhaseCount;
    public TextMeshProUGUI txtGold;
    public Button btnBossChallenge;

    [Header("보스몬스터 도전 관련 UI 항목" )]
    public TextMeshProUGUI txtBossMonChallengeTimer;
    public Image imgBossMonTimer;
    public Image imgBossMonTimerParent;

    //[Header("뽑기 관련 UI 항목")]
    //public Transform trLotteryGameSet;

    List<GameObject> mainPanels = new List<GameObject>();
    public List<MainBtnSlot> mainButtons = new List<MainBtnSlot>();

    //[Header("진화 UI 관련 항목")]
    //public List<Sprite> evolutionGradeBadgeImages = new List<Sprite>();
    //public List<EvolutionSlot> evolutionSlots = new List<EvolutionSlot>();
    

    void Start()
    {
        
    }

    void SetMainPanels()
    {
        foreach (MenuPanelType type in Enum.GetValues(typeof(MenuPanelType)))
        {
            var panel = UtilityMethod.GetCustomTypeGMById((int)type);
            mainPanels.Add(panel);
        }
    }

    /// <summary> UI 관련 정보 세팅 </summary>
    public IEnumerator Init()
    {
        // set panel gameObjcts
        SetMainPanels();

        // set ui data
        SetUiData();

        // set btn event
        SetBtnEvent();

        // Disable Ui Elements
        DisableUiElements();

       

        // 재화 UI 세팅 ( 골드 , 뼈조각 , 보석 UI 초기 세팅 )
        SetGoodsUI();



        yield return null;

    }


    // SET PLAYER UI
    void SetGoodsUI()
    {
        //TODO: 저장된 데이터에서 불어와야 함
        SetTxtGold(0);
        SetTxtBone(0);
        SetTxtGem(0);
        SetTxtDice(0);  // 현재 남은 진화 주사위 개수 UI 적용
    }



    /* SET MONSTER UI */
    public void SetTxtMonsterHp(float value)
    {
        txtMonsterHp.text = value.ToString();
    }
      
    public void SetTxtPhaseCount(int value)
    {
        txtPhaseCount.text = value.ToString();
    }

    public void SetSliderPhaseValue()
    {

    }

    public void SetTxtGold(int value)
    {
        var changeValue = UtilityMethod.ChangeSymbolNumber(value);
        txtGold.text = changeValue.ToString();
    }

    public void SetTxtBone(int value)
    {
        var changeValue = UtilityMethod.ChangeSymbolNumber(value);
        UtilityMethod.SetTxtCustomTypeByID(60, changeValue.ToString());
    }

    public void SetTxtGem(int value)
    {
        var changeValue = UtilityMethod.ChangeSymbolNumber(value);
        UtilityMethod.SetTxtCustomTypeByID(79, changeValue.ToString());
    }

    public void SetTxtDice(int value)
    {
        var changeValue = UtilityMethod.ChangeSymbolNumber(value);
        UtilityMethod.SetTxtCustomTypeByID(65, changeValue);
    }
         

    public void SetTxtBossChallengeTimer(int value)
    {
        txtBossMonChallengeTimer.text = value.ToString();
    } 

    public void SetImgTimerFilledRaidal(float value)
    {
        imgBossMonTimer.fillAmount = value; 
    }
        

    void SetUiData()
    {
        txtMonsterHp             = customTypeDataManager.GetCustomTypeData_Text(1);
        txtPhaseCount            = customTypeDataManager.GetCustomTypeData_Text(2);
        txtGold                  = customTypeDataManager.GetCustomTypeData_Text(4);
        btnBossChallenge         = customTypeDataManager.GetCustomTypeData_Button(0);
        txtBossMonChallengeTimer = customTypeDataManager.GetCustomTypeData_Text(3);
        imgBossMonTimer          = customTypeDataManager.GetCustomTypeData_Image(0);
        imgBossMonTimerParent    = customTypeDataManager.GetCustomTypeData_Image(1);
        //trLotteryGameSet         = customTypeDataManager.GetCustomTypeData_Transform(0);
    }


    void SetBtnEvent()
    {
        // 보스 몬스터 도전 버튼
        btnBossChallenge.onClick.AddListener(() => {
            EventManager.instance.RunEvent(CallBackEventType.TYPES.OnBossMonsterChallenge);
        });

     


        #region btn type list
        /*
        trainingDamage,
        trainingCriticalChance,
        trainingCriticalDamage,
        talentDamage,
        talentCriticalChance,
        talentCriticalDamage,
        talentMoveSpeed,
        talentSpawnSpeed,
        talentGoldBonus
         */
        #endregion
        // STAT 구매 버튼 이벤트 세팅
        //int btnId = 8;
        //foreach(EnumDefinition.SaleStatType type in Enum.GetValues(typeof(EnumDefinition.SaleStatType)))
        //{
        //    UtilityMethod.SetBtnEventCustomTypeByID(btnId, () =>
        //    {
        //        GlobalData.instance.saleManager.AddData(new SaleStatMsgData(type));
        //    });
        //    btnId++;
        //}


        // 메인 판넬 열기
        foreach (MenuPanelType type in Enum.GetValues(typeof(MenuPanelType)))
        {
            UtilityMethod.SetBtnEventCustomTypeByID(((int)type + 1), () => { EnableMenuPanel(type); });
        }

     
    }




    public void EnableMenuPanel(MenuPanelType type)
    {
        for (int i = 0; i < mainPanels.Count; i++)
        {
            if (i == (int)type)
            {
                var enableValue = !mainPanels[i].activeSelf;
                mainPanels[i].SetActive(enableValue);
                mainButtons[i].Select(enableValue);
            }
            else
            {
                mainButtons[i].Select(false);
                mainPanels[i].SetActive(false);
            }

        }
    }
    public void AllDisableMenuPanels()
    {
        foreach(var panel in mainPanels)
            panel.SetActive(false);
    }




    // 초기화시 UI 오브젝트 비활성화 
    void DisableUiElements()
    {
        btnBossChallenge.gameObject.SetActive(false);
        imgBossMonTimerParent.gameObject.SetActive(false);
        UtilityMethod.GetCustomTypeBtnByID(30).gameObject.SetActive(false);
    }
}