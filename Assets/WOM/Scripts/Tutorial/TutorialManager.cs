using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Coffee.UIExtensions;

public class TutorialManager : MonoBehaviour
{
    public TextAsset tutorialJsonData;

    // 투토리얼 데이터
    public TutorialStepDatas tutorialStepData;

    // setId 기준으로 나눈 투토리얼 세트 데이터
    public List<TutorialStepSetData> tutorialStepSetDatas = new List<TutorialStepSetData>();

    // 투토리얼 버튼들
    public List<TutorialButton> tutorialButtons = new List<TutorialButton>();

    // 현재 진행중인 투토리얼 세트의 아이디
    public int curTutorialSetID = 0;
    // 현재 진행중인 투토리얼 세트의 스텝 아이디
    public int curTutorialStepID = 0;

    public TutorialUiController tutorialUiCont;

    void Start()
    {
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        // get json data
        tutorialStepData = JsonUtility.FromJson<TutorialStepDatas>(tutorialJsonData.text);

        yield return new WaitForEndOfFrame();

        SetData();
        yield return null;
    }

    void SetData()
    {
        // set set data ( setId 기준으로 세트 리스트를 만듦 )
        for (int i = 0; i < tutorialStepData.data.Count; i++)
        {
            var data = tutorialStepData.data[i];
            if(!tutorialStepSetDatas.Any(a=> a.setId == data.setId))
            {
                tutorialStepSetDatas.Add(new TutorialStepSetData
                {
                    setId = data.setId,
                    steps = new List<TutorialStep> { data }
                });
            }
            else
            {
                tutorialStepSetDatas.First(s => s.setId == data.setId).steps.Add(data);
            }
        }
    }


    TutorialStepSetData GetTutorialSetById(int setID)
    {
        return tutorialStepSetDatas.FirstOrDefault(f => f.setId == setID);
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EnableTutorialSet();
        }
    }

    public void EnableTutorialSet()
    {
        var tutorialSet = GetTutorialSetById(curTutorialSetID);   
        var step = tutorialSet.steps[curTutorialStepID];
        var buttonId = GetTutorialButtonById(step.tutorialBtnId);
        tutorialUiCont.EnableTutorial(step.description, buttonId.image);
    }

    TutorialButton GetTutorialButtonById(int id)
    {
        return tutorialButtons.FirstOrDefault(f => f.id == id);
    }


}

[System.Serializable]
public class TutorialStepDatas
{
    public List<TutorialStep> data = new List<TutorialStep>();
}

[System.Serializable]
public class TutorialStepSetData
{
    public int setId;
    public bool isSetComplete = false;
    public List<TutorialStep> steps = new List<TutorialStep>();
}