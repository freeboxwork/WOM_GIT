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
    public EnumDefinetion.MonsterType monsterType;
    public EnumDefinetion.AttackType attackType;

    void Start()
    {

    }

   
}

/*
[System.Serializable]
public class MonsterNomal : MonsterBase
{

}

[System.Serializable]
public class MonsterGold : MonsterBase
{

}

[System.Serializable]
public class MonsterBoss : MonsterBase
{

}
*/