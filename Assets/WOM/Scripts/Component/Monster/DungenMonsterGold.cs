using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungenMonsterGold : DungeonMonsterBase
{

    public DungeonMonsterDatas datas;


    void Start()
    {
        datas = JsonUtility.FromJson<DungeonMonsterDatas>(jsonData.text);
    }

    
}
