using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using static EnumDefinition;
using System.Linq;
using Unity.VisualScripting;

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

        yield return null;

        // Disable Ui Elements
        DisableUiElements();

        // Init Text Values

        // 훈련 UI 초기 값 세팅
        SetStatInfoTxt();

        // 진화 판넬 UI 초기 값 세팅
        SetUI_Pannel_Evolution(GlobalData.instance.player.evalutionLeveldx);

        // 진화 슬롯 UI 초기 세팅 
        SetUI_EvolutionSlots(GlobalData.instance.player.evalutionLeveldx);

        // 진화 주사위 굴리기 버튼 상태 세팅
        EanbleBtnEvolutionRollDice();

        // 현재 남은 진화 주사위 개수 UI 적용
        UtilityMethod.SetTxtCustomTypeByID(65,GlobalData.instance.player.diceCount.ToString());

        
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

    public void SetTxtGold(int value)
    {
        txtGold.text = value.ToString();
    }

    public void SetTxtBone(int value)
    {
        UtilityMethod.SetTxtCustomTypeByID(60, value.ToString());
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





    #region 훈련 스탯 - 구매 가능한 스탯 UI 세팅 
    // --- [ TRANING UI ] ---

    void SetStatInfoTxt() // title , maximum level
    {
        // SET TITLE , MAXIMUM LEVEL
        string[] names = { "Traning Damage", "Traning Critical Chance", "Traning Critical Damage", "Talent Damage", "Talent Critical Chance", "Talent Critical Damage" , "Talent Spawn Speed" , "Talent Move Speed", "Talent Gold Bonus" };
        int idx = 0;
        foreach(SaleStatType stat in Enum.GetValues(typeof(SaleStatType)))
        {
            var titleIdx =     6 + (4 * idx);
            var curValueIdx =  7 + (4 * idx);
            //var nextValueIdx = 8 + (4 * idx);
            //var maxLevIdx =    9 + (4 * idx);
            //UtilityMethod.SetTxtCustomTypeByID(titleIdx, names[idx]);
            UtilityMethod.SetTxtCustomTypeByID(curValueIdx, GetCurStatValue(stat));
            //UtilityMethod.SetTxtCustomTypeByID(nextValueIdx, GetNextStatValue(stat));
            //UtilityMethod.SetTxtCustomTypeByID(maxLevIdx,  GetStatMaxLevel(stat));
            idx++;
        }
    }

    string GetCurStatValue(SaleStatType statType)
    {
        var curLevel = GlobalData.instance.player.curStatData.statLevelDatas[(int)statType];
        return GlobalData.instance.dataManager.GetSaleStatDataByTypeId(statType, curLevel).value.ToString();
    }

    string GetNextStatValue(SaleStatType statType)
    {

        var data = GlobalData.instance.dataManager.GetSaleStatDataByType(statType);
        var curLevel = GlobalData.instance.player.curStatData.statLevelDatas[(int)statType];
        // TODO: 다음 레벨 존재할때 어떤 값으로 넣어야 하는지 체크 필요함
        if (data.data.Last().level > curLevel)
            return GlobalData.instance.dataManager.GetSaleStatDataByTypeId(statType, curLevel+1).value.ToString();
        else
            return GlobalData.instance.dataManager.GetSaleStatDataByTypeId(statType, curLevel).value.ToString();
    }

    string GetStatMaxLevel(SaleStatType statType)
    {
        return "Max : " +  GlobalData.instance.dataManager.GetSaleStatDataByType(statType).data.Last().level.ToString();
    }
    

    // 현재 스탯값과 다음 스탯값 텍스트를 세팅 할때 사용
    public void SetTxtTraningValues(SaleStatType statType, float[] values)
    {
        switch (statType)
        {
            case SaleStatType.trainingDamage: UtilityMethod.SetTxtsCustomTypeByIDs(new int[] { 7 },values); break;
            case SaleStatType.trainingCriticalChance: UtilityMethod.SetTxtsCustomTypeByIDs( new int[] { 11 },values); break;
            case SaleStatType.trainingCriticalDamage: UtilityMethod.SetTxtsCustomTypeByIDs( new int[] { 15 },values); break;
            case SaleStatType.talentDamage: UtilityMethod.SetTxtsCustomTypeByIDs( new int[] { 19 },values); break;
            case SaleStatType.talentCriticalChance: UtilityMethod.SetTxtsCustomTypeByIDs( new int[] { 23 },values); break;
            case SaleStatType.talentCriticalDamage: UtilityMethod.SetTxtsCustomTypeByIDs( new int[] { 27 },values); break;
            case SaleStatType.talentSpawnSpeed: UtilityMethod.SetTxtsCustomTypeByIDs( new int[] { 31 },values); break;
            case SaleStatType.talentMoveSpeed: UtilityMethod.SetTxtsCustomTypeByIDs( new int[] { 35 },values); break;
            case SaleStatType.talentGoldBonus: UtilityMethod.SetTxtsCustomTypeByIDs( new int[] { 39 },values); break;
        }
    }

    #endregion


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