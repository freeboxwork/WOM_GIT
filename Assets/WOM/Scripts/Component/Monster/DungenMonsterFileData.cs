using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungenMonsterFileData : ScriptableObject
{
    public EnumDefinition.GoodsType goodsType;
    public EnumDefinition.MonsterType monsterType;
    public int monsterFaceId;
    public float battleTime;
    public int maxKeyCount;
    public int maxAdCount;
    public int bgID;
}
