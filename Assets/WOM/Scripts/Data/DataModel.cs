using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro.EditorUtilities;

public class DataModel : MonoBehaviour
{
   
}


[Serializable]
public class SaveData
{
    public bool isFirstConnect;
    public int stageIdx;
    public InsectSaveData beeSaveData;
    public InsectSaveData beetleSaveData;
    public InsectSaveData mentisSaveData;
    public int upgradeLevelIdx;
    public int gold;
    public string offlineTime;
    public string playingTime;
}

[Serializable]
public class InsectSaveData
{
    public int evolutionIdx;
    public int upgradeLevel;
    public string evolutionTechTree;
    public EvolutionData evolutionLastData;
}


[Serializable]
public class StageData
{
    public int stageId;
    public string stageName;
    public int monsterNormalId;
    public int monsterBossId;
}





[Serializable]
public class EvolutionData
{

}
