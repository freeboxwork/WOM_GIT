using System.Collections;
using System.Collections.Generic;
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
        
        //yield return StartCoroutine(animContTransition.UI_ImageColorAnim(image,)

        // ������ ����

        // Ʈ������ �ƿ�

        // ���� ��
    }


}
