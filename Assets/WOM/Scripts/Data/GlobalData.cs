using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{

    public  static GlobalData instance;
    public DataManager dataManager;
    private void Awake()
    {
        SetInstance();
    }

    void Start()
    {
        
    }

    void SetInstance()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }

   

}
