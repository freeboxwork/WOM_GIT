using System.Collections.Generic;
using UnityEngine;

namespace WOM.SheetData
{
    [CreateAssetMenu(fileName = "SheetDataMaker", menuName = "WOM/SheetDataMaker", order = 1)]
    public class SheetDataMaker : ScriptableObject
    {
        public List<EvolutionData> evolutionDatas;
    }

}


