using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumDefinition;

/// <summary>
/// 모든 스탯값을 통합하여 관리함
/// </summary>
public class StatManager : MonoBehaviour
{

    void Start()
    {

    }

    public IEnumerator Init()
    {
        yield return null;
    }

    #region INSECT 

    /// <summary> 곤충 공격력 </summary>
    public float GetInsectDamage(InsectType insectType)
    {
        return 0;
    }

    /// <summary> 곤충 치명타 확율 </summary>
    public float GetInsectCriticalChance(InsectType insectType)
    {
        return 0;
    }

    /// <summary> 곤충 치명타 공격력 </summary>
    public float GetInsectCriticalDamage(InsectType insectType)
    {
        return 0;
    }

    /// <summary> 곤충 공격력 증가율 </summary>
    public float GetInsectTalentDamage(InsectType insectType)
    {
        return 0;
    }

    /// <summary> 곤충 이동 속도 </summary>
    public float GetInsectMoveSpeed(InsectType insectType)
    {
        return 0;
    }

    //  클릭하면 나오는 방식인데 스폰 타임 어떻게 적용 되는지?
    /// <summary> 곤충 생성 속도 </summary>
    public float GetInsectSpwanTime(InsectType insectType)
    {
        return 0;
    }

    #endregion


    /*---------------------------------------------------------------------------------------------------------------*/


    #region UNION

    /// <summary> 유니온 공격력 </summary>
    public float GetUnionDamage(int unionIndex)
    {
        return 0;
    }

    /// <summary> 유니온 이동속도 </summary>
    public float GetUnionMoveSpeed(int unionIndex)
    {
        return 0;
    }

    /// <summary> 유니온 생성속도 </summary>
    public float GetUnionSpwanSpeed(int unionIndex)
    {
        return 0;
    }

    /// <summary> 유니온 공격력 증가율 </summary>
    public float GetUnionTalentDamage(int unionIndex)
    {
        return 0;
    }

    #endregion


    /*---------------------------------------------------------------------------------------------------------------*/


    #region GOODS

    /// <summary> 골드 획득량 </summary>
    public float GetTalentGoldBonus()
    {
        return 0;
    }

    #endregion


    /*---------------------------------------------------------------------------------------------------------------*/


    #region SKILLS

    public IEnumerator EnableSkill_InsectDamageUP() 
    {
        yield return null;
    }

    public IEnumerator EnableSkill_UnionDamageUP()
    {
        yield return null;
    }

    public IEnumerator EnableSkill_AllUnitSpeedUP()
    {
        yield return null;
    }

    public IEnumerator EnableSkill_GoldBonusUP()
    {
        yield return null;
    }

    public IEnumerator EnableSkill_MonsterKing()
    {
        yield return null;
    }

    public IEnumerator EnableSkill_AllUnitCriticalChanceUP()
    {
        yield return null;
    }


    #endregion


    /*---------------------------------------------------------------------------------------------------------------*/


    #region 개별 DATA

    public void GoldPig()
    {

    }

    public void SkillDuration()
    {

    }

    public void SkillCoolTime()
    {

    }

    public void BossDamage()
    {

    }

    public void MonsterHpLess()
    {

    }

    public void BoneBonus()
    {

    }

    public void GoldMonsterBonus()
    {

    }

    public void OfflineBonus()
    {

    }


    #endregion
}
