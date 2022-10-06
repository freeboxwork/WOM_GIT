using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InsectManager : MonoBehaviour
{
    
    public InsectBee insectBee;
    public InsectBeetle insectBeetle;
    public InsectMentis insectMentis;

    public InsectBullet prefabInsectBee;
    public InsectBullet prefabInsectBeetle;
    public InsectBullet prefabInsectMentis;

    public List<InsectBullet> insectBullets_Bee;
    public List<InsectBullet> insectBullets_Beetle;
    public List<InsectBullet> insectBullets_Mentis;

    public Transform tr_insectPool;
    public int insectBirthCount = 15;

   
    void Start()
    {
        //StartCoroutine(Init());
    }

    public IEnumerator Init(PlayerDataManager playerDataManager)
    {
        yield return null;

        // 곤충 생성
        BirthInsectBullets();

        // 곤충 인스턴스 생성
        SetInsectData(EnumDefinetion.InsectType.bee, playerDataManager.saveData.beeSaveData.evolutionLastData);

    }

    void SetInsectData(EnumDefinetion.InsectType insectType, EvolutionData evolutionData)
    {
        switch (insectType)
        {
            case EnumDefinetion.InsectType.mentis:
                insectMentis.insectType = insectType;
                SetInsectEvolutionData(insectMentis, evolutionData);
                break;
            case EnumDefinetion.InsectType.bee:
                insectBee.insectType = insectType;
                SetInsectEvolutionData(insectBee, evolutionData);
                break;
            case EnumDefinetion.InsectType.beetle:
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
        insectBee.ciriticalDamage = evolutionData.criticalDamage;
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
    public void EnableBullet(EnumDefinetion.InsectType insectType)
    {
        var bullets = GetBulletsByInsectType(insectType);
        var bullet = bullets.FirstOrDefault(f => !f.gameObject.activeSelf);
        if(bullet != null)
            bullet.gameObject.SetActive(true);
        else
        {
            //TODO 모든 오브젝트 ENABLE 상태일때 새로운 BULLET 추가 

        }
    }

    List<InsectBullet> GetBulletsByInsectType(EnumDefinetion.InsectType insectType)
    {
        switch (insectType)
        {
            case EnumDefinetion.InsectType.mentis: return insectBullets_Mentis;
            case EnumDefinetion.InsectType.bee: return insectBullets_Bee;
            case EnumDefinetion.InsectType.beetle: return insectBullets_Beetle; 
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



