using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;

public class GoogleLogin : MonoBehaviour
{
    public Button btnLogIn;
    public Button btnLogOut;

    void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    void SetBtnEvents()
    {
        btnLogIn.onClick.AddListener(LogIn);
        btnLogOut.onClick.AddListener(LogOut);
    }
    

    void LogIn()
    {
        if(PlayGamesPlatform.Instance.localUser.authenticated == false)
        {
            Social.localUser.Authenticate((bool sucess) =>
            {
                if (sucess)
                {
                    Debug.Log("구글 로그인 성공");
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
