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

        [Header("�ִϸ��̼� ����")]
        public float duration = 1.0f;
        [Header("����ũ��")]
        public int fontsSize = 4;

        //�ӽ�
        private bool isCritical = false;
        
        //�븻 �ؽ�Ʈ �÷�, ũ��Ƽ�� �ؽ�Ʈ �÷�,
        //ũ��Ƽ�� ����!


        private void Awake()
        {
            rend = GetComponent<MeshRenderer>();
            textMesh = GetComponent<TextMeshPro>();

            //��Ʈ ���� ����
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
            //���� �ö󰡴� �ִϸ��̼�.
            //������ ��ġ �ʱ�ȭ
        }

        private void OnDisable()
        {
            
        }
    }
}
