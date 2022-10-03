using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{

    public  static GlobalData instance;

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

    // Update is called once per frame
    void Update()
    {
        
    }

}
