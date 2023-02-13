using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using static EnumDefinition;
public class SaveDataManager : MonoBehaviour
{
    public SaveDataTotal saveDataTotal;
    const string dataFileName = "saveData.json";
    public GlobalData globalData;

    /* 맥스 레벨 제한값 추가 되어야 함 */
    // 데이터 로드 했을때 현재 레벨이 맥스 레벨임에도 그 이상의 레벨을 요구할 경우 문제가 생기기 때문에 필히 예외 처리 해야함
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

    // 트레이닝 데이터 레벨 세팅
    public void SetLevelByTraningType(EnumDefinition.SaleStatType traningType, int newLevel)
    {
        SaveDataTraning traning = GetSaveDataTraningByTraningType(traningType);

        if (traning != null)
        {
            traning.level = newLevel;
        }
    }

    public SaveDataDNA GetDNADataByType(DNAType dnaType)
    {
        foreach (SaveDataDNA dna in saveDataTotal.saveDataDNAs.saveDatas)
        {
            if (dna.dnaType  == dnaType)
            {
                return dna;
            }
        }
        return null;
    }

    public void SetLevelDNAByType(DNAType dNAType, int level)
    {
        GetDNADataByType(dNAType).level = level;
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
    public SaveDataDNAs saveDataDNAs;
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
    // 진화 레벨 ( 진화 레벨에 따라 슬롯 오픈됨 )
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
public class SaveDataDNAs
{
    #region data list
    /*
        insectDamage,
        insectCriticalChance,
        insectCriticalDamage,
        unionDamage,
        glodBonus,
        insectMoveSpeed,
        unionMoveSpeed,
        unionSpawnTime,
        goldPig,
        skillDuration,
        skillCoolTime,
        bossDamage,
        monsterHpLess,
        boneBonus,
        goldMonsterBonus,
        offlineBonus
     */
    #endregion

    public List<SaveDataDNA> saveDatas = new List<SaveDataDNA>();
}

[System.Serializable]
public class SaveDataDNA
{
    public int level;
    public DNAType dnaType;
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
