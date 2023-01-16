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
    public UiController uiController;
    public EffectManager effectManager;
    public LotteryManager lotteryManager;
    public EvolutionDiceLotteryManager evolutionDiceLotteryManager;
    public SkillManager skillManager;
    public UnionManager unionManager;


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
        
        // 스테이지 세팅
        yield return StartCoroutine(stageManager.Init(playerDataManager.saveData.stageIdx));

        // 곤충 세팅
        yield return StartCoroutine(insectManager.Init(playerDataManager));

        // 몬스터 세팅
        yield return StartCoroutine(monsterManager.Init(stageManager.stageData.stageId));

        // 이펙트 세팅
        yield return StartCoroutine(effectManager.Init());

        // Player data 세팅
        yield return StartCoroutine(player.Init(playerDataManager.saveData));

        // UI Controller 세팅 
        yield return StartCoroutine(uiController.Init());

        // 뽑기 매니저 초기화 ( 첫 뽑기 데이터는 모두 0번 )
        yield return StartCoroutine(lotteryManager.Init(0));

        // 진화 주사위 뽑기 세팅
        yield return StartCoroutine(evolutionDiceLotteryManager.Init());

        // 스킬 데이터 및 UI 세팅
        yield return StartCoroutine(skillManager.Init());

        // 유니온 데이터및 UI 세팅
        yield return StartCoroutine(unionManager.Init());

        // 타겟 몬스터 지정 -> 첫 시작은 노멀 몬스터
        player.SetCurrentMonster(monsterManager.monsterNormal);

        // 등장 몬스터 활성화 -> 첫 시작은 노멀 몬스터
        // TODO : 하나만 켜야 하는지 확인 필요 현재 모두 나타나도록 수정
        monsterManager.EnableMonster(EnumDefinition.MonsterType.normal);

        // UI 초기화
        SetUI_Init();

        // Monster In Animation
        yield return StartCoroutine(player.currentMonster.inOutAnimator.AnimPositionIn());

        // 공격 가능 상태로 전환
        attackController.SetAttackableState(true);

    }

    void SetUI_Init()
    {
        // set boss monster hp
        var hp = player.currentMonster.hp;
        var phaseCount = player.currentStageData.phaseCount;
        uiController.SetTxtMonsterHp(hp);
        uiController.SetTxtPhaseCount(phaseCount);
    }




}
