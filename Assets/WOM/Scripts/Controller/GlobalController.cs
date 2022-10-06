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

        // get player data ( 게임 종료전 저장 되어있는 데이터 로드 )
        yield return StartCoroutine(playerDataManager.InitPlayerData());

        // Player data 세팅
        

        // 곤충 데이터 세팅
        yield return StartCoroutine(insectManager.Init(playerDataManager));


        // 몬스터 데이터 세팅


        // 스테이지 세팅
        yield return StartCoroutine(stageManager.Init(playerDataManager.saveData.stageIdx));

    }

}
