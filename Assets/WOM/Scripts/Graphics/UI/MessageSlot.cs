using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace ProjectGraphics
{
    //단순하고 심플하게
    public class MessageSlot : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI titleText;
        [SerializeField] TextMeshProUGUI messageText;
        [SerializeField] Image backImage;
        [SerializeField] RectTransform rect;
        [SerializeField] GlobalPopupAnimationController popupAnimationController;

        Color titleTextColor;
        Color messageTextColor;
        Color backImageColor;

        private void Awake()
        {
            //준비
            titleTextColor = titleText.color;
            messageTextColor = messageText.color;
            backImageColor = backImage.color;
        }

        private void OnEnable()
        {
            InitializedSlot();
        }

        //초기화
        private void InitializedSlot()
        {
            transform.localScale = Vector3.zero;
            rect.anchoredPosition = popupAnimationController.startPosition;

            titleText.color = titleTextColor;
            titleText.text = "Title";

            messageText.color = messageTextColor;
            messageText.text = "Message";

            backImage.color = backImageColor;
        }

        //등장
        public void OpenTheMessageAnimation(string title, string message)
        {
            titleText.text = title;
            messageText.text = message;
            transform.DOScale(Vector3.one, popupAnimationController.openDuration).SetEase(Ease.OutBack);
        }

        //밀림과 삭제
        public void ClimbTheMessageAnimation()
        {
            titleText.DOFade(0.0f, popupAnimationController.climbDuration).SetEase(Ease.InQuad);
            messageText.DOFade(0.0f, popupAnimationController.climbDuration).SetEase(Ease.InQuad);
            backImage.DOFade(0.0f, popupAnimationController.climbDuration).SetEase(Ease.InQuad);
            rect.DOAnchorPos(popupAnimationController.climeTargetPosition, popupAnimationController.climbDuration).SetEase(Ease.OutQuart)
                .OnComplete(OnCompletedAnimation);
        }

        private void OnCompletedAnimation()
        {
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            InitializedSlot();
        }
    }
}