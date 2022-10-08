using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{

    public DataManager dataManager;
    public PlayerDataManager playerDataManager;
    public InsectManager insectManager;
    public StageManager stageManager;
    public MonsterManager monsterManager;
    public Player player;
    public AttackController attackController;


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
        yield return StartCoroutine(player.Init(playerDataManager.saveData));
        
        // 스테이지 세팅
        yield return StartCoroutine(stageManager.Init(playerDataManager.saveData.stageIdx));

        // 곤충 데이터 세팅
        yield return StartCoroutine(insectManager.Init(playerDataManager));

        // 몬스터 데이터 세팅
        yield return StartCoroutine(monsterManager.Init(stageManager.stageData.stageId));

        // 타겟 몬스터 지정 -> 첫 시작은 노멀 몬스터
        player.SetCurrentMonster(monsterManager.monsterNormal);

        // 등장 몬스터 활성화 -> 첫 시작은 노멀 몬스터
        monsterManager.EnableMonster(EnumDefinition.MonsterType.normal);

        // 공격 가능 상태로 전환
        attackController.SetAttackableState(true);
        
    }

}
