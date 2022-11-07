using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteDatas", menuName = "ScriptableObject/SpriteData")]
public class SpriteFileData : ScriptableObject
{
    [SerializeField]
    SpriteDataInfo[] data;

    public Sprite GetSpriteData(int num) => data[num].sprite;
    public Sprite GetIconData(int num) => data[num].icon;

    [System.Serializable]
    public struct SpriteDataInfo
    {
        public EnumDefinition.UnionGradeType type;
        public string discription;
        public Sprite icon;
        public Sprite sprite;
    }
}


