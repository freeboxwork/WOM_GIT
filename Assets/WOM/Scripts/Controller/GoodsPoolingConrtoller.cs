using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoodsPoolingConrtoller : MonoBehaviour
{

    // ������ ������ �θ� �����Ʈ
    public Transform trEffects;

    public int goldPoolCount = 25;
    // ��� ������
    public GoldAnimController prefabGoldAnimCont;

    // ��� ������Ʈ Ǯ
    public List<GoldAnimController> goldAnimConts = new List<GoldAnimController>();
    // Ȱ��ȭ�� ��� ������Ʈ��
    List<GoldAnimController> enableGoldAnimConts = new List<GoldAnimController>();

    public EnumDefinition.GoodsType goodsType;

    public void Init()
    {
        //Ǯ�� ������Ʈ ����
        CreateInstanceGolds();
    }

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
            if (gold != null)
            {
                gold.gameObject.SetActive(true);
                gold.GoldInAnimStart();
                enableGoldAnimConts.Add(gold);
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                Debug.Log(goodsType + " Pool �� ��� �ֽ��ϴ�.");
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
                    // ��尡 UI ������ �̵��� ���� �̺�Ʈ ����
                    //Debug.Log("gold goal event!!");
                });

                yield return new WaitForSeconds(0.05f);
            }

        }
        enableGoldAnimConts.Clear();
    }


    GoldAnimController GetDisableGold()
    {
        var obj = goldAnimConts?.FirstOrDefault(f => !f.gameObject.activeSelf);
        if (obj != null) return obj;
        else return null;
        //return goldAnimConts.FirstOrDefault(f => !f.gameObject.activeSelf);
    }


}
