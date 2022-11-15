using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EnumDefinition;

public class InsectManager : MonoBehaviour
{
    
    public InsectBee insectBee;
    public InsectBeetle insectBeetle;
    public InsectMentis insectMentis;

    public InsectBullet prefabInsectBee;
    public InsectBullet prefabInsectBeetle;
    public InsectBullet prefabInsectMentis;

    // using object pooling...
    public List<InsectBullet> insectBullets_Bee;
    public List<InsectBullet> insectBullets_Beetle;
    public List<InsectBullet> insectBullets_Mentis;

    public Transform tr_insectPool;
    public int insectBirthCount = 15;

    public List<InsectBase> insects = new List<InsectBase>();

    public Transform trHalfPoint;

    public bool damageDebug = false;
    public float debugDamage = 20;
    Player player;
    void Start()
    {

        player = GlobalData.instance.player;
        //StartCoroutine(Init());
    }

    public IEnumerator Init(PlayerDataManager playerDataManager)
    {
        // ���� ����
        BirthInsectBullets();

        // ���� �ν��Ͻ� ����
        SetInsectData(EnumDefinition.InsectType.bee, playerDataManager.saveData.beeSaveData.evolutionLastData);
        SetInsectData(EnumDefinition.InsectType.beetle, playerDataManager.saveData.beetleSaveData.evolutionLastData);
        SetInsectData(EnumDefinition.InsectType.mentis, playerDataManager.saveData.mentisSaveData.evolutionLastData);


        // add insects list 
        // ���� : mentis, bee, beetle
        insects.Add(insectMentis);
        insects.Add(insectBee);
        insects.Add(insectBeetle);

        yield return new WaitForEndOfFrame();
    }


    public void SetAllInsectData(StageData stageData)
    {
        // ��ȭ ���� �ʿ�    
    }

    EvolutionData GetInsectEvolDataById(EnumDefinition.InsectType insectType, int idx)
    {
        return GlobalData.instance.dataManager.GetEvolutionDataById(insectType, idx);
    }


    /// <summary> ���� ���Ž� �������� ���� ����� ���� </summary>
    public void DisableHalfLineInsects()
    {
        DisableInsects(insectBullets_Bee);
        DisableInsects(insectBullets_Beetle);
        DisableInsects(insectBullets_Mentis);
    }

    bool IsHalfPointUpSide(InsectBullet insectBullet)
    {
        return insectBullet.transform.position.y > trHalfPoint.position.y;
    }

    public void DisableInsects(List<InsectBullet> insectBullets)
    {
        foreach (var insect in insectBullets)
            if (insect.gameObject.activeSelf)
                if(IsHalfPointUpSide(insect))
                    insect.DisableInsect();
    }

    public InsectBase GetInsect(EnumDefinition.InsectType insectType)
    {
        return insects[(int)insectType];
    }


    /// <summary> ���� ���� ������ �� </summary>
    public float GetInsectDamage(EnumDefinition.InsectType insectType)
    {
        var insect = GetInsect(insectType);
        var damage = GetInsectDamage(insect);
        if (HasCriticalDamage(insect)) // ũ��Ƽ�� ������ ��������
        {
            damage = damage * GetInsectCriticalDamage(insect);
        }
        if (damageDebug) return debugDamage;
        return damage;
    }
    // ---------- �Ʒ� ���׷��̵� ����
    float GetInsectDamage(InsectBase insect)
    {
        // ���ݷ� ���� : (damage+ (damage* damageRate))
        //return insect.damage + (insect.damage * insect.damageRate);
        return (insect.damage + player.GetStatValue(SaleStatType.trainingDamage)) * (insect.damageRate + player.GetStatValue(SaleStatType.talentDamage));
    }

    // ġ��Ÿ ������ ���
    float GetInsectCriticalDamage(InsectBase insect)
    {
        //return 2 + insect.criticalDamage;
        return 2 + insect.criticalDamage + player.GetStatValue(SaleStatType.trainingCriticalChance) + player.GetStatValue(SaleStatType.talentCriticalChance) ;

    }
    // ũ��Ƽ�� �������� ������ �ִ���? ( ũ��Ƽ�� ������ī �������� )
    bool HasCriticalDamage( InsectBase insect )
    {
        //var percentage = 1+insect.criticalChance;
        var percentage = 1+insect.criticalChance + player.GetStatValue(SaleStatType.trainingCriticalDamage) + player.GetStatValue(SaleStatType.talentCriticalDamage);
        var randomValue = Random.Range(0f, 100f);
        return randomValue <= percentage;
    }

    ///talentSpawnSpeed ��ġ������ ������ ��ȯ�� ��� �ʿ��� ���� ���� ������ ������ �׽�Ʈ �� �ʿ信 ���� ���� �� ����
    ///talentMoveSpeed ���� �����Ǿ� �ִ� ������ ���ǵ尡 Max. Max Speed�� = 500, Min Speed�� = 0, InsectBullet.AttackAnim speed ���� �ʿ��ϸ� EvolutionData Speed�� ���� ���Եǰ� + Player.GetStatValue(SaleStatType.trainingMoveSpeed) �� ��
    ///talentGoldBonus ��ȭ�� ȹ���ϴ� ���� üũ EventController.GainGold -> Player.AddGold 

    //---------- �Ʒ� ���׷��̵� ��


    public void SetInsectData(EnumDefinition.InsectType insectType, EvolutionData evolutionData)
    {
        switch (insectType)
        {
            case EnumDefinition.InsectType.mentis:
                insectMentis.insectType = insectType;
                SetInsectEvolutionData(insectMentis, evolutionData);
                break;
            case EnumDefinition.InsectType.bee:
                insectBee.insectType = insectType;
                SetInsectEvolutionData(insectBee, evolutionData);
                break;
            case EnumDefinition.InsectType.beetle:
                insectBeetle.insectType = insectType;
                SetInsectEvolutionData(insectBeetle, evolutionData);
                break;
        }
    }

    void SetInsectEvolutionData(InsectBase insectBase , EvolutionData evolutionData)
    {
        insectBase.name = evolutionData.name;
        insectBase.damage = evolutionData.damage;
        insectBee.damageRate = evolutionData.damageRate;
        insectBee.criticalChance = evolutionData.criticalChance;
        insectBee.criticalDamage = evolutionData.criticalDamage;
        insectBee.speed = evolutionData.speed;
        insectBee.goldBonus = evolutionData.goldBonus;
        insectBee.bossDamage = evolutionData.bossDamage;
    }

    void BirthInsectBullets()
    {
        InitancingInsects(prefabInsectBee, insectBullets_Bee);
        InitancingInsects(prefabInsectBeetle, insectBullets_Beetle);
        InitancingInsects(prefabInsectMentis, insectBullets_Mentis);
    }

    // pooling system
    public void EnableBullet(EnumDefinition.InsectType insectType, Vector2 targetPos)
    {
        var bullets = GetBulletsByInsectType(insectType);
        var bullet = bullets.FirstOrDefault(f => !f.gameObject.activeSelf);
        if(bullet != null)
        {
            bullet.transform.position = targetPos;
            bullet.gameObject.SetActive(true);
        }
        else
        {
            //TODO ��� ������Ʈ ENABLE �����϶� ���ο� BULLET �߰� 

        }
    }

    List<InsectBullet> GetBulletsByInsectType(EnumDefinition.InsectType insectType)
    {
        switch (insectType)
        {
            case EnumDefinition.InsectType.mentis: return insectBullets_Mentis;
            case EnumDefinition.InsectType.bee: return insectBullets_Bee;
            case EnumDefinition.InsectType.beetle: return insectBullets_Beetle; 
        }
        return null;
    }


    void InitancingInsects(InsectBullet prefab, List<InsectBullet> bullets)
    {
        for (int i = 0; i < insectBirthCount; i++)
        {
            var bullet = Instantiate(prefab,tr_insectPool);
            bullets.Add(bullet);
            bullet.gameObject.SetActive(false);
        }
    }
  
}



