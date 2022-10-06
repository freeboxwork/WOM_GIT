using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;

public class StageManager : MonoBehaviour
{
    public StageData stageData;
    public SpriteRenderer rdBg;

    
    void Start()
    {
        
    }


    public IEnumerator Init(int stageId)
    {
        yield return null;

        SetStageData(stageId);
        SetBgImage();
    }

    void SetStageData(int stageId)
    {
        var data = GlobalData.instance.dataManager.GetStageDataById(stageId);
        stageData = data.CopyInstance();
    }

    void SetBgImage()
    {
        // set texture
    }
}
