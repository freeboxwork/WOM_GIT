using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGraphics
{
    public class CastleController : MonoBehaviour
    {
        [SerializeField] GameObject castleObject; //ĳ��������Ʈ
        [SerializeField] GameObject[] campBuild;//��ȯ�ǹ�(ķ��)
        [SerializeField] GameObject[] factoryBuild;//������ ����ǹ�(������)
        [SerializeField] GameObject[] mineBuild;//��� ����ǹ�(����)
        [SerializeField] GameObject[] labBuild;//�������׷��̵� �ǹ�(������)

        //�ӽ�
        [Range(0, 4), SerializeField]
        int campBuildLv = 0;
        [Range(0, 4), SerializeField]
        int factoryBuildLv = 0;
        [Range(0, 4), SerializeField]
        int mineBuildLv = 0;
        [Range(0, 4), SerializeField]
        int labBuildLv = 0;

        public enum BuildingType
        {
            CAMP, FACTORY, MINE, LAB
        }

        void Update()
        {
            /*
            for (int i = 0; i < campBuild.Length; i++)
            {
                if (campBuildLv < i) campBuild[i].SetActive(false);     else campBuild[i].SetActive(true);
                if (factoryBuildLv < i) factoryBuild[i].SetActive(false);     else factoryBuild[i].SetActive(true);
                if (mineBuildLv < i) mineBuild[i].SetActive(false);       else mineBuild[i].SetActive(true);
                if (labBuildLv < i) labBuild[i].SetActive(false); else labBuild[i].SetActive(true);
            }
            */
        }

        public void SetBuildUpgrade(BuildingType type, int level)
        {
            switch (type)
            {
                case BuildingType.CAMP: SetCampBuild(level);   break;
                case BuildingType.FACTORY: SetFactoryBuild(level);   break;
                case BuildingType.MINE:     SetMineBuild(level);    break;
                case BuildingType.LAB: SetLabBuild(level); break;
            }

            if(!castleObject.activeSelf) castleObject.SetActive(true);
        }

        private void SetCampBuild(int level)
        {
            for (int i = 0; i < campBuild.Length; i++)
            {
                if(i <= level) campBuild[i].SetActive(true);
                else campBuild[i].SetActive(false);
            }

            SetBuildEffect(campBuild[level].transform.position);
        }

        private void SetFactoryBuild(int level)
        {
            for (int i = 0; i < factoryBuild.Length; i++)
            {
                if (i <= level) factoryBuild[i].SetActive(true);
                else factoryBuild[i].SetActive(false);
            }

            SetBuildEffect(factoryBuild[level].transform.position);
        }

        private void SetMineBuild(int level)
        {
            for (int i = 0; i < mineBuild.Length; i++)
            {
                if (i <= level) mineBuild[i].SetActive(true);
                else mineBuild[i].SetActive(false);
            }

            SetBuildEffect(mineBuild[level].transform.position);
        }

        private void SetLabBuild(int level)
        {
            for (int i = 0; i < labBuild.Length; i++)
            {
                if (i <= level) labBuild[i].SetActive(true);
                else labBuild[i].SetActive(false);
            }

            SetBuildEffect(labBuild[level].transform.position);
        }

        private void SetBuildEffect(Vector3 pos)
        {
            //�����Ǵ� ��ġ�� ����Ʈ �߰�.
            Debug.Log("����Ʈ ��ġ : " + pos);
        }
    }
}