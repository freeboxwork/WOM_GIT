using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiController : MonoBehaviour
{
    public CustomTypeDataManager customTypeDataManager;
    
    [Header("BOSS ���� UI ��")]
    public TextMeshProUGUI txtBossHp;

    void Start()
    {
        
    }

    /// <summary> �ʿ� UI SETTING </summary>
    public IEnumerator SetUiDatas()
    {
        txtBossHp = customTypeDataManager.GetCustomTypeData_Text(1).components.text;
        yield return null;
    }


    /* SET MONSTER UI */
    public void SetTxtMonsterHp(float value)
    {
        txtBossHp.text = value.ToString();
    }
      

}
