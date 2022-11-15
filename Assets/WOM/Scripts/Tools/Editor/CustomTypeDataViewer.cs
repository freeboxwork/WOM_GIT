using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine.UIElements;

public class CustomTypeDataViewer : EditorWindow
{

    [MenuItem("GM_TOOLS/CustomTypeDataViewer")]
    public static void ShowWindow()
    {
        var window = GetWindow<CustomTypeDataViewer>();
        window.Show();
    }

    List<bool> enableTypes;
    AnimBool tAnimBool;
    private void OnEnable()
    {
        SetEnableTypes();
        tAnimBool = new AnimBool(false);
        tAnimBool.valueChanged.AddListener(Repaint);
    }

    void SetEnableTypes()
    {
        enableTypes = new List<bool>();

    }
    Vector2 scrollViwe;

    bool fold = false;
    private void OnGUI()
    {
        
        if (GUILayout.Button("On/Off"))
        {
            fold = !fold;
        }
        tAnimBool.target = fold;
     
        FoldIn(tAnimBool);
        

    }

    void FoldIn(AnimBool animValue)
    { 
        var value = EditorGUILayout.BeginFadeGroup(animValue.faded);
        if (value)
        {

            
           
            
            GUILayout.Button("TEST");
            GUILayout.Button("TEST");
            GUILayout.Button("TEST");
            GUILayout.Button("TEST");
            GUILayout.Button("TEST");
            
            GUILayout.EndScrollView();
            
        }
        
        
    }


}
