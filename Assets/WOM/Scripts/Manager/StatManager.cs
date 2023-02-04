using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static EnumDefinition;

/// <summary>
/// ��� ���Ȱ��� �����Ͽ� ������
/// </summary>
public class StatManager : MonoBehaviour
{
    DataManager dataManager;
    EvolutionManager evolutionManager;
    TraningManager traningManager;
    UnionManager unionManager;
    DNAManager dnaManager;

    void Start()
    {
       

    }

    public IEnumerator Init()
    {
        yield return null;
        GetManagers();
    }

    void GetManagers()
    {
        dataManager = GlobalData.instance.dataManager;
        evolutionManager = GlobalData.instance.evolutionManager;
        traningManager = GlobalData.instance.traningManager;
        unionManager = GlobalData.instance.unionManager;
        dnaManager = GlobalData.instance.dnaManger;
    }

    #region INSECT 

    /// <summary> ���� ���ݷ� </summary>
    public float GetInsectDamage(InsectType insectType)
    {
        var itd = GetEvolutionData(insectType).damage;
        var ttd = GetTraningData(SaleStatType.trainingDamage).value;
        var value = itd + ttd;
        return value;
    }

    /// <summary> ���� ġ��Ÿ Ȯ�� </summary>
    public float GetInsectCriticalChance(InsectType insectType)
    {
        var trcc = GetTraningData(SaleStatType.trainingCriticalChance).value;
        var tacc = GetTraningData(SaleStatType.talentCriticalChance).value;
        var icc = GetDnaData(DNAType.insectCriticalChance).power;
        var value = trcc + tacc + icc;
        return value;
    }

    /// <summary> ���� ġ��Ÿ ���ݷ� </summary>
    public float GetInsectCriticalDamage(InsectType insectType)
    {
        var trcd = GetTraningData(SaleStatType.trainingCriticalDamage).value;
        var tacd = GetTraningData(SaleStatType.talentCriticalDamage).value;
        var icc = GetDnaData(DNAType.insectCriticalChance).power;
        var value = trcd + tacd + icc;
        return value;
    }

    /// <summary> ���� ���ݷ� ������ </summary>
    public float GetInsectTalentDamage(InsectType insectType)
    {
        var ttd = GetTraningData(SaleStatType.trainingDamage).value;
        var upd = unionManager.GetAllUnionPassiveDamage();
        var did = GetDnaData(DNAType.insectDamage).power;
        var value = ttd + upd + did;
        return value;
    }

    /// <summary> ���� �̵� �ӵ� </summary>
    public float GetInsectMoveSpeed(InsectType insectType)
    {
        var tms = GetTraningData(SaleStatType.talentMoveSpeed).value;
        var ims = GetDnaData(DNAType.insectMoveSpeed).power;
        var value = tms + ims;
        return value;
    }

    //  Ŭ���ϸ� ������ ����ε� ���� Ÿ�� ��� ���� �Ǵ���?
    /// <summary> ���� ���� �ӵ� </summary>
    public float GetInsectSpwanTime(InsectType insectType)
    {
        var tss = GetTraningData(SaleStatType.talentSpawnSpeed).value;
        return tss;
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


    /*---------------------------------------------------------------------------------------------------------------*/


    #region UTILITY METHOD
    EvolutionData GetEvolutionData(InsectType insectType)
    {
        return dataManager.GetEvolutionDataById(insectType, evolutionManager.evalutionLeveldx);
    }

    TraningInGameData GetTraningData(SaleStatType saleStatType)
    {
        return traningManager.GetTraningInSlotByType(saleStatType).traningInGameData;
    }

    DNAInGameData GetDnaData(DNAType dnaType)
    {
        return dnaManager.GetDNAInGameData(dnaType);
    }

    #endregion

}
