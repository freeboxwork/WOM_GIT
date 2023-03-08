using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;

public class GoogleLogin : MonoBehaviour
{
    public Button btnLogin;
    public Button btnLogout;


    void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        SetEvents();
    }

    void SetEvents()
    {
        btnLogin.onClick.AddListener(Login);
        btnLogout.onClick.AddListener(LogOut);
    }

    void Login()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated == false)
        {
            Social.localUser.Authenticate((bool succes) =>
            {
                string txt;
                if (succes)
                {
                    txt = $"로그인 성공\n{Social.localUser.id}\n{Social.localUser.userName}";
                }
                else
                {
                    txt = $"로그인 실패";
                }
                Debug.Log(txt);
            });
        }
    }

    void LogOut()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
        string txt = "로그아웃 완료";
        Debug.Log(txt);
    }
}
