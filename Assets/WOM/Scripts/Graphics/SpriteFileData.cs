using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteDatas", menuName = "ScriptableObject/SpriteData")]
public class SpriteFileData : ScriptableObject
{
    [SerializeField]
    public SpriteDataInfo[] data;

    public Sprite GetSpriteData(int num, out Sprite sprite)
    {
        sprite = data[num].sprite;
        return data[num].icon;
    }

    [System.Serializable]
    public struct SpriteDataInfo
    {
        public EnumDefinition.UnionGradeType type;
        public int num;
        public string discription;
        public Sprite icon;
        public Sprite sprite;
    }
}


