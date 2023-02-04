using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static EnumDefinition;

/// <summary>
/// 모든 스탯값을 통합하여 관리함
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

    /// <summary> 곤충 공격력 </summary>
    public float GetInsectDamage(InsectType insectType)
    {
        var itd = GetEvolutionData(insectType).damage;
        var ttd = GetTraningData(SaleStatType.trainingDamage).value;
        var value = itd + ttd;
        return value;
    }

    /// <summary> 곤충 치명타 확율 </summary>
    public float GetInsectCriticalChance(InsectType insectType)
    {
        var trcc = GetTraningData(SaleStatType.trainingCriticalChance).value;
        var tacc = GetTraningData(SaleStatType.talentCriticalChance).value;
        var icc = GetDnaData(DNAType.insectCriticalChance).power;
        var value = trcc + tacc + icc;
        return value;
    }

    /// <summary> 곤충 치명타 공격력 </summary>
    public float GetInsectCriticalDamage(InsectType insectType)
    {
        var trcd = GetTraningData(SaleStatType.trainingCriticalDamage).value;
        var tacd = GetTraningData(SaleStatType.talentCriticalDamage).value;
        var icc = GetDnaData(DNAType.insectCriticalChance).power;
        var value = trcd + tacd + icc;
        return value;
    }

    /// <summary> 곤충 공격력 증가율 </summary>
    public float GetInsectTalentDamage(InsectType insectType)
    {
        var ttd = GetTraningData(SaleStatType.trainingDamage).value;
        var upd = unionManager.GetAllUnionPassiveDamage();
        var did = GetDnaData(DNAType.insectDamage).power;
        var value = ttd + upd + did;
        return value;
    }

    /// <summary> 곤충 이동 속도 </summary>
    public float GetInsectMoveSpeed(InsectType insectType)
    {
        var tms = GetTraningData(SaleStatType.talentMoveSpeed).value;
        var ims = GetDnaData(DNAType.insectMoveSpeed).power;
        var value = tms + ims;
        return value;
    }

    //  클릭하면 나오는 방식인데 스폰 타임 어떻게 적용 되는지?
    /// <summary> 곤충 생성 속도 </summary>
    public float GetInsectSpwanTime(InsectType insectType)
    {
        var tss = GetTraningData(SaleStatType.talentSpawnSpeed).value;
        return tss;
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
