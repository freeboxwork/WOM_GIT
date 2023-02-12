using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StatViewer : EditorWindow
{

    float labelWidth = 200;


    [MenuItem("GM_TOOLS/STAT Viewer")]
    public static void ShowWindow()
    {
        var window = GetWindow<StatViewer>();
        window.Show();
    }


    

    private void OnGUI()
    {
        EditorCustomGUI.GUI_Title("���� ������ ���� Ȯ�� �� �� �ֽ��ϴ�.");

        if (Application.isPlaying && GlobalData.instance.statManager!= null)
        {
            var insectBeeDamage = GlobalData.instance.statManager.GetInsectDamage(EnumDefinition.InsectType.bee);
            var insectBeetleDamage = GlobalData.instance.statManager.GetInsectDamage(EnumDefinition.InsectType.bee);
            var insectMentisDamage = GlobalData.instance.statManager.GetInsectDamage(EnumDefinition.InsectType.bee);

            var insectBeeSpeed = GlobalData.instance.statManager.GetInsectMoveSpeed(EnumDefinition.InsectType.bee);
            var insectBeetleSpeed = GlobalData.instance.statManager.GetInsectMoveSpeed(EnumDefinition.InsectType.bee);
            var insectMentisSpeed = GlobalData.instance.statManager.GetInsectMoveSpeed(EnumDefinition.InsectType.bee);

            EditorCustomGUI.GUI_Label(labelWidth, "Bee ���ݷ�", insectBeeDamage.ToString());
            EditorCustomGUI.GUI_Label(labelWidth, "Beetle ���ݷ�", insectBeetleDamage.ToString());
            EditorCustomGUI.GUI_Label(labelWidth, "Mentis ���ݷ�", insectMentisDamage.ToString());

            EditorCustomGUI.GUI_Label(labelWidth, "Bee �̵� �ӵ�", insectBeeSpeed.ToString());
            EditorCustomGUI.GUI_Label(labelWidth, "Beetle �̵� �ӵ�", insectBeetleSpeed.ToString());
            EditorCustomGUI.GUI_Label(labelWidth, "Mentis �̵� �ӵ�", insectMentisSpeed.ToString());



        }
        else
        {
            GUILayout.Box("��� ���϶��� Ȯ�� ���� �մϴ�.");
        }
        
    }

}
