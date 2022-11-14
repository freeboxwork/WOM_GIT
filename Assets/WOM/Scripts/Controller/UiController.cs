using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using static EnumDefinition;

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

        // Init Text Values
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
        trLotteryGameSet         = customTypeDataManager.GetCustomTypeData_Transform(0);
    }


    void SetBtnEvent()
    {
        // ���� ���� ���� ��ư
        btnBossChallenge.onClick.AddListener(() => {
            EventManager.instance.RunEvent(CallBackEventType.TYPES.OnBossMonsterChallenge);
        });

        // �̱� ���� 10 , 30 , 50ȸ
        UtilityMethod.SetBtnEventCustomTypeByID(17, () => LotteryGameStart(10));
        UtilityMethod.SetBtnEventCustomTypeByID(18, () => LotteryGameStart(10));
        UtilityMethod.SetBtnEventCustomTypeByID(19, () => LotteryGameStart(10));


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
        int btnId = 8;
        foreach(EnumDefinition.SaleStatType type in Enum.GetValues(typeof(EnumDefinition.SaleStatType)))
        {
            UtilityMethod.SetBtnEventCustomTypeByID(btnId, () =>
            {
                GlobalData.instance.saleManager.AddData(new SaleStatMsgData(type));
            });
            btnId++;
        }
    }


    // Set Txt Traning Pannel Text
    // ���� ���Ȱ��� ���� ���Ȱ� �ؽ�Ʈ�� ���� �Ҷ� ���
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