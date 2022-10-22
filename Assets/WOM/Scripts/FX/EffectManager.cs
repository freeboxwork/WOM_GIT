using ProjectGraphics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngineInternal;

public class EffectManager : MonoBehaviour
{
    [Header("곤충 이펙트 관련 항목")]
    public int insectDisableEffectsBirthCount = 25;
    public int insectAttackEffectsBrithCount = 10;
    public ParticleSystem prefabInsectDisableEff;
    public List<ParticleSystem> insectDisableEffects = new List<ParticleSystem>();

    /// <summary> 보스 공격 했을때 나타나는 파티클 이펙트 </summary>
    // mentis , bee , beetle 
    public List<ParticleRoate> prefabInsectAttackEff = new List<ParticleRoate>();
    public List<ParticleRoate> insectAttackEffMentis = new List<ParticleRoate>();
    public List<ParticleRoate> insectAttackEffBee = new List<ParticleRoate>();
    public List<ParticleRoate> insectAttackEffBeetle = new List<ParticleRoate>();
    [Header("=====================================================================================================================")]


    [Header("이펙트들 부모 게임 오브젝트")]
    // 프리팹 생성될 부모 모드젝트
    public Transform trEffects;
    [Header("=====================================================================================================================")]
    
    [Header("골드 관련 항목")]
    // 골드 풀링 카운트
    public int goldPoolCount = 25;
    // 골드 프리팹
    public GoldAnimController prefabGoldAnimCont;
    // 골드 생성 포인트들
    public List<Transform> goldSFX_RandomPoints = new List<Transform>();
    // 골드 오브젝트 풀
    public List<GoldAnimController> goldAnimConts = new List<GoldAnimController>();
    // 활성화된 골드 오브젝트들
    List<GoldAnimController> enableGoldAnimConts = new List<GoldAnimController>();
   




    void Start()
    {

    }



    public IEnumerator Init()
    {
        yield return null;

        // disable effect
        CreateInsectDisableEffects();

        // attack effect mentis
        CreateInstanceAttackEffects(prefabInsectAttackEff[(int)EnumDefinition.InsectType.mentis], insectAttackEffMentis);
        // attack effect bee
        CreateInstanceAttackEffects(prefabInsectAttackEff[(int)EnumDefinition.InsectType.bee], insectAttackEffBee);
        // attack effect beetle
        CreateInstanceAttackEffects(prefabInsectAttackEff[(int)EnumDefinition.InsectType.beetle], insectAttackEffBeetle);

        // 골드 풀링 오브젝트 생성
        CreateInstanceGolds();
    }

    // 골드 생성 ( 풀링 오브젝트 ) 
    void CreateInstanceGolds()
    {
        for (int i = 0; i < goldPoolCount; i++)
        {
            var gold = Instantiate(prefabGoldAnimCont, trEffects);
            goldAnimConts.Add(gold);
            gold.gameObject.SetActive(false);
        }
    }

    public IEnumerator EnableGoldEffects(int enableCount)
    {
        StopAllCoroutines();
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < enableCount; i++)
        {
            var gold = GetDisableGold();
            if(gold != null)
            {
                gold.gameObject.SetActive(true);
                gold.GoldInAnimStart();
                enableGoldAnimConts.Add(gold);
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                Debug.Log("Gold Pool 이 비어 있습니다.");
            }
            
        }
    }

    public IEnumerator DisableGoldEffects()
    {
        for (int i = 0; i < enableGoldAnimConts.Count; i++)
        {
            var gold = enableGoldAnimConts[i];
            if (gold.gameObject.activeSelf)
            {
                gold.GoldOutAnimStart(() => {
                    // 골드가 UI 쪽으로 이동후 개별 이벤트 실행
                    //Debug.Log("gold goal event!!");
                });

                yield return new WaitForSeconds(0.05f);
            }
          
        }
        enableGoldAnimConts.Clear();
    }


    void CreateInstanceAttackEffects(ParticleRoate prefabParticle , List<ParticleRoate> list )
    {
        for (int i = 0; i < insectAttackEffectsBrithCount; i++)
        {
            var effect = Instantiate(prefabParticle, trEffects);
            list.Add(effect);
            effect.gameObject.SetActive(false);
        }
    }


    void CreateInsectDisableEffects()
    {
        for (int i = 0; i < insectDisableEffectsBirthCount; i++)
        {
            var effect = Instantiate(prefabInsectDisableEff, trEffects);
            insectDisableEffects.Add(effect);
            effect.gameObject.SetActive(false);
        }
    }

    public void EnableInsectDisableEffect(Transform tr)
    {
        var effect = GetInsectDisableEff();
        effect.transform.position = tr.transform.position;
        effect.gameObject.SetActive(true);
    }

    public void EnableAttackEffectByInsectType(EnumDefinition.InsectType insectType, Transform tr)
    {
        switch (insectType)
        {
            case EnumDefinition.InsectType.mentis: EnableAttackEffect(insectAttackEffMentis, tr); break;
            case EnumDefinition.InsectType.bee:    EnableAttackEffect(insectAttackEffBee, tr);    break;
            case EnumDefinition.InsectType.beetle: EnableAttackEffect(insectAttackEffBeetle, tr); break;
        }
    }
        
    void EnableAttackEffect(List<ParticleRoate> particles ,Transform tr)
    {
        var effect = GetDisableParticleRoate(particles);
        var zRot = tr.eulerAngles.z;
        effect.transform.position = tr.transform.position;
        effect.SetParticleRotateDirection(zRot);
        effect.gameObject.SetActive(true);
    }

    GoldAnimController GetDisableGold()
    {
        var obj = goldAnimConts?.FirstOrDefault(f => !f.gameObject.activeSelf);
        if (obj != null) return obj;
        else return null;
        //return goldAnimConts.FirstOrDefault(f => !f.gameObject.activeSelf);
    }

    ParticleRoate GetDisableParticleRoate(List<ParticleRoate> particles)
    {
        return particles.FirstOrDefault(f => !f.gameObject.activeSelf);
    }

    ParticleSystem GetInsectDisableEff()
    {
        return insectDisableEffects.FirstOrDefault(f => !f.gameObject.activeSelf);
    }

    // 골드 생성 포인트
    public Transform GetGoldSfxRandomPoint(EnumDefinition.GoldPosType pointType)
    {
        return goldSFX_RandomPoints[(int)pointType];
    }

}
