using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using static EnumDefinition;
using System.Linq;
using ProjectGraphics;
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

    //[Header("뽑기 관련 UI 항목")]
    //public Transform trLotteryGameSet;

    List<GameObject> mainPanels = new List<GameObject>();
    public List<MainBtnSlot> mainButtons = new List<MainBtnSlot>();

    //[Header("진화 UI 관련 항목")]
    //public List<Sprite> evolutionGradeBadgeImages = new List<Sprite>();
    //public List<EvolutionSlot> evolutionSlots = new List<EvolutionSlot>();

    public float menuPannelScrollView_posY_traning;
    public float menuPannelScrollView_posY_union;
    public float menuPannelScrollView_posY_dna;
    public float menuPannelScrollView_posY_shop;

    public FloatingTextValues floatingTextGold;
    public FloatingTextValues floatingTextBone;
    public FloatingTextValues floatingTextGem;

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

        // 메인 판넬 스크롤뷰 시작 위치 가져오기 ( 리셋을 위해 )
        GetMainPannelsScrollViewPosY();

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
        var countValue = GlobalData.instance.player.pahseCountOriginalValue - value;
        txtPhaseCount.text = countValue.ToString();

        
    }

    public void SetSliderMonsterHp(float value)
    {
        var sliderValue = value / GlobalData.instance.player.currentMonsterHp ;
        UtilityMethod.GetCustomTypeImageById(41).fillAmount = sliderValue;
        
        // 0.1초 뒤에 실행
        StartCoroutine(SetSliderBgWithDelay(sliderValue, 0.15f));
    }

    public void SetSliderDungeonMonsterHP(float value)
    {
        var sliderValue = value / GlobalData.instance.monsterManager.GetMonsterDungeon().curMonsterHP;
        UtilityMethod.GetCustomTypeImageById(41).fillAmount = sliderValue;

        // 0.1초 뒤에 실행
        StartCoroutine(SetSliderBgWithDelay(sliderValue, 0.15f));
    }

    public void SetSliderBg(float value)
    {
        UtilityMethod.GetCustomTypeImageById(44).fillAmount = value;
    }

    private IEnumerator SetSliderBgWithDelay(float value, float delay)
    {
        yield return new WaitForSeconds(delay);
        SetSliderBg(value);
    }


    public void SetSliderPhaseValue(float value)
    {
        var calcValue = (float)value / GlobalData.instance.player.pahseCountOriginalValue;
        var sliderValue = 1 - calcValue;
        UtilityMethod.GetCustomTypeImageById(42).fillAmount = sliderValue;

        EnableGlodMonsterIconOutlineEffect(sliderValue >= 1);
    }

    public void SetTxtGold(int value)
    {
        var changeValue = UtilityMethod.ChangeSymbolNumber(value);

        // floting text effect
        floatingTextGold.gameObject.SetActive(true);
        floatingTextGold.SetText(changeValue, FloatingTextValues.ValueType.Gold);

        txtGold.text = changeValue.ToString();
    }

    public void SetTxtBone(int value)
    {
        var changeValue = UtilityMethod.ChangeSymbolNumber(value);

        // floting text effect
        floatingTextBone.gameObject.SetActive(true);
        floatingTextBone.SetText(changeValue, FloatingTextValues.ValueType.Bone);

        UtilityMethod.SetTxtCustomTypeByID(60, changeValue.ToString());
    }

    public void SetTxtGem(int value)
    {
        var changeValue = UtilityMethod.ChangeSymbolNumber(value);
        
        // floting text effect
        floatingTextGem.gameObject.SetActive(true);
        floatingTextGem.SetText(changeValue, FloatingTextValues.ValueType.jewel);

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
        var sliderValue = 1 - value;
        imgBossMonTimer.fillAmount = sliderValue; 
    }
        
    public void EnableGlodMonsterIconOutlineEffect( bool value)
    {
        UtilityMethod.GetCustomTypeImageById(43).enabled = value;
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

        // 던전 몬스터 - 골드 도전 버튼
        UtilityMethod.SetBtnEventCustomTypeByID(45, () => {
            EventManager.instance.RunEvent(CallBackEventType.TYPES.OnDungeonMonsterChallenge, EnumDefinition.MonsterType.dungeonGold);
        });
        UtilityMethod.SetBtnEventCustomTypeByID(46, () => {
            EventManager.instance.RunEvent(CallBackEventType.TYPES.OnDungeonMonsterChallenge, EnumDefinition.MonsterType.dungeonDice);
        });
        UtilityMethod.SetBtnEventCustomTypeByID(47, () => {
            EventManager.instance.RunEvent(CallBackEventType.TYPES.OnDungeonMonsterChallenge, EnumDefinition.MonsterType.dungeonBone);
        });
        UtilityMethod.SetBtnEventCustomTypeByID(48, () => {
            EventManager.instance.RunEvent(CallBackEventType.TYPES.OnDungeonMonsterChallenge, EnumDefinition.MonsterType.dungeonCoal);
        });


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
                ResetMainPannelScrollViewPosY(type);
            }
            else
            {
                mainButtons[i].Select(false);
                mainPanels[i].SetActive(false);
            }
        }
    }

    void ResetMainPannelScrollViewPosY(MenuPanelType type)
    {
        switch(type)
        {
            case MenuPanelType.training:
                SetMenuPannelScrollView_Pos(0, menuPannelScrollView_posY_traning);
                break;
            case MenuPanelType.union:
                SetMenuPannelScrollView_Pos(1, menuPannelScrollView_posY_union);
                break;
            case MenuPanelType.dna:
                SetMenuPannelScrollView_Pos(2, menuPannelScrollView_posY_dna);
                break;
            case MenuPanelType.shop:
                SetMenuPannelScrollView_Pos(3, menuPannelScrollView_posY_shop);
                break;
        }
    }

    void SetMenuPannelScrollView_Pos(int id, float changePosY)
    {
        var curPos = UtilityMethod.GetCustomTypeTrById(id).localPosition;
        var changePos = new Vector3(curPos.x, changePosY, curPos.z);
        UtilityMethod.GetCustomTypeTrById(id).localPosition = changePos;
    }

    void GetMainPannelsScrollViewPosY()
    {
        menuPannelScrollView_posY_traning = UtilityMethod.GetCustomTypeTrById(0).localPosition.y;
        menuPannelScrollView_posY_union = UtilityMethod.GetCustomTypeTrById(1).localPosition.y;
        menuPannelScrollView_posY_dna = UtilityMethod.GetCustomTypeTrById(2).localPosition.y;
        menuPannelScrollView_posY_shop = UtilityMethod.GetCustomTypeTrById(3).localPosition.y;
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

    public void SetEnablePhaseCountUI(bool value)
    {
        UtilityMethod.GetCustomTypeGMById(7).SetActive(value);
    }
}