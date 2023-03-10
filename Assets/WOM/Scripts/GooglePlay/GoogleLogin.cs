using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SceneManagement;

public class GoogleLogin : MonoBehaviour
{
    public Button btnLogIn;
    public Button btnLogOut;

    void Start()
    {
        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        SetBtnEvents();
    }

    void SetBtnEvents()
    {
        btnLogIn.onClick.AddListener(LogIn);
        btnLogOut.onClick.AddListener(LogOut);
    }
    

    void LogIn()
    {
        Debug.Log("로그인 시도");
        //로그인이 안되어 있으면
        if (!Social.localUser.authenticated)
        {

            Social.localUser.Authenticate((bool isSuccess) =>
            {
                if (isSuccess)
                {
                    Debug.Log("구글 로그인 성공");
                    SceneManager.LoadScene("Main");
                }
                else
                {
                    Debug.Log("구글 로그인 실패");

                }
            });

        }
        
    }

    void LogOut()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
        Debug.Log("구글 로그아웃"); 
    }
}
