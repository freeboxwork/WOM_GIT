using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Accessibility;

public class StageManager : MonoBehaviour
{
    public StageData stageData;
    public SpriteRenderer rdBg;
    public List<Sprite> bgImages = new List<Sprite>();
    
    void Start()
    {
        
    }


    public IEnumerator Init(int stageId)
    {
        yield return null;

        SetStageData(stageId);
        // SetBgImage();
    }

    public IEnumerator SetStageById(int stageIdx)
    {
        // set data
        SetStageData(stageIdx);

        // set bg image 
        // TODO : Animation 추가
        // SetBgImage();

        yield return null;
    }


    void SetStageData(int stageId)
    {
        var data = GlobalData.instance.dataManager.GetStageDataById(stageId);
        stageData = data.CopyInstance();
    }

    // bg scroll controller 에서 제어 하도록 수정
    void SetBgImage()
    {
        // set texture
        rdBg.sprite = GetBgImgById(stageData.stageId);
    }

    Sprite GetBgImgById(int id)
    {
        return bgImages[id];
    }
}
