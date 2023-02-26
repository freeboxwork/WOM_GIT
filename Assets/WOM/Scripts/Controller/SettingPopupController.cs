using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO : AUDIO MANAGER 분리...
public class SettingPopupController : MonoBehaviour
{

    public AudioSource playerBgm;
    public AudioSource playerSfxInGame;   // 몬스터 공격과 같은 효과음
    public AudioSource playerSfxUiPlayer; // UI 에서 사용되는 효과음


    public Button btnBgmOnOff;
    public Button btnSfxOnOff;
    public Button btnReview;

    public List<AudioClip> bgmList = new List<AudioClip>();
    public List<AudioClip> sfxList = new List<AudioClip>();

    bool bgmOn = true;
    bool sfxInGame = true;
    bool sfxUI = true;

    void Start()
    {
        SetBtnEvents();
    }

    public IEnumerator Init()
    {

        // BGM PLAY
        PlayBgm(EnumDefinition.BGM_TYPE.BGM_01);
        yield return null;
    }
    
    void SetBtnEvents()
    {
        btnBgmOnOff.onClick.AddListener(() => {

            BGM_OnOff(bgmOn = !bgmOn);
        });


    }

    public void BGM_OnOff(bool bgmOn)
    {
        if(bgmOn)
            playerBgm.Play();
        else
            playerBgm.Stop();
    }
    
    public void SFX_InGameOnOff(bool sfxOn)
    {
        var volume = sfxOn ? 1 : 0;
        playerSfxInGame.volume = volume;
    }

    public void SFX_UI_OnOff(bool sfxOn)
    {
        var volume = sfxOn ? 1 : 0;
        playerSfxUiPlayer.volume = volume;
    }

    public void PlayBgm(EnumDefinition.BGM_TYPE bgmType)
    {
        playerBgm.clip = bgmList[(int)bgmType];
        playerBgm.Play();
    }

    public void PlaySfxInGame(EnumDefinition.SFX_TYPE sfxType)
    {
        playerSfxInGame.clip = sfxList[(int)sfxType];
        playerSfxInGame.Play();
    }

    public void PlaySfxUI(EnumDefinition.SFX_TYPE sfxType)
    {
        playerSfxUiPlayer.clip = sfxList[(int)sfxType];
        playerSfxUiPlayer.Play();
    }

}


