using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RewardManager : MonoBehaviour
{

    void Start()
    {
        SetRewardDic();
    }


    Dictionary<EnumDefinition.RewardType, UnityAction<int>> rewardDic = new Dictionary<EnumDefinition.RewardType, UnityAction<int>>();

    public void RewardByType(EnumDefinition.RewardType rewardType, int rewardValye)
    {
        rewardDic[rewardType].Invoke(rewardValye);

        // 획득 연출 추가

        // 획득 이벤트 타입과 획득량을 로그로 출력함
        Debug.Log($"획득 이벤트 타입 : {rewardType}, 획득량 : {rewardValye}");
    }

    void SetRewardDic()
    {
        rewardDic.Add(EnumDefinition.RewardType.gold, GlobalData.instance.player.AddGold);
        rewardDic.Add(EnumDefinition.RewardType.bone, GlobalData.instance.player.AddBone);
        rewardDic.Add(EnumDefinition.RewardType.dice, GlobalData.instance.player.AddDice);
        rewardDic.Add(EnumDefinition.RewardType.coal, GlobalData.instance.player.AddCoal);
        rewardDic.Add(EnumDefinition.RewardType.gem, GlobalData.instance.player.AddGem);
        rewardDic.Add(EnumDefinition.RewardType.clearTicket, GlobalData.instance.player.AddClearTicket);
    }



}
