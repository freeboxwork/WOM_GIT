using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiController : MonoBehaviour
{
    public CustomTypeDataManager customTypeDataManager;
    
    [Header("Monster ���� UI �׸�")]
    public TextMeshProUGUI txtMonsterHp;
    public TextMeshProUGUI txtPhaseCount;
    public Button btnBossChallenge;
   


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

    void SetUiData()
    {
        txtMonsterHp = customTypeDataManager.GetCustomTypeData_Text(1).components.text;
        txtPhaseCount = customTypeDataManager.GetCustomTypeData_Text(2).components.text;
        btnBossChallenge = customTypeDataManager.GetCustomTypeData_Button(0).components.button;
    }


    void SetBtnEvent()
    {
        btnBossChallenge.onClick.AddListener(() => {
            EventManager.instance.RunEvent(CallBackEventType.TYPES.OnBossMonsterChallenge);
        });
    }

    // �ʱ�ȭ�� UI ������Ʈ ��Ȱ��ȭ 
    void DisableUiElements()
    {
        btnBossChallenge.gameObject.SetActive(false);
    }
}
