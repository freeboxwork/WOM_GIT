using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;


public class SettingPopupController : MonoBehaviour
{

    public Button btnBgmOnOff;
    public Button btnSfxOnOff;
    public Button btnReview;
    public Button btnSetting;
    public Button btnClose;


    public GameObject popupSetting;
    public TextMeshProUGUI txtBGM_OnOff;
    public TextMeshProUGUI txtSFX_OnOff;


    void Start()
    {
        SetBtnEvents();
    }
    

    void SetBtnEvents()
    {
        btnBgmOnOff.onClick.AddListener(() => {
            GlobalData.instance.soundManager.BGM_OnOff();

            var txtValue = GlobalData.instance.soundManager.bgmOn ? "����� OFF" : "����� ON";
            txtBGM_OnOff.text = txtValue;
        });

        btnSfxOnOff.onClick.AddListener(() => {
            GlobalData.instance.soundManager.SFX_OnOff();

            var txtValue = GlobalData.instance.soundManager.sfxOn ? "ȿ���� OFF" : "ȿ���� ON";
            txtSFX_OnOff.text = txtValue;
        });

        btnReview.onClick.AddListener(() => {
            Debug.Log("review...");
        });

        btnClose.onClick.AddListener(() => {
            popupSetting.SetActive(false);
        });

        btnSetting.onClick.AddListener(() =>
        {
            popupSetting.SetActive(true);
        });

    }

  

}


