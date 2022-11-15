using ProjectGraphics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngineInternal;

public class EffectManager : MonoBehaviour
{
    [Header("���� ����Ʈ ���� �׸�")]
    public int insectDisableEffectsBirthCount = 25;
    public int insectAttackEffectsBrithCount = 10;
    public ParticleSystem prefabInsectDisableEff;
    public List<ParticleSystem> insectDisableEffects = new List<ParticleSystem>();

    /// <summary> ���� ���� ������ ��Ÿ���� ��ƼŬ ����Ʈ </summary>
    // mentis , bee , beetle 
    public List<ParticleRoate> prefabInsectAttackEff = new List<ParticleRoate>();
    public List<ParticleRoate> insectAttackEffMentis = new List<ParticleRoate>();
    public List<ParticleRoate> insectAttackEffBee = new List<ParticleRoate>();
    public List<ParticleRoate> insectAttackEffBeetle = new List<ParticleRoate>();
    [Header("=====================================================================================================================")]


    [Header("����Ʈ�� �θ� ���� ������Ʈ")]
    // ������ ������ �θ� �����Ʈ
    public Transform trEffects;
    [Header("=====================================================================================================================")]
    

    [Header("��� ���� �׸�")]
    // ��� ���� ����Ʈ��
    public List<Transform> goldSFX_RandomPoints = new List<Transform>();
    public GoodsPoolingConrtoller goldPoolingCont;
    public GoodsPoolingConrtoller bonePoolingCont;


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

        // ��� Ǯ�� ������Ʈ ����
        goldPoolingCont.Init();
        bonePoolingCont.Init();
        //CreateInstanceGolds();
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



    ParticleRoate GetDisableParticleRoate(List<ParticleRoate> particles)
    {
        return particles.FirstOrDefault(f => !f.gameObject.activeSelf);
    }

    ParticleSystem GetInsectDisableEff()
    {
        return insectDisableEffects.FirstOrDefault(f => !f.gameObject.activeSelf);
    }

    // ��� ���� ����Ʈ
    public Transform GetGoldSfxRandomPoint(EnumDefinition.GoldPosType pointType)
    {
        return goldSFX_RandomPoints[(int)pointType];
    }

}
