using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;




public class RewardInfoData
{
    public float amount;
    public EnumDefinition.RewardType type;

    public Sprite icon;

    public RewardInfoData(EnumDefinition.RewardType type, float amount,Sprite sp)
    {
        this.type = type;
        this.amount = amount;
        this.icon = sp;
    }
}

public enum EEfectGameObjectTYPE
{
    None,
    BossReward,
    Max
}


public class PopupRewardInfoData
{
    //보상 재화 정보
    private List<RewardInfoData> rewardInfoList;
    //보상 팝업 이름
    private string popupTitleName;
    //연출 이펙트 타입
    private EEfectGameObjectTYPE effectType;
    //콜백 액션
    private Action callBack;

    public void SetPopupData(string title)
    {
        this.popupTitleName = title;
    }
    public void SetPopupData(string title, List<RewardInfoData> list)
    {
        this.popupTitleName = title;
        this.rewardInfoList = list;
    }
    public void SetPopupData(string title, List<RewardInfoData> list, Action cb)
    {
        this.popupTitleName = title;
        this.rewardInfoList = list;
        this.callBack = cb;
    }
    public void SetPopupData(string title , List<RewardInfoData> list, Action cb,EEfectGameObjectTYPE type)
    {
        this.popupTitleName = title;
        this.rewardInfoList = list;
        this.callBack = cb;
        this.effectType = type;
    }

    //GET//
    public string GetTitle()
    {
        return popupTitleName;
    }
    public List<RewardInfoData> GetRewardInfoDataList()
    {
        return rewardInfoList;
    }
    public EEfectGameObjectTYPE GetEffectType()
    {
        return effectType;
    }
    public Action GetCallBack()
    {
        return callBack;
    }
    

}

public class PopupController : MonoBehaviour
{
    public static PopupController instance;

    [Header("유니온 Sprite SO")]
    [SerializeField] SpriteFileData spriteFileData; //GetIconData()

    //由ъ썙?뱶 ?젙蹂닿?? ?떞湲? ?겢?옒?뒪
    PopupRewardInfoData popupRewardInfoData = null;
    //肄쒕갚 ?씠踰ㅽ듃 ?븸?뀡
    List<Action> callbacks = new List<Action>();
    [Header("이펙트 연출 게임오브젝트")]
    public GameObject rewardEffect;
    [Header("팝업의 부모 오브젝트")]
    public Transform popupParent;

    private void Awake()
    {
        instance = this;
    }


    public void SetupPopupInfo(PopupRewardInfoData data)
    {
        popupRewardInfoData = data;

        callbacks.Clear();

        var cb = popupRewardInfoData.GetCallBack();

        //rewardEffect.SetActive(true);

        if (cb != null)
        {
            callbacks.Add(cb);
        }

        callbacks.Add(Reward);

        PopupBuilder popupBuilder = new PopupBuilder(popupParent);
        popupBuilder.SetTitle(popupRewardInfoData.GetTitle());
        //popupBuilder.SetButton(I2.Loc.LocalizationManager.GetTranslation("?꺆?븯?뿬?떕湲? 踰꾪듉") , callbacks);
        popupBuilder.SetButton("?떕湲?", callbacks);
        var rewards = popupRewardInfoData.GetRewardInfoDataList();

        for (int i = 0; i < rewards.Count; i++)
        {
            popupBuilder.SetRewardInfo(rewards[i].type, rewards[i].amount);

        }

        popupBuilder.Build();

    }

    void Reward()
    {
        //Insert Audio Effect Play Code 

        var rewards = popupRewardInfoData.GetRewardInfoDataList();

        for (int i = 0; i < rewards.Count; i++)
        {
            switch(rewards[i].type)
            {
                case EnumDefinition.RewardType.gold:

                    break;

                case EnumDefinition.RewardType.bone:

                    break;

                case EnumDefinition.RewardType.gem:

                    break;

                case EnumDefinition.RewardType.dice:

                    break;

                case EnumDefinition.RewardType.coal:
                    break;

                case EnumDefinition.RewardType.clearTicket:
                    break;

                case EnumDefinition.RewardType.goldKey:
                    break;

                case EnumDefinition.RewardType.boneKey:
                    break;

                case EnumDefinition.RewardType.diceKey:
                    break;

                case EnumDefinition.RewardType.coalKey:
                    break;

                case EnumDefinition.RewardType.union:
                    break;

                case EnumDefinition.RewardType.dna:
                    break;
                case EnumDefinition.RewardType.none:
                    break;
                    
            } 
        }

        //rewardEffect.SetActive(false);


        //Player Data Save
    }


    [Sirenix.OdinInspector.Button]
    public void TestPopup()
    {
        PopupRewardInfoData data = new PopupRewardInfoData();
        List<RewardInfoData> rewards = new List<RewardInfoData>();

        rewards.Add(new RewardInfoData(EnumDefinition.RewardType.gold, 10000, null));
        rewards.Add(new RewardInfoData(EnumDefinition.RewardType.gem, 20000, null));
 

        data.SetPopupData("REWARD",rewards);

        SetupPopupInfo(data);




    }




}
