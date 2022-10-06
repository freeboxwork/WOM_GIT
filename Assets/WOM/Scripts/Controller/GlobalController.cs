using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{

    public DataManager dataManager;
    public PlayerDataManager playerDataManager;
    public InsectManager insectManager;
    public StageManager stageManager;
         
         

    void Start()
    {
        if (dataManager == null) dataManager = FindObjectOfType<DataManager>();
        StartCoroutine(Init());
    }
   
    IEnumerator Init()
    {
        // set data
        yield return StartCoroutine(dataManager.SetDatas());

        // get player data ( ���� ������ ���� �Ǿ��ִ� ������ �ε� )
        yield return StartCoroutine(playerDataManager.InitPlayerData());

        // Player data ����
        

        // ���� ������ ����
        yield return StartCoroutine(insectManager.Init(playerDataManager));


        // ���� ������ ����


        // �������� ����
        yield return StartCoroutine(stageManager.Init(playerDataManager.saveData.stageIdx));

    }

}
