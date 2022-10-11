using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

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

    void Start()
    {
        //StartCoroutine(Init());
    }

    public IEnumerator Init(PlayerDataManager playerDataManager)
    {
        // 곤충 생성
        BirthInsectBullets();

        // 곤충 인스턴스 생성
        SetInsectData(EnumDefinition.InsectType.bee, playerDataManager.saveData.beeSaveData.evolutionLastData);

        // add insects list 
        // 순서 : mentis, bee, beetle
        insects.Add(insectMentis);
        insects.Add(insectBee);
        insects.Add(insectBeetle);

        yield return new WaitForEndOfFrame();
    }

    /// <summary> 몬스터 제거시 하프라인 위의 곤충들 제거 </summary>
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

    // 공격력 공식 : (damage+ (damage* damageRate)) x (1+ criticalChance x ( 2 + criticalDamage -1))
    /// <summary> 계산된 곤충 데미지 값 </summary>
    public float GetInsectDamage(EnumDefinition.InsectType insectType)
    {
        var insect = GetInsect(insectType);
        return 1f;// (insect.damage + (insect.damage * insect.damageRate)) * (1 + insect.criticalChance * (2 + insect.criticalDamage - 1));
    }



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
            //TODO 모든 오브젝트 ENABLE 상태일때 새로운 BULLET 추가 

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



