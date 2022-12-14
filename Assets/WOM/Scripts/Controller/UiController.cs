using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public Button btnLottery10Round;
    public Button btnLottery30Round;
    public Button btnLottery50Round;
    public Transform trLotteryGameSet;


    void Start()
    {
        
    }

    /// <summary> UI 관련 정보 세팅 </summary>
    public IEnumerator Init()
    {
        // set ui data
        SetUiData();

        // set btn event
        SetBtnEvent();

        yield return null;

        // Disable Ui Elements
        DisableUiElements();

   
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
        btnLottery10Round        = customTypeDataManager.GetCustomTypeData_Button(10);
        btnLottery30Round        = customTypeDataManager.GetCustomTypeData_Button(11);
        btnLottery50Round        = customTypeDataManager.GetCustomTypeData_Button(12);
        trLotteryGameSet         = customTypeDataManager.GetCustomTypeData_Transform(0);
    }


    void SetBtnEvent()
    {
        // 보스 몬스터 도전 버튼
        btnBossChallenge.onClick.AddListener(() => {
            EventManager.instance.RunEvent(CallBackEventType.TYPES.OnBossMonsterChallenge);
        });


        // 뽑기 게임 10회 
        btnLottery10Round.onClick.AddListener(()=>LotteryGameStart(10));

        // 뽑기 게임 30회 
        btnLottery30Round.onClick.AddListener(() => LotteryGameStart(30));

        // 뽑기 게임 50회 
        btnLottery50Round.onClick.AddListener(() => LotteryGameStart(50));

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
