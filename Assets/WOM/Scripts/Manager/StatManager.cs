using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
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
    SkillManager skillManager;

    //SKILL VALUES
    float skill_InsectDamageUp = 0;
    float skill_UnionDamageUp = 0;
    float skill_AllUnitSpeedUp = 0;
    float skill_GoldBounsUp = 0;
    float skill_MonsterKing = 0;
    float skill_AllUnitCriticalChanceUp = 0;

    #region STAT INFOMATION


    #endregion

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
        skillManager = GlobalData.instance.skillManager;
    }

    #region INSECT 

    /// <summary> ���� ���ݷ� </summary>
    public float GetInsectDamage(InsectType insectType)
    {
        var itd = GetEvolutionData(insectType).damage;
        var ttd = GetTraningData(SaleStatType.trainingDamage).value;
        var value = itd + ttd  + skill_InsectDamageUp;
        return value;
    }

    /// <summary> ���� ġ��Ÿ Ȯ�� </summary>
    public float GetInsectCriticalChance(InsectType insectType)
    {
        var trcc = GetTraningData(SaleStatType.trainingCriticalChance).value;
        var tacc = GetTraningData(SaleStatType.talentCriticalChance).value;
        var icc = GetDnaData(DNAType.insectCriticalChance).power;
        var value = trcc + tacc + icc + skill_AllUnitCriticalChanceUp;
        return value;
    }

    /// <summary> ���� ġ��Ÿ ���ݷ� </summary>
    public float GetInsectCriticalDamage(InsectType insectType)
    {
        var trcd = GetTraningData(SaleStatType.trainingCriticalDamage).value;
        var tacd = GetTraningData(SaleStatType.talentCriticalDamage).value;
        var icc = GetDnaData(DNAType.insectCriticalDamage).power;
        var value = trcd + tacd + icc;
        return value;
    }

    /// <summary> ���� ���ݷ� ������ </summary>
    public float GetInsectTalentDamage(InsectType insectType)
    {
        var ttd = GetTraningData(SaleStatType.talentDamage).value;
        var upd = unionManager.GetAllUnionPassiveDamage();
        var did = GetDnaData(DNAType.insectDamage).power;
        var idr = GetEvolutionData(insectType).damageRate;
        var value = ttd + upd + did + idr;
        return value;
    }

    /// <summary> ���� �̵� �ӵ� </summary>
    public float GetInsectMoveSpeed(InsectType insectType)
    {
        var ies = GetEvolutionData(insectType).speed;
        var tms = GetTraningData(SaleStatType.talentMoveSpeed).value;
        var ims = GetDnaData(DNAType.insectMoveSpeed).power;
        var value = ies + (ies * (tms + ims + skill_AllUnitSpeedUp));
        return value;
    }
    
    /// <summary> ���� ���� �ӵ� </summary>
    public float GetInsectSpwanTime(InsectType insectType)
    {
        var ist = GetEvolutionData(insectType).spawnTime;
        var tst = GetTraningData(SaleStatType.talentSpawnSpeed).value;
        var value = ist - (ist * tst);
        return value;
    }

    #endregion


    /*---------------------------------------------------------------------------------------------------------------*/


    #region UNION

    /// <summary> ���Ͽ� ���ݷ� </summary>
    public float GetUnionDamage(int unionIndex)
    {
        var ud = GetUnionData(unionIndex).damage + skill_UnionDamageUp;
        return ud;
    }

    /// <summary> ���Ͽ� �̵��ӵ� </summary>
    public float GetUnionMoveSpeed(int unionIndex)
    {
        var ums = GetUnionData(unionIndex).moveSpeed;
        var dms = GetDnaData(DNAType.insectMoveSpeed).power;
        var value = ums + dms + skill_AllUnitSpeedUp;
        return value * 0.01f;
    }

    /// <summary> ���Ͽ� �����ӵ� </summary>
    public float GetUnionSpwanSpeed(int unionIndex)
    {
        var dst = GetDnaData(DNAType.unionSpawnTime).power; 
        return dst;
    }

    /// <summary> ���Ͽ� ���ݷ� ������ </summary>
    public float GetUnionTalentDamage(int unionIndex)
    {
        var dud = GetDnaData(DNAType.unionDamage).power;
        return dud;
    }

    #endregion


    /*---------------------------------------------------------------------------------------------------------------*/


    #region GOODS

    /// <summary> ��� ȹ�淮 </summary>
    public float GetTalentGoldBonus()
    {
        var dgb = GetDnaData(DNAType.glodBonus).power;
        var tgb = GetTraningData(SaleStatType.talentGoldBonus).value;
        var value = dgb + tgb + skill_GoldBounsUp;
        return value;
    }

    #endregion


    /*---------------------------------------------------------------------------------------------------------------*/


    #region SKILLS

    
    public void UsingSkill(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.insectDamageUp:
                StartCoroutine(EnableSkill_InsectDamageUP());
                break;
            case SkillType.unionDamageUp:
                StartCoroutine(EnableSkill_UnionDamageUP());
                break;
            case SkillType.allUnitSpeedUp:
                StartCoroutine(EnableSkill_AllUnitSpeedUP());
                break;
            case SkillType.glodBonusUp:
                StartCoroutine(EnableSkill_GoldBonusUP()); 
                break;
            case SkillType.monsterKing:
                StartCoroutine(EnableSkill_MonsterKing()); 
                break;
            case SkillType.allUnitCriticalChanceUp:
                StartCoroutine(EnableSkill_AllUnitCriticalChanceUP());
                break;
        }
    }


    public IEnumerator EnableSkill_InsectDamageUP() 
    {
        var data = GetSkillData(SkillType.insectDamageUp);
        skill_InsectDamageUp = data.damage;
        yield return new WaitForSeconds(data.duaration);
        skill_InsectDamageUp = 0;
    }

    public IEnumerator EnableSkill_UnionDamageUP()
    {
        var data = GetSkillData(SkillType.unionDamageUp);
        skill_UnionDamageUp = data.damage;
        yield return new WaitForSeconds(data.duaration);
        skill_UnionDamageUp = 0;
    }

    public IEnumerator EnableSkill_AllUnitSpeedUP()
    {
        var data = GetSkillData(SkillType.allUnitSpeedUp);
        skill_AllUnitSpeedUp = data.power;
        yield return new WaitForSeconds(data.duaration);
        skill_UnionDamageUp = 0;
    }

    public IEnumerator EnableSkill_GoldBonusUP()
    {
        var data = GetSkillData(SkillType.glodBonusUp);
        skill_GoldBounsUp = data.power;
        yield return new WaitForSeconds(data.duaration);
        skill_GoldBounsUp = 0;
    }

    public IEnumerator EnableSkill_MonsterKing()
    {
        var data = GetSkillData(SkillType.monsterKing);
        skill_MonsterKing = data.power;
        yield return new WaitForSeconds(data.duaration);
        skill_MonsterKing = 0;
    }

    public IEnumerator EnableSkill_AllUnitCriticalChanceUP()
    {
        var data = GetSkillData(SkillType.allUnitCriticalChanceUp);
        skill_AllUnitCriticalChanceUp = data.power;
        yield return new WaitForSeconds(data.duaration);
        skill_AllUnitCriticalChanceUp = 0;
    }


    #endregion


    /*---------------------------------------------------------------------------------------------------------------*/


    #region ���� DATA

    public float GoldPig()
    {
        return GetDnaData(DNAType.goldPig).power;
    }

    public float SkillDuration()
    {
        return GetDnaData(DNAType.skillDuration).power;
    }

    public float SkillCoolTime()
    {
        return GetDnaData(DNAType.skillCoolTime).power;
    }

    public float BossDamage()
    {
        return GetDnaData(DNAType.bossDamage).power;
    }

    public float MonsterHpLess()
    {
        return GetDnaData(DNAType.monsterHpLess).power;
    }

    public float BoneBonus()
    {
        return GetDnaData(DNAType.boneBonus).power;
    }

    public float GoldMonsterBonus()
    {
        return GetDnaData(DNAType.goldMonsterBonus).power;
    }

    public float OfflineBonus()
    {
        return GetDnaData(DNAType.offlineBonus).power;
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
        return traningManager.GetTraningInGameData(saleStatType);
    }

    DNAInGameData GetDnaData(DNAType dnaType)
    {
        return dnaManager.GetDNAInGameData(dnaType);
    }

    UnionInGameData GetUnionData(int unionIndex)
    {
        return unionManager.GetUnionInGameDataByID(unionIndex);
    }
    Skill_InGameData GetSkillData(SkillType skillType)
    {
        return skillManager.GetSkillInGameDataByType(skillType);
    }


    #endregion

}
