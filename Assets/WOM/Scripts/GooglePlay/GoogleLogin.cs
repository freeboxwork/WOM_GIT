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
                    Debug.Log("���� �α��� ����");
                }
                else
                {
                    Debug.Log("���� �α��� ����");
                }

            });
        }
    }

    void LogOut()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
        Debug.Log("���� �α׾ƿ�"); 
    }
}
