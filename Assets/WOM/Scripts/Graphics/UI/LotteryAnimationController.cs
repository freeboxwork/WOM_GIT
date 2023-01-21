using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectGraphics
{
    public class LotteryAnimationController : MonoBehaviour
    {
        public SpriteFileData data;
        [Header("DNA Icon 이미지"), SerializeField]
        private Sprite[] dnaIcons;

        public Color[] effectColor;
        public Sprite[] gradeBackImage;
        private Lottery_Slot[] slots;


#if UNITY_EDITOR
        //[SerializeField] int ii;
#endif

        private void Awake()
        {
            slots = GetComponentsInChildren<Lottery_Slot>();
            foreach(var slot in slots) slot.gameObject.SetActive(false);
        }

#if UNITY_EDITOR
        void Start()
        {
            /*
            int[] uIndex = new int[ii];
            for (int i = 0; i < uIndex.Length; i++)
            {
                uIndex[i] = Random.Range(0, data.GetDataSize());
            }
            StartCoroutine(ShowUnionSlotCardOpenProcess(uIndex));
            */
        }
#endif

        public void StartLotteryAnimation(int[] unionIndex)
        {
            StartCoroutine(ShowUnionSlotCardOpenProcess(unionIndex));
        }

        IEnumerator ShowUnionSlotCardOpenProcess(int[] u)
        {
            yield return new WaitForSeconds(0.05f);

            for (int i = 0; i < u.Length; i++)
            {
                int typeIndex = SetImageFromUnionType(data.GetGradeData(u[i]));
                
                if (typeIndex == 5)
                {
                    yield return new WaitForSeconds(0.2F);
                }

                slots[i].SetSlotImage(effectColor[typeIndex], gradeBackImage[typeIndex], data.GetIconData(u[i]));
                slots[i].gameObject.SetActive(true);
                
                //여기 출현 사운드 필요함.

                for (int j = 0; j < i; j++)
                {
                    slots[j].SetShakeAction();
                }

                if (typeIndex == 5)
                {
                    yield return new WaitForSeconds(0.5F);
                }
                else
                {
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }

        public void StartDNASlotAnimation(int[] u)
        {
            StartCoroutine(ShowDNAIconSlotCardOpenProcess(u));
        }

        //슬롯 형태 확인 하고, 백 이미지 지우고 아이콘 이미지만 처리 이펙트 컬러 통일.
        IEnumerator ShowDNAIconSlotCardOpenProcess(int[] u)
        {
            yield return new WaitForSeconds(0.05f);

            for (int i = 0; i < u.Length; i++)
            {
                //타입 없음
                //GradeBackImage = DNA Icon
                int typeIndex = 0;

                slots[i].SetSlotImage(effectColor[typeIndex], gradeBackImage[u[i]]);
                slots[i].gameObject.SetActive(true);

                //여기 출현 사운드 필요함.

                for (int j = 0; j < i; j++)
                {
                    slots[j].SetShakeAction();
                }

                yield return new WaitForSeconds(0.1f);
            }
        }

        int SetImageFromUnionType(EnumDefinition.UnionGradeType type)
        {
            switch (type)
            {
                case EnumDefinition.UnionGradeType.high: return 1;
                case EnumDefinition.UnionGradeType.rare: return 2;
                case EnumDefinition.UnionGradeType.hero: return 3;
                case EnumDefinition.UnionGradeType.legend: return 4;
                case EnumDefinition.UnionGradeType.unique: return 5;
                default: return 0;
            }
        }

        private void OnDisable()
        {
            foreach (var slot in slots) slot.gameObject.SetActive(false);
        }
    }
}