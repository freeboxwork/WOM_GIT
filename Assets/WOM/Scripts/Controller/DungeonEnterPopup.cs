using SRDebugger.UI.Other;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using static EnumDefinition;

public class DungeonEnterPopup : MonoBehaviour
{

    public Image keyIcon;

    public TextMeshProUGUI textClearTicket;
    public TextMeshProUGUI textKeyCount;
    public TextMeshProUGUI textPervClearLevel;
    public TextMeshProUGUI textRewardValue;

    public SerializableDictionary<EnumDefinition.MonsterType, Sprite> monsterTypeToIconMap;

    public EnumDefinition.MonsterType curMonsterType;
    public GameObject contents;

    public Button btn_AD_Dungeon;
    public Button btn_KeyDungeon;
    public Button btn_Ticket_Dungeon;

    private void Start()
    {
        SetBtnEvents();
    }

    public void SetTicketUI(int count)
    {

    }

    void SetBtnEvents()
    {
        btn_KeyDungeon.onClick.AddListener(() => {
            if (IsValidDungeonKeyCount(curMonsterType))
            {
                EventManager.instance.RunEvent(CallBackEventType.TYPES.OnDungeonMonsterChallenge, curMonsterType);
            }
            contents.SetActive(false);
        });
    }


    public void EnablePopup(EnumDefinition.MonsterType monsterType)
    {
        curMonsterType= monsterType;   
        var monsterLevel = GlobalData.instance.player.dungeonMonsterClearLevel.GetLeveByDungeonMonType(curMonsterType);
        var data = GlobalData.instance.dataManager.GetDungeonMonsterDataByTypeLevel(curMonsterType, monsterLevel);
        contents.SetActive(true);
        SetKeyUI(monsterType);
        SetRewardUI(monsterLevel,data);
    }

    void SetRewardUI(int level, DungeonMonsterData data)
    {
        textPervClearLevel.text = $"{level}�ܰ�";
        textRewardValue.text = data.currencyAmount.ToString();

    }

    // ���� ���� ���� ��� ���� üũ
    bool IsValidDungeonKeyCount(EnumDefinition.MonsterType monsterType)
    {
        var usingKeyCount = GlobalData.instance.monsterManager.GetMonsterDungeon().monsterToDataMap[monsterType].usingKeyCount;
        var curKeyCount = GlobalData.instance.player.GetCurrentDungeonKeyCount(monsterType);
        if (curKeyCount < usingKeyCount)
        {
            // enable popup
            // TODO: �ڵ� ����ȭ �� �����丵
            int messageId = 12;
            switch (monsterType)
            {
                case EnumDefinition.MonsterType.dungeonGold: messageId = 12; break;
                case EnumDefinition.MonsterType.dungeonBone: messageId = 13; break;
                case EnumDefinition.MonsterType.dungeonDice: messageId = 14; break;
                case EnumDefinition.MonsterType.dungeonCoal: messageId = 15; break;
            }
            // message popup (���谡 �����մϴ�)
            GlobalData.instance.globalPopupController.EnableGlobalPopupByMessageId("Message", messageId);

            return false;
        }
        return true;
    }


    public void SetKeyUI(EnumDefinition.MonsterType monsterType)
    {
        keyIcon.sprite = monsterTypeToIconMap[monsterType];
        // ������ ���� ���� ��
        var keyCount = GlobalData.instance.player.GetCurrentDungeonKeyCount(monsterType);
        textKeyCount.text = keyCount.ToString();
    }

    public void SetDungeonEnterPopup(int clear, int key, Sprite sp)
    {
        textClearTicket.text = string.Format("{0}", clear);
        textKeyCount.text = string.Format("{0}", key);
        keyIcon.sprite = sp;
    }








}
