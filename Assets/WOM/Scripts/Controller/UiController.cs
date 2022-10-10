using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiController : MonoBehaviour
{
    public CustomTypeDataManager customTypeDataManager;
    
    [Header("BOSS 관련 UI 항")]
    public TextMeshProUGUI txtBossHp;

    void Start()
    {
        
    }

    /// <summary> 필요 UI SETTING </summary>
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
