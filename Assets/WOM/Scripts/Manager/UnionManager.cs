using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnionManager : MonoBehaviour
{
    public List<UnionSlot> unionSlots = new List<UnionSlot>();
    public List<UnionInGameData> unionInGameDatas = new List<UnionInGameData>(); // 저장 데이터
    public SpriteFileData spriteFileData;
    void Start()
    {
        
    }


    // set slot data
    public IEnumerator Init()
    {
        var unionDatas = GlobalData.instance.dataManager.unionDatas.data;

        for (int i = 0; i < unionDatas.Count; i++)
        {
            var data = unionDatas[i];
            var slot = unionSlots[i];

            // set id
            slot.unionId = data.unionIndex;
            // set type
            slot.unionGradeType = (EnumDefinition.UnionGradeType)System.Enum.Parse( typeof(EnumDefinition.UnionGradeType), data.gradeType);

            // set face image
            slot.SetUIImageUnion(spriteFileData.GetIconData(data.unionIndex));

            // TODO: 저장된 데이터에서 불러와야 함

            // set level 
            slot.curLevel = 0;

            // set reqirement count
            slot.LevelUpReqirementCount = data.reqirementCount;

            // set equip type
            slot.unionEquipType = EnumDefinition.UnionEquipType.NotEquipped;

            // set slider value
            slot.SetSliderValue();

            // set ui
            slot.SetUITxtLevel();
            slot.SetUITxtUnionCount();
            slot.SetUITxtUnionEquipState();

            yield return null;
        }
    }


    UnionSlot GetUnionSlotByID(int id)
    {
        return unionSlots.FirstOrDefault(f => f.unionId == id);
    }

    public void AddUnion(int unionId)
    {
        var slot = GetUnionSlotByID(unionId);
        if (slot.isUnlock == false) slot.EnableSlot();

        slot.AddUnion(1);
        slot.SetUITxtUnionCount();
        slot.SetSliderValue();
    }

    public void AddUnions(List<int> indexList)
    {
        for (int i = 0; i < indexList.Count; i++)
        {
            var id = indexList[i];
            AddUnion(id);
        }
    }
}
