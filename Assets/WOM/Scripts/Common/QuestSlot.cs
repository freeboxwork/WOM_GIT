using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestSlot : MonoBehaviour
{

    public Image imgNotifyIcon;
    public Image imgRewardIcon;
    public TextMeshProUGUI txtQuestName;
    public Image imgQuestProgress;
    public TextMeshProUGUI txtQuestProgressCount;
    public Button btnReward;
    public TextMeshProUGUI txtDoing;



    void Start()
    {

    }

    void SetBtnEvent()
    {
        btnReward.onClick.AddListener(() =>
        {
        });
    }

    public void SetNotifyIcon(Sprite sprite)
    {
        imgNotifyIcon.sprite = sprite;
    }

    public void SetRewardIcon(Sprite sprite)
    {
        imgRewardIcon.sprite = sprite;
    }

    public void SetQuestName(string name)
    {
        txtQuestName.text = name;
    }

    public void SetQuestProgress(float progress)
    {
        imgQuestProgress.fillAmount = progress;
    }

    public void SetQuestProgressCount(string count)
    {
        txtQuestProgressCount.text = count;
    }

    public void SetRewardButton(bool isActive)
    {
        btnReward.interactable = isActive;
    }

    public void SetDoingText(string text)
    {
        txtDoing.text = text;
    }


}
