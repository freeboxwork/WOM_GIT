using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteDatas", menuName = "ScriptableObject/SpriteData")]
public class SpriteFileData : ScriptableObject
{
    [SerializeField]
    public SpriteDataInfo[] normal;
    public SpriteDataInfo[] high;
    public SpriteDataInfo[] rare;
    public SpriteDataInfo[] hero;
    public SpriteDataInfo[] legend;
    public SpriteDataInfo[] unique;

    [System.Serializable]
    public struct SpriteDataInfo
    {
        public int num;
        public string discription;
        public Sprite icon;
        public Sprite sprite;
    }
}


