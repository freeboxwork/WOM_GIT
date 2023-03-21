using SRDebugger.UI.Other;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonEnterPopup : MonoBehaviour
{

    public Image keyIcon;

    public TextMeshProUGUI textClearTicket;
    public TextMeshProUGUI textKeyCount;

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
            EventManager.instance.RunEvent(CallBackEventType.TYPES.OnDungeonMonsterChallenge, curMonsterType);
            contents.SetActive(false); 
        });
    }


    public void EnablePopup(EnumDefinition.MonsterType monsterType)
    {
        curMonsterType= monsterType;   
        contents.SetActive(true);
        SetKeyUI(monsterType);
    }
    
    public void SetKeyUI(EnumDefinition.MonsterType monsterType)
    {
        keyIcon.sprite = monsterTypeToIconMap[monsterType];
        // 던전별 보유 열쇠 수
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
