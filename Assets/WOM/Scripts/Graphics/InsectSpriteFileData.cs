using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InsectSpriteData", menuName = "ScriptableObject/InsectSpriteData")]
public class InsectSpriteFileData : ScriptableObject
{
    [Header("beeSprite")] public Sprite[] bee;
    [Header("beetleSprite")] public Sprite[] beetle;
    [Header("mantisSprite")] public Sprite[] mantis;
}
