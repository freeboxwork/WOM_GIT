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

    [Header("진화 UI 관련 항목")]
    public List<Sprite> evolutionGradeBadgeImages = new List<Sprite>();

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
        UtilityMethod.SetBtnEventCustomTypeByID(17, () => LotteryGameStart(10));
        UtilityMethod.SetBtnEventCustomTypeByID(18, () => LotteryGameStart(30));
        UtilityMethod.SetBtnEventCustomTypeByID(19, () => LotteryGameStart(50));


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
            var nextValueIdx = 8 + (4 * idx);
            var maxLevIdx =    9 + (4 * idx);
            UtilityMethod.SetTxtCustomTypeByID(titleIdx, names[idx]);
            UtilityMethod.SetTxtCustomTypeByID(curValueIdx, GetCurStatValue(stat));
            UtilityMethod.SetTxtCustomTypeByID(nextValueIdx, GetNextStatValue(stat));
            UtilityMethod.SetTxtCustomTypeByID(maxLevIdx,  GetStatMaxLevel(stat));
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
            case SaleStatType.trainingDamage: UtilityMethod.SetTxtsCustomTypeByIDs(new int[] { 7, 8 },values); break;
            case SaleStatType.trainingCriticalChance: UtilityMethod.SetTxtsCustomTypeByIDs( new int[] { 11, 12 },values); break;
            case SaleStatType.trainingCriticalDamage: UtilityMethod.SetTxtsCustomTypeByIDs( new int[] { 15, 16 },values); break;
            case SaleStatType.talentDamage: UtilityMethod.SetTxtsCustomTypeByIDs( new int[] { 19, 20 },values); break;
            case SaleStatType.talentCriticalChance: UtilityMethod.SetTxtsCustomTypeByIDs( new int[] { 23, 24 },values); break;
            case SaleStatType.talentCriticalDamage: UtilityMethod.SetTxtsCustomTypeByIDs( new int[] { 27, 28 },values); break;
            case SaleStatType.talentSpawnSpeed: UtilityMethod.SetTxtsCustomTypeByIDs( new int[] { 31, 32 },values); break;
            case SaleStatType.talentMoveSpeed: UtilityMethod.SetTxtsCustomTypeByIDs( new int[] { 35, 36 },values); break;
            case SaleStatType.talentGoldBonus: UtilityMethod.SetTxtsCustomTypeByIDs( new int[] { 39, 40 },values); break;
        }
    }

    #endregion


    public void EnableMenuPanel(MenuPanelType type)
    {
        for (int i = 0; i < mainPanels.Count; i++)
        {
            if (i == (int)type)
                mainPanels[i].SetActive(!mainPanels[i].activeSelf);
            else
                mainPanels[i].SetActive(false);
        }
    }
    public void AllDisableMenuPanels()
    {
        foreach(var panel in mainPanels)
            panel.SetActive(false);
    }

    void LotteryGameStart(int roundCount)
    {
        trLotteryGameSet.gameObject.SetActive(true);
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