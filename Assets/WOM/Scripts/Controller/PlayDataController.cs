using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class PlayDataController : MonoBehaviour
{
    public SaveData saveData;
    string fileName = "saveData.txt";
    string path;


    System.DateTime startDataTime;
    
    void Start()
    {
        path = Path.Combine(Application.dataPath, fileName);
        SetGamePlayStartDateTime();
        LoadData();
    }

    void SetGamePlayStartDateTime()
    {
        startDataTime = System.DateTime.Now;
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    void Update()
    {
        
    }

    // save data
    void SaveData()
    {
        // Sample Code
        // TODO : 각 데이터에서 값 로드 하여 저장
        saveData = new SaveData();
        saveData.isFirstConnect = false;
        saveData.stageIdx = 0;
        saveData.beeSaveData = GetInsectSaveData();
        saveData.beetleSaveData = GetInsectSaveData();
        saveData.mentisSaveData = GetInsectSaveData();
        saveData.upgradeLevelIdx = 0;
        saveData.gold = 123;
        saveData.offlineTime = GetOfflineTime();
        saveData.playingTime = GetPlayingTime();

        var json = JsonUtility.ToJson(saveData);

        Debug.Log(json);

        // save file
        //File.CreateText(path);  
        File.WriteAllText(path, json, System.Text.Encoding.Default);
    }

    //TODO : 계산식 적용
    string GetOfflineTime()
    {
        return "1";
    }

    //TODO : 계산식 적용
    string GetPlayingTime()
    {
        return "2";
    }



    InsectSaveData GetInsectSaveData()
    {
        InsectSaveData data = new InsectSaveData();
        data.evolutionIdx = 1;
        data.upgradeLevel = 1;
        data.evolutionTechTree = "1,2,3";
        data.evolutionLastData = new EvolutionData();
        return data;
    }

    // load data
    void LoadData()
    {
        var existFile = File.Exists(path);
        if (existFile)
        {
            var json = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
    }

}
