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

        [Header("�ִϸ��̼� ����")]
        public float duration = 1.0f;
        [Header("��ǥ Y ��ġ")]
        public float targetPosY = 0.5f;
        [Header("����ũ��")]
        public int fontsSize = 4;

        //�ӽ�
        private bool isCritical = false;
        
        //�븻 �ؽ�Ʈ �÷�, ũ��Ƽ�� �ؽ�Ʈ �÷�,
        //ũ��Ƽ�� ����!
        private Color setColor = Color.white;

        private void Awake()
        {
            rend = GetComponentInChildren<MeshRenderer>();
            textMesh = GetComponentInChildren<TextMeshPro>();

            //��Ʈ ���� ����
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
            //���� �ö󰡴� �ִϸ��̼�.
            //������ ��ġ �ʱ�ȭ
            textTransform.DOLocalMoveY(targetPosY, duration).SetEase(Ease.OutCirc).OnComplete(() => this.gameObject.SetActive(false));
            
        }

        private void OnDisable()
        {
            textTransform.localPosition = Vector3.zero;
            textMesh.color = Color.white;
        }
    }
}
