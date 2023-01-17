using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnionManager : MonoBehaviour
{
    public List<UnionSlot> unionSlots = new List<UnionSlot>();
    //public List<UnionInGameData> unionInGameDatas = new List<UnionInGameData>(); // 저장 데이터
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
            int index = i;
            var data = unionDatas[index];
            var slot = unionSlots[index];

            //UnionInGameData inGameData = new UnionInGameData();

            // set id
            slot.inGameData.unionIndex = data.unionIndex;
            // set type
            slot.unionGradeType = (EnumDefinition.UnionGradeType)System.Enum.Parse( typeof(EnumDefinition.UnionGradeType), data.gradeType);

            // set face image
            slot.SetUIImageUnion(spriteFileData.GetIconData(data.unionIndex));
                                   
            // TODO: 저장된 데이터에서 불러와야 함

            // set level 
            slot.inGameData.level = 0;

            // set reqirement count
            slot.inGameData.LevelUpReqirementCount = data.reqirementCount;

            // set equip type
            slot.unionEquipType = EnumDefinition.UnionEquipType.NotEquipped;

            // set slider value
            slot.SetSliderValue();

            // set ui
            slot.SetUITxtLevel();
            slot.SetUITxtUnionCount();
            slot.SetUITxtUnionEquipState();

            // set BtnAction
            slot.btn.onClick.AddListener(() => {
                GlobalData.instance.unionInfoPopupController.EnablePopup(slot, data, slot.inGameData);
            });


            // SET IN GAME DATA ( TODO: 저장된 데이터에서 불러와야 함 )
            slot.inGameData.damage = data.damage;
            slot.inGameData.spawnTime = data.spawnTime;
            slot.inGameData.moveSpeed = data.moveSpeed;
            slot.inGameData.passiveDamage = data.passiveDamage;

            yield return null;
        }
    }


    UnionSlot GetUnionSlotByID(int id)
    {
        return unionSlots.FirstOrDefault(f => f.inGameData.unionIndex == id);
    }


    public void AddUnion(int unionId)
    {
        var slot = GetUnionSlotByID(unionId);
        if (slot.inGameData.isUnlock == false)
        {
            slot.EnableSlot();
        }

        // set slot data
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
