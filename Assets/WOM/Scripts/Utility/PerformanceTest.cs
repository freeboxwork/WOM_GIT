using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PerformanceTest : MonoBehaviour
{
    //public static void RunPerformanceTest()
    //{
    //    int iterations = 1000;
    //    long totalTime = 0L ;

    //    for (int i = 0; i < iterations; i++)
    //    {
    //        string filePath = Application.dataPath + "/test" + i + ".json";

    //        float startTime = Time.realtimeSinceStartup;
    //       // SerializeClassToJson(myClass, filePath);
    //        float endTime = Time.realtimeSinceStartup;

    //        float elapsedTime = endTime - startTime;
    //        totalTime += elapsedTime;
    //    }

    //    float averageTime = totalTime / iterations;
    //    Debug.Log("Average time per iteration: " + averageTime + " seconds.");
    //}

    int iterations = 1000;
    long totalTime = 0L;

    // todo : total save data - data set...
    void SetTotalSaveData()
    {

    }

    // todo : traning save data - data set....
    void SetTraningSaveData()
    {

    }


    // 클래스 10개 분량을 한번에 저장 , 1000 회 반복
    public void RunPerformanceTestSaveDataTotal()
    {
        for (int i = 0; i < iterations; i++)
        {
            string filePath = Application.dataPath + "/test" + i + ".json";
            float startTime = Time.realtimeSinceStartup * 1000f;

            

            //SerializeClassToJson(totalSaveData, filePath);
           

            float endTime = Time.realtimeSinceStartup * 1000f;
            long elapsedTime = (long)(endTime - startTime);
            totalTime += elapsedTime;
        }

        float averageTime = (float)totalTime / iterations;
        Debug.Log("Average time per iteration: " + averageTime.ToString("F2") + " milliseconds.");
    }

    // 클래스 1개 분량을 한번에 저장 , 1000 회 반복
    public void RunPerformanceTestSaveDataElement()
    {
        for (int i = 0; i < iterations; i++)
        {
            string filePath = Application.dataPath + "/test" + i + ".json";
            float startTime = Time.realtimeSinceStartup * 1000f;





            float endTime = Time.realtimeSinceStartup * 1000f;
            long elapsedTime = (long)(endTime - startTime);
            totalTime += elapsedTime;
        }

        float averageTime = (float)totalTime / iterations;
        Debug.Log("Average time per iteration: " + averageTime.ToString("F2") + " milliseconds.");
    }


    public static void SerializeClassToJson<T>(T obj, string filePath)
    {
        string json = JsonUtility.ToJson(obj);
        File.WriteAllText(filePath, json);
    }


}

