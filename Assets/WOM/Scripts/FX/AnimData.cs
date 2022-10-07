using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimData : MonoBehaviour
{
    public EnumDefinition.AnimCurveType animCurveType;

    //JH - 스크립트에는 animDuration 2f로 설정되어 있었는데 Inspector 창에는 0.5f로 입력되어 있어서 어차피 0.5f로 적용되는 것 확인하고 수정했습니다
    public float animDuration = 0.5f;
    public float value;
    public float animTime;
    public float animValue;
    public float animStartTime;

    public void ResetAnimData()
    {
        value = 0;
        animTime = 0f;
        animValue = 0f;
        animStartTime = Time.time;
    }

}
