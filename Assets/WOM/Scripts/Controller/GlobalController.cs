using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{

    public DataManager dataManager;
    void Start()
    {
        if (dataManager == null) dataManager = FindObjectOfType<DataManager>();
        StartCoroutine(Init());
    }
   
    IEnumerator Init()
    {
        // set data
        yield return StartCoroutine(dataManager.SetDatas());
    }

}
