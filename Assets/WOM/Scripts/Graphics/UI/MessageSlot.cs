using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MessageSlot : MonoBehaviour
{
    [SerializeField] RectTransform rect;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] RectTransform messageRect;
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] RectTransform backImageRect;
}
