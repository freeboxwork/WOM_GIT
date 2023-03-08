using GoogleSheetsToUnity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using WOM.SheetData;

[CustomEditor(typeof(SheetDataMaker))]
public class SheetDataMakerEditor : Editor
{

    SheetDataMaker sheetDataMaker;
    delegate void OnSpreadSheetLoadedWithAction(GstuSpreadSheet ss);

    private void OnEnable()
    {
        sheetDataMaker = (SheetDataMaker)target;
    }

    public override void OnInspectorGUI()
    {
        EditorCustomGUI.GUI_Title("Google Sheet를 기준으로 Data 를 생성 합니다.");

        EditorCustomGUI.GUI_Button("Make Evolution Bee Data", () => {
            UpdateStats(SheetDataGlobalValues.sheetNameEvolutionBee, sheetDataMaker.evolutionDatas, SheetDataGlobalValues.dataPathEvolutionBee, "EvolutionBee", UpdateMethodOne);
        });

        GUILayout.Space(20);

        base.OnInspectorGUI();
    }


    void UpdateStats<T>(string sheetName, List<T> dataList, string datSavePath, string fileName, OnSpreadSheetLoadedWithAction callback, bool mergedCells = false) where T : ScriptableObject, new()
    {
        var search = new GSTU_Search(SheetDataGlobalValues.associatedSheetName, sheetName, "A2", "Z100", "A", 2);
        SpreadsheetManager.ReadPublicSpreadsheet(search, (ss) => {
            callback(ss);
            AppayData(ss, dataList, datSavePath, fileName);
        });
    }

    void UpdateMethodOne(GstuSpreadSheet ss) { }
    void AppayData<T>(GstuSpreadSheet ss, List<T> dataList, string datSavePath, string fileName) where T : ScriptableObject, new()
    {
        if (dataList == null)
        {
            dataList = new List<T>();
        }
        else
        {
            dataList.Clear();
        }

        //skip title row.
        foreach (KeyValuePair<string, int> row in ss.rows.secondaryKeyLink.Skip(1))
        {
            string key = row.Key;

            T data = new T();
            UtilityMethod.UpdateStats(data, ss.rows[key]);
            AssetDatabase.CreateAsset(data, $"{SheetDataGlobalValues.dataRootPath}{datSavePath}/{fileName}_{key}.asset");

            dataList.Add(data);
            EditorUtility.SetDirty(data);
        }

        EditorUtility.SetDirty(target);
    }

}
