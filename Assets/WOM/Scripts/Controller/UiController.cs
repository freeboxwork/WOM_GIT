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

    [Header("뽑기 관련 UI 항목")]
    public Transform trLotteryGameSet;

    List<GameObject> mainPanels = new List<GameObject>();
    public List<MainBtnSlot> mainButtons = new List<MainBtnSlot>();

    [Header("진화 UI 관련 항목")]
    public List<Sprite> evolutionGradeBadgeImages = new List<Sprite>();
    public List<EvolutionSlot> evolutionSlots = new List<EvolutionSlot>();
    

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

        // 진화 판넬 UI 초기 값 세팅
        SetUI_Pannel_Evolution(GlobalData.instance.player.evalutionLeveldx);

        // 진화 슬롯 UI 초기 세팅 
        SetUI_EvolutionSlots(GlobalData.instance.player.evalutionLeveldx);

        // 골드 , 뼈조각 , 보석 UI 초기 세팅


        // 진화 주사위 굴리기 버튼 상태 세팅
        EanbleBtnEvolutionRollDice();

        // 재화 UI 세팅
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
        trLotteryGameSet         = customTypeDataManager.GetCustomTypeData_Transform(0);
    }


    void SetBtnEvent()
    {
        // 보스 몬스터 도전 버튼
        btnBossChallenge.onClick.AddListener(() => {
            EventManager.instance.RunEvent(CallBackEventType.TYPES.OnBossMonsterChallenge);
        });

        // 뽑기 게임 10 , 30 , 50회
        UtilityMethod.SetBtnEventCustomTypeByID(17, () => LotteryGameStart(5));
        UtilityMethod.SetBtnEventCustomTypeByID(18, () => LotteryGameStart(15));
        UtilityMethod.SetBtnEventCustomTypeByID(19, () => LotteryGameStart(33));


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
        int btnId = 8;
        foreach(EnumDefinition.SaleStatType type in Enum.GetValues(typeof(EnumDefinition.SaleStatType)))
        {
            UtilityMethod.SetBtnEventCustomTypeByID(btnId, () =>
            {
                GlobalData.instance.saleManager.AddData(new SaleStatMsgData(type));
            });
            btnId++;
        }


        // 메인 판넬 열기
        foreach (MenuPanelType type in Enum.GetValues(typeof(MenuPanelType)))
        {
            UtilityMethod.SetBtnEventCustomTypeByID(((int)type + 1), () => { EnableMenuPanel(type); });
        }

        /* 진화전 */

        // 진화전 버튼
        UtilityMethod.SetBtnEventCustomTypeByID(20, () =>
        {
            EventManager.instance.RunEvent(CallBackEventType.TYPES.OnEvolutionMonsterChallenge);
            
            // 메뉴 판넬 숨김
            AllDisableMenuPanels();
            // 진화전 버튼 선택 효과 숨김
            EnableMenuPanel(MenuPanelType.evolution);

            // 진화전 버튼 비활성화
            EnableBtnEvolutionMonsterChange(false);

        });

        // 진화 업그레이드 이펙트 확인 버튼
        UtilityMethod.SetBtnEventCustomTypeByID(23, () =>
        {
            // 기존 UI Canvas 활성화
            UtilityMethod.GetCustomTypeGMById(6).SetActive(true);
            // 진화 업그레이트 이펙트 비활성화
            GlobalData.instance.gradeAnimCont.gameObject.SetActive(false);
        });

        // 진화 주사위 뽑기 버튼
        UtilityMethod.SetBtnEventCustomTypeByID(22, () => {
            GlobalData.instance.evolutionDiceLotteryManager.RollEvolutionDice();
        });
    }


    public void SetUI_Pannel_Evolution(int dataId)
    {
        var data = GlobalData.instance.dataManager.GetRewaedEvolutionGradeDataByID(dataId);

        // set badge image
        UtilityMethod.SetImageSpriteCustomTypeByID(21, evolutionGradeBadgeImages[data.id]);

        // set txt evolution grade 
        UtilityMethod.SetTxtCustomTypeByID(61, data.evolutionGradeType);

        // set txt damage rate
        UtilityMethod.SetTxtCustomTypeByID(62, $"{data.damageRate}" );

        // set txt slot count
        UtilityMethod.SetTxtCustomTypeByID(63, $"{data.slotCount}");
    }

    public void SetUI_EvolutionSlots(int dataId)
    {
        var data = GlobalData.instance.dataManager.GetRewaedEvolutionGradeDataByID(dataId);

        // slot count 만큼 슬롯 열어줌
        for (int i = 0; i < data.slotCount; i++)
        {
            evolutionSlots[i].UnLockSlot();
        }
    }

    public void SetEvolutuinSlotName(EvolutionDiceStatType type, int slotId)
    {
        var nameData = GlobalData.instance.dataManager.GetConvertTextDataByEvolutionDiceStatType(type);
        //evolutionSlots[i]


    }

    
    
    public  void EanbleBtnEvolutionRollDice()
    {
        // 주사위 하나라도 오픈 되어 있으면 주사위 굴리기 버튼 활성화
        var enableValue = evolutionSlots.Any(a=> a.isUnlock == true);
        
        // 22 : 주사위 굴리기 버튼
        UtilityMethod.GetCustomTypeBtnByID(22).interactable = enableValue;
        var diceImageColor = enableValue ? Color.white : Color.gray;
        UtilityMethod.GetCustomTypeImageById(22).color = diceImageColor;

        // 주사위 사용 개수 
        var count = UtilityMethod.GetEvolutionDiceUsingCount();
        UtilityMethod.SetTxtCustomTypeByID(64, count.ToString());
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

    public void EnableBtnEvolutionMonsterChange(bool value)
    {
        UtilityMethod.GetCustomTypeBtnByID(20).interactable = value;
    }

    void LotteryGameStart(int roundCount)
    {
        //trLotteryGameSet.gameObject.SetActive(true);
        GlobalData.instance.lotteryManager.LotteryStart(roundCount, () => {
            Debug.Log(roundCount + "회 뽑기 게임 종료 이벤트 실행");
        });
    }


    // 초기화시 UI 오브젝트 비활성화 
    void DisableUiElements()
    {
        btnBossChallenge.gameObject.SetActive(false);
        imgBossMonTimerParent.gameObject.SetActive(false);
    }
}