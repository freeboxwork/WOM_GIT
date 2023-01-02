using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 진화 및 진화전 관리
/// </summary>

public class EvolutionManager : MonoBehaviour
{
    // current evolution grade id
    public int curEvolGradeDataId = 0;
    public int curEvolGradMonId = 0;
    public AnimationController animContTransition;
    public AnimData animDataTranIn;
    public AnimData animDataTranOut;


    //진화전 프로세스
    /*
     0 : 트랜지션 인
     1 : 몬스터 및 스테이지 세팅
     2 : 트랜지션 아웃
     3 : 진화전 몬스터 등장
     4 : 몬스터 사냥
     5 : 몬스터 사냥 성공 -> 진화
     6 : 몬스터 사냥 실패 -> 이전 몬스터 등장  
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
        // 진화전 버튼
        UtilityMethod.SetBtnEventCustomTypeByID(20, () => {
            if (isEvolutionGamePlaying == false)
                StartCoroutine(EvolutionUpgradeGameStart());
        });

    }

    IEnumerator EvolutionUpgradeGameStart()
    {
        isEvolutionGamePlaying = true;

        yield return null;

        // 트랜지션 인
        var image = UtilityMethod.GetCustomTypeImageById(20);
        
        //yield return StartCoroutine(animContTransition.UI_ImageColorAnim(image,)

        // 데이터 세팅

        // 트랜지션 아웃

        // 몬스터 인
    }


}
