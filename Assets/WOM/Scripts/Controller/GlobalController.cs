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


    void Start()
    {
        if (dataManager == null) dataManager = FindObjectOfType<DataManager>();
        StartCoroutine(Init());
    }
   
    IEnumerator Init()
    {
        // set data
        yield return StartCoroutine(dataManager.SetDatas());

        // set ui data
        yield return StartCoroutine(uiController.Init());
        
        // get player data ( ���� ������ ���� �Ǿ��ִ� ������ �ε� )
        yield return StartCoroutine(playerDataManager.InitPlayerData());
        
        // �������� ����
        yield return StartCoroutine(stageManager.Init(playerDataManager.saveData.stageIdx));

        // ���� ����
        yield return StartCoroutine(insectManager.Init(playerDataManager));

        // ���� ����
        yield return StartCoroutine(monsterManager.Init(stageManager.stageData.stageId));

        // ����Ʈ ����
        yield return StartCoroutine(effectManager.Init());

        // Player data ����
        yield return StartCoroutine(player.Init(playerDataManager.saveData));

        // Ÿ�� ���� ���� -> ù ������ ��� ����
        player.SetCurrentMonster(monsterManager.monsterNormal);

        // ���� ���� Ȱ��ȭ -> ù ������ ��� ����
        // TODO : �ϳ��� �Ѿ� �ϴ��� Ȯ�� �ʿ� ���� ��� ��Ÿ������ ����
        monsterManager.EnableMonster(EnumDefinition.MonsterType.normal);

        // UI �ʱ�ȭ
        SetUI_Init();

        // Monster In Animation
        yield return StartCoroutine(player.currentMonster.inOutAnimator.AnimPositionIn());

        // ���� ���� ���·� ��ȯ
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
