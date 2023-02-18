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

        //�ӽ�
        //public bool isCritical = false;

        //�븻 �ؽ�Ʈ �÷�, ũ��Ƽ�� �ؽ�Ʈ �÷�,
        //ũ��Ƽ�� ����!
        public Color nomalColor;
        public Color criticalColor;

        private void Awake()
        {
            rend = GetComponentInChildren<MeshRenderer>();
            textMesh = GetComponentInChildren<TextMeshPro>();

            //��Ʈ ���� ����
            //textMesh.fontStyle = FontStyles.Normal;
        }

        private void OnEnable()
        {
            /*
            if (isCritical)
            {
                textMesh.fontSize = 4;
                textMesh.text = "Critical!\n" + "0000";
                textMesh.color = criticalColor;
                textMesh.fontStyle = FontStyles.Italic | FontStyles.Bold | FontStyles.UpperCase;
            }
            else
            {
                textMesh.fontSize = 3;
                textMesh.text = "0000";
                textMesh.color = nomalColor;
                textMesh.fontStyle = FontStyles.Bold | FontStyles.UpperCase;
            }
            */
            StartAnimation();
        }

        public void SetText(string text, bool crit)
        {
            if (crit)
            {
                textMesh.fontSize = 4;
                textMesh.text = "Critical!\n" + text;
                textMesh.color = criticalColor;
                textMesh.fontStyle = FontStyles.Italic | FontStyles.Bold | FontStyles.UpperCase;
            }
            else
            {
                textMesh.fontSize = 3;
                textMesh.text = text;
                textMesh.color = nomalColor;
                textMesh.fontStyle = FontStyles.Bold | FontStyles.UpperCase;
            }
            
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
