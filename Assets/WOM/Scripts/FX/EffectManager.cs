using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public int InsectDisableEffectsBirthCount = 15;
    public ParticleSystem prefabInsectDisableEff;
    public List<ParticleSystem> InsectDisableEffects = new List<ParticleSystem>();

    public Transform trEffects;
    void Start()
    {

    }

    public IEnumerator Init()
    {
        yield return null;
        CreateInsectDisableEffects();
    }

    void CreateInsectDisableEffects()
    {
        for (int i = 0; i < InsectDisableEffectsBirthCount; i++)
        {
            var effect = Instantiate(prefabInsectDisableEff, trEffects);
            InsectDisableEffects.Add(effect);
            effect.gameObject.SetActive(false);
        }
    }

    public void EnableInsectDisableEffect(Transform tr)
    {
        var effect = GetInsectDisableEff();
        effect.transform.position = tr.transform.position;
        effect.gameObject.SetActive(true);
        //effect.Play();
    }

    ParticleSystem GetInsectDisableEff()
    {
        return InsectDisableEffects.FirstOrDefault(f => !f.gameObject.activeSelf);
    }

}
