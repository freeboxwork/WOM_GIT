using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
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

            var d = new Rect(position.x, position.y, 10, 80);
            EditorGUI.DrawRect(d, GetPropertyColor(property));

            //아이콘 이미지 위치조정
            var iconRect = new Rect(position.x + 10, position.y, 64, 64);
            var iconProperty = property.FindPropertyRelative("icon");
            iconProperty.objectReferenceValue =
                EditorGUI.ObjectField(iconRect, iconProperty.objectReferenceValue, typeof(Sprite), false);
            EditorGUI.LabelField(new Rect(position.x + 10, position.y + 40, 64, 64), "icon");

            //스프라이트 이미지 위치조정
            var spriteRect = new Rect(position.x + 75, position.y, 64, 64);
            var spriteProperty = property.FindPropertyRelative("sprite");
            spriteProperty.objectReferenceValue =
                EditorGUI.ObjectField(spriteRect, spriteProperty.objectReferenceValue, typeof(Sprite), false);
            EditorGUI.LabelField(new Rect(position.x + 75, position.y + 40, 64, 64), "sprite");

            //라벨 (정보표시)
            string indexNumber = property.displayName;
            indexNumber = Regex.Replace(indexNumber, @"\D", "");
            string spriteName = "";
            if(property.FindPropertyRelative("sprite").objectReferenceValue != null)
            {
                spriteName = property.FindPropertyRelative("sprite").objectReferenceValue.name;
            }

            EditorGUI.LabelField(new Rect(position.x + 150, position.y, position.width * 0.5f, position.height),
                                "Number : " + indexNumber + " , " + spriteName);

            //라벨
            EditorGUI.LabelField(new Rect(position.x + 150, position.y + (position.height + 2), position.width * 0.2f, position.height),
                                "Discription");

            //설명
            var discriptionRect =
                new Rect(position.x + 220, position.y + (position.height + 2), position.width * 0.3f, position.height);
            var discriptionProperty = property.FindPropertyRelative("discription");
            EditorGUI.PropertyField(discriptionRect, discriptionProperty, GUIContent.none);

            //Enum
            var enumRect = new Rect(position.x + 150, position.y +((position.height*2) + 10) , halfWidth, position.height);
            var enumPropoerty = property.FindPropertyRelative("type");
            EditorGUI.PropertyField(enumRect, enumPropoerty, GUIContent.none);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        //return base.GetPropertyHeight(property, label);
        //float height = EditorGUI.GetPropertyHeight(property, label, true);
        return 80;
    }

    public Color GetPropertyColor(SerializedProperty property)
    {
        var valueIndex = property.FindPropertyRelative("type").enumValueIndex;
        switch (valueIndex)
        {
            case 0: return new Color(0.8f, 0.8f, 0.8f);
            case 1: return new Color(0.8f, 0.95f, 0.75f);
            case 2: return new Color(1.0f, 0.98f, 0.55f);
            case 3: return new Color(0.8f, 0.5f, 0.6f);
            case 4: return new Color(1.0f, 0.6f, 0.0f);
            case 5: return Color.red;
        }
        return Color.white;
    }
}
#endif
