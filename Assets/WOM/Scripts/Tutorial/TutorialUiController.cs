using System.Collections;
using System.Collections.Generic;
using Coffee.UIExtensions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialUiController : MonoBehaviour
{
    public GameObject tutoSet;
    public Unmask unmask;
    public Image imgUnmask;
    public TextMeshProUGUI txtDesc;

    public void EnableTutorial(string message, Image image)
    {
        tutoSet.gameObject.SetActive(true);

        SetTxtDesc(message);
        imgUnmask.sprite = image.sprite;
        unmask.fitTarget = image.rectTransform;
    }

    void SetTxtDesc(string value)
    {
        txtDesc.text = value;   
    }
   
    public void DisableTutorial()
    {
        tutoSet.gameObject.SetActive(false);
    }
}
