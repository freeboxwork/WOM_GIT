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
        yield return StartCoroutine(uiController.SetUiDatas());
        
        // get player data ( ���� ������ ���� �Ǿ��ִ� ������ �ε� )
        yield return StartCoroutine(playerDataManager.InitPlayerData());
        
        // �������� ����
        yield return StartCoroutine(stageManager.Init(playerDataManager.saveData.stageIdx));

        // ���� ������ ����
        yield return StartCoroutine(insectManager.Init(playerDataManager));

        // ���� ������ ����
        yield return StartCoroutine(monsterManager.Init(stageManager.stageData.stageId));

        // Player data ����
        yield return StartCoroutine(player.Init(playerDataManager.saveData));

        // Ÿ�� ���� ���� -> ù ������ ��� ����
        player.SetCurrentMonster(monsterManager.monsterNormal);

        // ���� ���� Ȱ��ȭ -> ù ������ ��� ����
        monsterManager.EnableMonster(EnumDefinition.MonsterType.normal);

        // UI �ʱ�ȭ
        SetUI_Init();

        // ���� ���� ���·� ��ȯ
        attackController.SetAttackableState(true);
        
    }

    void SetUI_Init()
    {
        // set boss monster hp
        var hp = player.currentMonster.hp;
        uiController.SetTxtMonsterHp(hp);
    }




}
