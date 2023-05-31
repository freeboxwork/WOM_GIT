using UnityEngine.UI;
using TMPro;

public class CampPopup : CastlePopupBase
{

    public Image imgSummonCountProgress;
    public TextMeshProUGUI txtSummonCount;
    public Button btnGetReward;



    void Start()
    {
        SetBtnEvent();
    }

    public void SetSummonCountProgress(int totalValue, int curValue)
    {
        imgSummonCountProgress.fillAmount = (float)curValue / (float)totalValue;
        txtSummonCount.text = $"{curValue}/{totalValue}";
    }

    void SetBtnEvent()
    {
        btnGetReward.onClick.AddListener(() =>
        {
            GlobalData.instance.rewardManager.UnionReward();
        });
    }

}
