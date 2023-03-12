using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Coffee.UIExtensions;

public class TutorialManager : MonoBehaviour
{
    public TextAsset tutorialJsonData;

    // ���丮�� ������
    public TutorialStepDatas tutorialStepData;

    // setId �������� ���� ���丮�� ��Ʈ ������
    public List<TutorialStepSetData> tutorialStepSetDatas = new List<TutorialStepSetData>();

    // ���丮�� ��ư��
    public List<TutorialButton> tutorialButtons = new List<TutorialButton>();

    // ���� �������� ���丮�� ��Ʈ�� ���̵�
    public int curTutorialSetID = 0;
    // ���� �������� ���丮�� ��Ʈ�� ���� ���̵�
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
        // set set data ( setId �������� ��Ʈ ����Ʈ�� ���� )
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