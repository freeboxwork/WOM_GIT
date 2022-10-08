using ProjectGraphics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{
    public int monsterId;
    public float hp;
    public float exp;
    public float gold;
    public int goldCount;
    public int imageId;
    public int bgId;
    public EnumDefinition.MonsterType monsterType;
    public EnumDefinition.AttackType attackType;

    public SpriteLibraryChanged spriteLibraryChanged;
    
    void Start()
    {

    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.transform.name);
    }


}

