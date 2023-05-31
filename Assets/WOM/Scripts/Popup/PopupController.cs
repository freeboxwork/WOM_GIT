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
    private List<RewardInfoData> rewardInfoList;
    private string popupTitleName;

    private EEfectGameObjectTYPE effectType;

    private Action callBack;

    public void SetTitle(string title)
    {
        this.popupTitleName = title;
    }
    public void SetRewardInfoList(List<RewardInfoData> list)
    {
        this.rewardInfoList = list;
    }
    public void SetEffectType(EEfectGameObjectTYPE type)
    {
        this.effectType = type;
    }
    public void SetCallBack(Action cb)
    {
        this.callBack = cb;
    }

    //GET
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

    //리워드 정보가 담긴 클래스
    PopupRewardInfoData popupRewardInfoData = null;
    //콜백 이벤트 액션
    List<Action> callbacks = new List<Action>();
    [Header("보상 이펙트 게임오브젝트")]
    public GameObject rewardEffect;
    [Header("커스텀 팝업 생성 부모 위치")]
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
        //popupBuilder.SetButton(I2.Loc.LocalizationManager.GetTranslation("탭하여닫기 버튼") , callbacks);
        popupBuilder.SetButton("닫기", callbacks);
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




}
