using System.Collections;
using System.Collections.Generic;
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
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        yield return null;
        
        // °ïÃæ »ý¼º
        BirthInsectBullets();


    }

    void SetInsectData(EnumDefinetion.InsectType insectType)
    {
        switch (insectType)
        {
            case EnumDefinetion.InsectType.mentis: break;

            case EnumDefinetion.InsectType.bee: break;

            case EnumDefinetion.InsectType.beetle: break;
        }
    }

    void SetInsectEvolutionData(EvolutionData evolutionData)
    {
        
    }

    void BirthInsectBullets()
    {
        InitancingInsects(prefabInsectBee, insectBullets_Bee);
        InitancingInsects(prefabInsectBeetle, insectBullets_Beetle);
        InitancingInsects(prefabInsectMentis, insectBullets_Mentis);

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



