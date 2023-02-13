using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.Networking;

public class SaveDataManager : MonoBehaviour
{
    public SaveDataTotal saveDataTotal;
    const string dataFileName = "saveData.json";
    public GlobalData globalData;

    void Start()
    {
        globalData = FindObjectOfType<GlobalData>();

    }

   


    public IEnumerator Init()
    {
        LoadDataFromFile(); 
        
        yield return new WaitForEndOfFrame();

        SetData();
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyUp(KeyCode.J))
        {
            SampleSaveJsonData();
        }
    }


    // SMAPLE
    void SampleSaveJsonData()
    {
        SaveDataTest saveDataTest = new SaveDataTest();
        saveDataTest.skillType = EnumDefinition.SkillType.insectDamageUp;
        saveDataTest.level = 1;
        saveDataTest.saveDataUnions.Add(new SaveDataUnion() { equipSlotId = 1, level = 2, isEquip = false, unionId = 20 });

        var data = JsonUtility.ToJson(saveDataTest,true);
        File.WriteAllText(Application.dataPath + "/data.json", data);
    }


    public void SaveDataToFile()
    {
        var jsonData = JsonUtility.ToJson(saveDataTotal);
        var path = GetSaveDataFilePaht();
        File.WriteAllText(path, jsonData);
    }

    void LoadDataFromFile()
    {
        var path = GetSaveDataFilePaht();
        saveDataTotal = JsonUtility.FromJson<SaveDataTotal>(path);
    }

    void SetData()
    {

    }


    #region UTILITY METHOD
    public SaveDataTraning GetSaveDataTraningByTraningType(EnumDefinition.SaleStatType traningType)
    {
        foreach (SaveDataTraning traning in saveDataTotal.saveDataTranings.tranings)
        {
            if (traning.traningType == traningType)
            {
                return traning;
            }
        }
        return null;
    }

    public void SetLevelByTraningType(EnumDefinition.SaleStatType traningType, int newLevel)
    {
        SaveDataTraning traning = GetSaveDataTraningByTraningType(traningType);

        if (traning != null)
        {
            traning.level = newLevel;
        }
    }






    string GetSaveDataFilePaht()
    {
        string path = "";
#if UNITY_EDITOR
        path = Application.dataPath + "/"+ dataFileName;
#elif UNITY_ANDROID
            path = Application.persistentDataPath + "/"+ dataFileName;
#endif
        return path;
    }


    #endregion

}


#region MODELS


// SAMOPLE
[System.Serializable]
public class SaveDataTest
{
    public EnumDefinition.SkillType skillType;
    public int level;
    public List<SaveDataUnion> saveDataUnions= new List<SaveDataUnion>();   
}


[System.Serializable]
public class SaveDataTotal
{
    public SaveDataTranings saveDataTranings;
    public SaveDataEvolution saveDataEvolution;
    public SaveDataUnions saveDataUnions;
    public SaveDataDNA saveDataDNA;
    public SaveDataSkills saveDataSkills;
    public SaveDataShop saveDataShop;
    public SaveDataStage saveDataStage;
    public SaveDataGoods saveDataGoods;
    public SaveDataDateTime saveDataDateTime;
    public SaveDataSystem saveDataSystem;

}

#region old
//public int level_trainingDamage;
//public int level_trainingCriticalChance;
//public int level_trainingCriticalDamage;
//public int level_talentDamage;
//public int level_talentCriticalChance;
//public int level_talentCriticalDamage;
//public int level_talentMoveSpeed;
//public int level_talentSpawnSpeed;
//public int level_talentGoldBonus;
#endregion

[System.Serializable]
public class SaveDataTranings
{
    public List<SaveDataTraning> tranings = new List<SaveDataTraning>();
}

[System.Serializable]
public class SaveDataTraning
{
    public int level;
    public EnumDefinition.SaleStatType traningType;
}

[System.Serializable]
public class SaveDataEvolution
{
    // 진화 레벨 
    public int level_evolution;
    // 진화 주사위 돌려서 획득한 데이터 저장
    public DiceEvolutionInGameData diceEvolutionData;
}


[System.Serializable]
public class SaveDataUnions
{
    public List<SaveDataUnion> unions = new List<SaveDataUnion>();
}

[System.Serializable]
public class SaveDataUnion
{
    public int unionId;
    public int level;
    public int equipSlotId;
    public bool isEquip;
}


[System.Serializable]
public class SaveDataDNA
{
    public int level_insectDamage;
    public int level_insectCriticalChance;
    public int level_insectCriticalDamage;
    public int level_unionDamage;
    public int level_glodBonus;
    public int level_insectMoveSpeed;
    public int level_unionMoveSpeed;
    public int level_unionSpawnTime;
    public int level_goldPig;
    public int level_skillDuration;
    public int level_skillCoolTime;
    public int level_bossDamage;
    public int level_monsterHpLess;
    public int level_boneBonus;
    public int level_goldMonsterBonus;
    public int level_offlineBonus;
}


[System.Serializable]
public class SaveDataSkills
{
    /*
    insectDamageUp;
    unionDamageUp;
    allUnitSpeedUp;
    glodBonusUp;
    monsterKing;
    allUnitCriticalChanceUp;
    */
    public List<SaveDataSkill> saveDataSkills= new List<SaveDataSkill>();
}

[System.Serializable]
public class SaveDataSkill
{
    public int level;
    public bool isUsing; // 스킬 사용중 표시
    public float leftSkillTime; // 스킬 남은 시간 
    public EnumDefinition.SkillType skillType;
}


[System.Serializable]
public class SaveDataShop
{

}

[System.Serializable]
public class SaveDataStage
{
    public int stageLevel;
    public int phaseCount;
}


[System.Serializable]
public class SaveDataGoods
{
    public int gold;
    public int gem;
    public int bone;
    public int dice;
}

[System.Serializable]
public class SaveDataDateTime
{
    public DateTime time_gameEnd;
    public DateTime time_AD_Reset;
}

[System.Serializable]

public class SaveDataSystem
{
    // 배경음 OnOff
    public bool sfx_bg;
    // 효과음 OnOff
    public bool sfx_eff;
    // 투토리얼 진행 스탭
    public int tutorial_step;
}

#endregion
