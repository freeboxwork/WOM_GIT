using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGraphics
{
    public class GlobalPopupAnimationController : MonoBehaviour
    {
        [Header("���� �ӵ�")]
        public float openDuration = 0.5f;
        [Header("�ö󰡴� �ӵ�")]
        public float climbDuration = 0.5f;
        public Vector3 climeTargetPosition;
        public Vector3 startPosition;

        public MessageSlot[] slots;
        [SerializeField] int currentIndex;

        bool allSlotHide = true; //����ó����

        void Start()
        {
            currentIndex = 0;
        }

        public void OpenThePopup(string title, string message)
        {
            //�� �ڽ��� ������ ������ ���� �ȵ�.
            if (gameObject.activeSelf == false) return;

            //ó���������� 0�� �ε��� ȣ���ϰ� �ε��� �Է�
            //���� �ε����� ���� ������ ���� �ε��� ȣ��.
            //���� ���� �ε����� ������ �ε��� �϶� �ٽ� 0���ε��� ȣ��
            //���� �ε����� �ö󰡴� �ִϸ��̼� ����.

            if (slots[currentIndex].gameObject.activeSelf == true) currentIndex++;

            slots[currentIndex].gameObject.SetActive(true);
            slots[currentIndex].OpenTheMessageAnimation(title, message);

            int previousIndex = 0;

            if (currentIndex == 0)
            {
                if (slots[slots.Length - 1].gameObject.activeSelf == true)
                {
                    slots[slots.Length - 1].ClimbTheMessageAnimation();
                }
                return;
            }
            else if (currentIndex == slots.Length - 1)
            {
                previousIndex = currentIndex - 1;
                currentIndex = 0;
            }
            else
            {
                previousIndex = currentIndex - 1;
            }

            slots[previousIndex].ClimbTheMessageAnimation();
        }

        private void OnDisable()
        {
            currentIndex = 0;
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].gameObject.SetActive(false);
            }
        }
    }
}
