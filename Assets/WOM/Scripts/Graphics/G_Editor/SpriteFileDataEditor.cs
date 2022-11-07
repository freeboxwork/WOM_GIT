using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

#if UNITY_EDITOR
/*
[CustomEditor(typeof(SpriteFileData))]
public class SpriteDataListInspector : Editor
{
    ReorderableList lists;

    private void OnEnable()
    {
        var prop = serializedObject.FindProperty("spriteDatas");
        lists = new ReorderableList(serializedObject, prop);

        //������Ʈ ũ��
        lists.elementHeight = 80;

        //���
        lists.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, "spriteDatas");

        //������Ʈ ����
        lists.drawElementCallback = (rect, index, isActive, isFocused) =>
        {
            var element = prop.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(rect, element);
        };
    }


    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        serializedObject.Update();
        lists.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
*/
[CustomPropertyDrawer(typeof(SpriteFileData.SpriteDataInfo))]
public class SpriteDataInfoDraw : PropertyDrawer
{
    SpriteFileData.SpriteDataInfo data;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //base.OnGUI(position, property, label);

        using (new EditorGUI.PropertyScope(position, label, property))
        {
            EditorGUIUtility.labelWidth = 50;

            position.height = EditorGUIUtility.singleLineHeight;
            var halfWidth = position.width * 0.5f;

            //������ �̹��� ��ġ����
            var iconRect = new Rect(position.x, position.y, 64, 64);
            var iconProperty = property.FindPropertyRelative("icon");
            iconProperty.objectReferenceValue =
                EditorGUI.ObjectField(iconRect, iconProperty.objectReferenceValue, typeof(Sprite), false);
            EditorGUI.LabelField(new Rect(position.x, position.y + 40, 64, 64), "icon");



            //��������Ʈ �̹��� ��ġ����
            var spriteRect = new Rect(position.x + 65, position.y, 64, 64);
            var spriteProperty = property.FindPropertyRelative("sprite");
            spriteProperty.objectReferenceValue =
                EditorGUI.ObjectField(spriteRect, spriteProperty.objectReferenceValue, typeof(Sprite), false);
            EditorGUI.LabelField(new Rect(position.x + 65, position.y + 40, 64, 64), "sprite");


            //��
            EditorGUI.LabelField(new Rect(position.x + 140, position.y, position.width * 0.2f, position.height),
                                "Number");

            //������ �ѹ�
            var numberRect = new Rect(position.x + 200, position.y, position.width * 0.3f, position.height);
            var numberProperty = property.FindPropertyRelative("num");
            EditorGUI.PropertyField(numberRect, numberProperty, GUIContent.none);

            //��
            EditorGUI.LabelField(new Rect(position.x + 140, position.y + (position.height + 8), position.width * 0.2f, position.height),
                                "Discription");

            //����
            var discriptionRect =
                new Rect(position.x + 140, position.y + ((position.height * 2) + 10), halfWidth, position.height);
            var discriptionProperty = property.FindPropertyRelative("discription");
            EditorGUI.PropertyField(discriptionRect, discriptionProperty, GUIContent.none);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        //return base.GetPropertyHeight(property, label);
        //float height = EditorGUI.GetPropertyHeight(property, label, true);
        return 80;
    }
}
#endif
