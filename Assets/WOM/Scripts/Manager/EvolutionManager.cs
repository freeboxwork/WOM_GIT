using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ��ȭ �� ��ȭ�� ���� - ������� ����
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
    
    




}
