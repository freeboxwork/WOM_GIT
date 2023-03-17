using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGraphics
{
    public class CastleController : MonoBehaviour
    {
        [SerializeField] GameObject[] unionBuild;
        [SerializeField] GameObject[] skullBuild;
        [SerializeField] GameObject[] goldBuild;
        [SerializeField] GameObject[] dungeonBuild;

        //임시
        [Range(0, 4)]
        public int unionBuildLv = 0;
        [Range(0, 4)]
        public int skullBuildLv = 0;
        [Range(0, 4)]
        public int goldBuildLv = 0;
        [Range(0, 4)]
        public int dungeonBuildLv = 0;

        public enum BuildingType
        {
            UNION, SKULL, GOLD, DUNGEON
        }

        void Update()
        {
            for (int i = 0; i < unionBuild.Length; i++)
            {
                if (unionBuildLv < i) unionBuild[i].SetActive(false);     else unionBuild[i].SetActive(true);
                if (skullBuildLv < i) skullBuild[i].SetActive(false);     else skullBuild[i].SetActive(true);
                if (goldBuildLv < i) goldBuild[i].SetActive(false);       else goldBuild[i].SetActive(true);
                if (dungeonBuildLv < i) dungeonBuild[i].SetActive(false); else dungeonBuild[i].SetActive(true);
            }
        }

        public void SetBuildUpgrade(BuildingType type, int level)
        {
            switch (type)
            {
                case BuildingType.UNION:    SetUnionBuild(level);   break;
                case BuildingType.SKULL:    SetSkullBuild(level);   break;
                case BuildingType.GOLD:     SetGoldBuild(level);    break;
                case BuildingType.DUNGEON:  SetDungeonBuild(level); break;
            }
        }

        private void SetUnionBuild(int level)
        {
            for (int i = 0; i < unionBuild.Length; i++)
            {
                if(i <= level) unionBuild[i].SetActive(true);
                else unionBuild[i].SetActive(false);
            }

            SetBuildEffect(unionBuild[level].transform.position);
        }

        private void SetSkullBuild(int level)
        {
            for (int i = 0; i < skullBuild.Length; i++)
            {
                if (i <= level) skullBuild[i].SetActive(true);
                else skullBuild[i].SetActive(false);
            }

            SetBuildEffect(skullBuild[level].transform.position);
        }

        private void SetGoldBuild(int level)
        {
            for (int i = 0; i < goldBuild.Length; i++)
            {
                if (i <= level) goldBuild[i].SetActive(true);
                else skullBuild[i].SetActive(false);
            }

            SetBuildEffect(goldBuild[level].transform.position);
        }

        private void SetDungeonBuild(int level)
        {
            for (int i = 0; i < dungeonBuild.Length; i++)
            {
                if (i <= level) dungeonBuild[i].SetActive(true);
                else dungeonBuild[i].SetActive(false);
            }

            SetBuildEffect(dungeonBuild[level].transform.position);
        }

        private void SetBuildEffect(Vector3 pos)
        {
            //생성되는 위치에 이펙트 추가.
            Debug.Log("이펙트 위치 : " + pos);
        }
    }
}