using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.Events;

public class GambleManager : MonoBehaviour
{
    public int curSummonGrade = 0; // ��ȯ ��� ������ 
    public int roundCount = 0;     // �̱� �õ� ī��Ʈ


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }


    IEnumerator Gaembeulling(int gambleCount, UnityAction endEvent)
    {

        for (int i = 0; i < gambleCount; i++)
        {



            yield return null;  
        }

        endEvent.Invoke();
    }
}
