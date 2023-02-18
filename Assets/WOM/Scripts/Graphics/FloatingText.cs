using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using TMPro;
using UnityEngine.Playables;

namespace ProjectGraphics
{
    public class FloatingText : MonoBehaviour
    {
        MeshRenderer rend;
        TextMeshPro textMesh;

        [Header("애니메이션 길이")]
        public float duration = 1.0f;
        [Header("글자크기")]
        public int fontsSize = 4;

        //임시
        private bool isCritical = false;
        
        //노말 텍스트 컬러, 크리티컬 텍스트 컬러,
        //크리티컬 글자!


        private void Awake()
        {
            rend = GetComponent<MeshRenderer>();
            textMesh = GetComponent<TextMeshPro>();

            //폰트 형태 변경
            //textMesh.fontStyle = FontStyles.Normal;
        }

        private void OnEnable()
        {
            
        }

        public void SetText(string text)
        {

        }

        private void StartAnimation()
        {
            //위로 올라가는 애니메이션.
            //끝나면 위치 초기화
        }

        private void OnDisable()
        {
            
        }
    }
}
