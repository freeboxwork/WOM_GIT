using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InsectSpriteData", menuName = "ScriptableObject/InsectSpriteData")]
public class InsectSpriteFileData : ScriptableObject
{
    [Header("beeSprite")] public Sprite[] bee;
    [Header("beetleSprite")] public Sprite[] beetle;
    [Header("mantisSprite")] public Sprite[] mantis;

    public Sprite GetInsectFaceSprite(EnumDefinition.InsectType type, int id)
    {
        switch(type)
        {
            case EnumDefinition.InsectType.bee: return bee[id];
            case EnumDefinition.InsectType.beetle: return beetle[id];
            case EnumDefinition.InsectType.mentis: return mantis[id];
        }
        return null;
    }
}
