using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumDefinition;

/// <summary>
/// ��� ���Ȱ��� �����Ͽ� ������
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

    /// <summary> ���� ���ݷ� </summary>
    public float GetInsectDamage(InsectType insectType)
    {
        return 0;
    }

    /// <summary> ���� ġ��Ÿ Ȯ�� </summary>
    public float GetInsectCriticalChance(InsectType insectType)
    {
        return 0;
    }

    /// <summary> ���� ġ��Ÿ ���ݷ� </summary>
    public float GetInsectCriticalDamage(InsectType insectType)
    {
        return 0;
    }

    /// <summary> ���� ���ݷ� ������ </summary>
    public float GetInsectTalentDamage(InsectType insectType)
    {
        return 0;
    }

    /// <summary> ���� �̵� �ӵ� </summary>
    public float GetInsectMoveSpeed(InsectType insectType)
    {
        return 0;
    }

    //  Ŭ���ϸ� ������ ����ε� ���� Ÿ�� ��� ���� �Ǵ���?
    /// <summary> ���� ���� �ӵ� </summary>
    public float GetInsectSpwanTime(InsectType insectType)
    {
        return 0;
    }

    #endregion


    /*---------------------------------------------------------------------------------------------------------------*/


    #region UNION

    /// <summary> ���Ͽ� ���ݷ� </summary>
    public float GetUnionDamage(int unionIndex)
    {
        return 0;
    }

    /// <summary> ���Ͽ� �̵��ӵ� </summary>
    public float GetUnionMoveSpeed(int unionIndex)
    {
        return 0;
    }

    /// <summary> ���Ͽ� �����ӵ� </summary>
    public float GetUnionSpwanSpeed(int unionIndex)
    {
        return 0;
    }

    /// <summary> ���Ͽ� ���ݷ� ������ </summary>
    public float GetUnionTalentDamage(int unionIndex)
    {
        return 0;
    }

    #endregion


    /*---------------------------------------------------------------------------------------------------------------*/


    #region GOODS

    /// <summary> ��� ȹ�淮 </summary>
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


    #region ���� DATA

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
