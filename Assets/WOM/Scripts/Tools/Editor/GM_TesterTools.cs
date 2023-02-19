using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static EnumDefinition;

public class GM_TesterTools : EditorWindow
{
    Transform t;

    [MenuItem("GM_TOOLS/TesterTools")]
    public static void ShowWindwo()
    {
        var window = GetWindow<GM_TesterTools>();
        window.Show();
    }

    private void OnEnable()
    {
        var tg = GameObject.Find("TesterTransform");
        if (tg == null)
        {
            tg = new GameObject("TesterTransform");
        }
        t = tg.transform;
    }

    private void OnGUI()
    {
        EditorCustomGUI.GUI_Title("게임 테스트를 위한 툴 모음");
        
        EditorCustomGUI.GUI_Button("골드 10000 추가", () => { GlobalData.instance.player.AddGold(1000); });
        EditorCustomGUI.GUI_Button("뼈조각 10000 추가", () => { GlobalData.instance.player.AddBone(1000); });
        EditorCustomGUI.GUI_Button("보석 10000 추가", () => { GlobalData.instance.player.AddGem(1000); });
        EditorCustomGUI.GUI_Button("주사위 10000 추가", () => { GlobalData.instance.player.AddDice(1000); });
        EditorCustomGUI.GUI_Button("몬스터 즉시 사냥", () => { KillMonster(); });
    }

   

    void KillMonster()
    {
        var player = GlobalData.instance.player;
        player.currentMonster.hp = 0;
        EventManager.instance.RunEvent(CallBackEventType.TYPES.OnMonsterHit, EnumDefinition.InsectType.bee,0, t);
    }

}
