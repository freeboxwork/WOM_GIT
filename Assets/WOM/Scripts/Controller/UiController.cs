using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UiController : MonoBehaviour
{
    public CustomTypeDataManager customTypeDataManager;
    
    [Header("Monster ���� UI �׸�")]
    public TextMeshProUGUI txtMonsterHp;
    public TextMeshProUGUI txtPhaseCount;
    public TextMeshProUGUI txtGold;
    public Button btnBossChallenge;

    [Header("�������� ���� ���� UI �׸�" )]
    public TextMeshProUGUI txtBossMonChallengeTimer;
    public Image imgBossMonTimer;
    public Image imgBossMonTimerParent;

    [Header("�̱� ���� UI �׸�")]
    public Button btnLottery10Round;
    public Button btnLottery30Round;
    public Button btnLottery50Round;
    public Transform trLotteryGameSet;


    void Start()
    {
        
    }

    /// <summary> UI ���� ���� ���� </summary>
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
        btnLottery10Round        = customTypeDataManager.GetCustomTypeData_Button(11);
        btnLottery30Round        = customTypeDataManager.GetCustomTypeData_Button(12);
        btnLottery50Round        = customTypeDataManager.GetCustomTypeData_Button(13);
        trLotteryGameSet         = customTypeDataManager.GetCustomTypeData_Transform(0);
    }


    void SetBtnEvent()
    {
        // ���� ���� ���� ��ư
        btnBossChallenge.onClick.AddListener(() => {
            EventManager.instance.RunEvent(CallBackEventType.TYPES.OnBossMonsterChallenge);
        });


        // �̱� ���� 10ȸ 
        btnLottery10Round.onClick.AddListener(()=>LotteryGameStart(10));

        // �̱� ���� 30ȸ 
        btnLottery30Round.onClick.AddListener(() => LotteryGameStart(30));

        // �̱� ���� 50ȸ 
        btnLottery50Round.onClick.AddListener(() => LotteryGameStart(50));


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
        // STAT ���� ��ư �̺�Ʈ ����
        int btnId = 14;
        foreach(EnumDefinition.SaleStatType type in Enum.GetValues(typeof(EnumDefinition.SaleStatType)))
        {
            UtilityMethod.SetBtnEventCustomTypeByID(btnId, () =>
            {
                GlobalData.instance.saleManager.AddData(new SaleStatMsgData(type));
            });
            btnId++;
        }

    }

    void LotteryGameStart(int roundCount)
    {
        trLotteryGameSet.gameObject.SetActive(true);
        GlobalData.instance.lotteryManager.LotteryStart(roundCount, () => {
            Debug.Log(roundCount + "ȸ �̱� ���� ���� �̺�Ʈ ����");
        });
    }


    // �ʱ�ȭ�� UI ������Ʈ ��Ȱ��ȭ 
    void DisableUiElements()
    {
        btnBossChallenge.gameObject.SetActive(false);
        imgBossMonTimerParent.gameObject.SetActive(false);
    }
}