using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using TMPro;

namespace ProjectGraphics
{
    public class FloatingText : MonoBehaviour
    {
        [SerializeField]Transform textTransform;
        MeshRenderer rend;
        TextMeshPro textMesh;

        [Header("애니메이션 길이")]
        public float duration = 1.0f;
        [Header("목표 Y 위치")]
        public float targetPosY = 0.5f;
        [Header("글자크기")]
        public int fontsSize = 4;

        //임시
        private bool isCritical = false;
        
        //노말 텍스트 컬러, 크리티컬 텍스트 컬러,
        //크리티컬 글자!
        private Color setColor = Color.white;

        private void Awake()
        {
            rend = GetComponentInChildren<MeshRenderer>();
            textMesh = GetComponentInChildren<TextMeshPro>();

            //폰트 형태 변경
            //textMesh.fontStyle = FontStyles.Normal;
        }

        private void OnEnable()
        {
            StartAnimation();
        }

        public void SetText(string text)
        {

        }

        private void StartAnimation()
        {
            //위로 올라가는 애니메이션.
            //끝나면 위치 초기화
            textTransform.DOLocalMoveY(targetPosY, duration).SetEase(Ease.OutCirc).OnComplete(() => this.gameObject.SetActive(false));
            
        }

        private void OnDisable()
        {
            textTransform.localPosition = Vector3.zero;
            textMesh.color = Color.white;
        }
    }
}
