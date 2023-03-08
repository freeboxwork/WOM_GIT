using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace WOM.SheetData
{
    [System.Serializable]
    public class EvolutionData : SheetData
    {
        public int depthId;
        public int level;
        public string name;
        public EnumDefinition.InsectType insectType;
        public float damage;
        public float damageRate;
        public float criticalChance;
        public float criticalDamage;
        public float speed;
        public float goldBonus;
        public float bossDamage;
        public string grade;
        public float spawnTime;


    }

#if UNITY_EDITOR
    [CustomEditor(typeof(EvolutionData))]
    public class CompanyInfoEditor : Editor
    {
        EvolutionData _target;

        void OnEnable()
        {
            _target = (EvolutionData)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Pull Data"))
            {
                UtilityMethod.UpdateStats(SheetDataGlobalValues.sheetNameEvolutionBee, _target.dataId.ToString(), _target, UtilityMethod.UpdateMethodOne);
            };
        }

    }
#endif


}

