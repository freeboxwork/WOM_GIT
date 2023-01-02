using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ��ȭ �� ��ȭ�� ����
/// </summary>

public class EvolutionManager : MonoBehaviour
{
    // current evolution grade id
    public int curEvolGradeDataId = 0;
    public int curEvolGradMonId = 0;
    public AnimationController animContTransition;
    public AnimData animDataTranIn;
    public AnimData animDataTranOut;


    //��ȭ�� ���μ���
    /*
     0 : Ʈ������ ��
     1 : ���� �� �������� ����
     2 : Ʈ������ �ƿ�
     3 : ��ȭ�� ���� ����
     4 : ���� ���
     5 : ���� ��� ���� -> ��ȭ
     6 : ���� ��� ���� -> ���� ���� ����  
    */

    bool isEvolutionGamePlaying = false;

    void Start()
    {
        SetBtnEvents();
    }


    public IEnumerator Init()
    {
        yield return null;             
    }
    
    void SetBtnEvents()
    {
        // ��ȭ�� ��ư
        UtilityMethod.SetBtnEventCustomTypeByID(20, () => {
            if (isEvolutionGamePlaying == false)
                StartCoroutine(EvolutionUpgradeGameStart());
        });

    }

    IEnumerator EvolutionUpgradeGameStart()
    {
        isEvolutionGamePlaying = true;

        yield return null;

        // Ʈ������ ��
        var image = UtilityMethod.GetCustomTypeImageById(20);
        var colorAlpha_None = new Color(1, 1, 1, 1);
        var colorAlpha = new Color(1, 1, 1, 0);
        animContTransition.animData = animDataTranIn;
        yield return StartCoroutine(animContTransition.UI_ImageColorAnim(image, colorAlpha ,colorAlpha_None));

        // UI PANEL ����
        GlobalData.instance.uiController.AllDisableMenuPanels();

        yield return new WaitForSeconds(1f);

        // ���� ���� �ƿ�


        // ������ ����


        // Ʈ������ �ƿ�
        animContTransition.animData = animDataTranOut;
        yield return StartCoroutine(animContTransition.UI_ImageColorAnim(image, colorAlpha_None, colorAlpha));

        // ���� ��
    }


}
