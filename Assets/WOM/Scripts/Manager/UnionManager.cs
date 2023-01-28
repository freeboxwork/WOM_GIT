using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnionManager : MonoBehaviour
{
    public List<UnionSlot> unionSlots = new List<UnionSlot>();
    public List<UnionEquipSlot> unionEquipSlots = new List<UnionEquipSlot>();
    public SpriteFileData spriteFileData;
    
    // 장착된 유니온 데이터
   // public List<UnionSlot> equipUnionSlots = new List<UnionSlot>();

    UnionSlot selectedSlot;

    void Start()
    {
        UnlockEquipSlots(GlobalData.instance.player.evalutionLeveldx);
    }

    public void UnlockEquipSlots(int dataId)
    {
        var data = GlobalData.instance.dataManager.GetRewaedEvolutionGradeDataByID(dataId);

        // slot count 만큼 슬롯 열어줌
        for (int i = 0; i < data.slotCount; i++)
        {
            unionEquipSlots[i].UnLockSlot();
        }
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

            // set id
            slot.inGameData.unionIndex = data.unionIndex;
            // set type
            slot.unionGradeType = (EnumDefinition.UnionGradeType)System.Enum.Parse( typeof(EnumDefinition.UnionGradeType), data.gradeType);
            // set face image
            slot.SetUIImageUnion(spriteFileData.GetIconData(data.unionIndex));
            // set data
            slot.unionData = data;


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

    public void EnableEquipSlotBtns()
    {
        var data = GlobalData.instance.dataManager.GetRewaedEvolutionGradeDataByID(GlobalData.instance.player.evalutionLeveldx);
        // slot count 만큼 슬롯 열어줌
        for (int i = 0; i < data.slotCount; i++)
        {
            unionEquipSlots[i].SetBtnEnableState(true);
            unionEquipSlots[i].EnableEffHighlight(true);
        }
    }

    public void EquipSlot(UnionEquipSlot equipSlot)
    {
        equipSlot.EquipUnion(selectedSlot);
        unionEquipSlots.ForEach(f => f.SetBtnEnableState(false));
    }

    public void SetSelectedSlot(UnionSlot slot)
    {
        selectedSlot = slot;
    }
        
    public UnionSlot GetSelectedSlotData()
    {
        return selectedSlot;
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

    public float GetUnionDamage(UnionSlot slot)
    {
        var damage = slot.unionData.damage + (slot.inGameData.level * slot.unionData.addDamage); 
        return damage;
    }

    public float GetUnionPassiveDamage(UnionSlot slot) 
    {
        var passiveDamage = slot.unionData.passiveDamage + (slot.inGameData.level * slot.unionData.addPassiveDamage); 
        return passiveDamage;
    }

    public int GetUnionReqireCount(UnionSlot slot)
    {
        var reqieCount = slot.unionData.reqirementCount + (slot.inGameData.level * slot.unionData.addReqirementCount);
        return reqieCount;
    }

    public bool IsValidLevelUpCount(UnionSlot slot)
    {
        return slot.inGameData.unionCount >= GetUnionReqireCount(slot);
    }

    public bool LevelUpUnion(UnionSlot slot)
    {
        var isValidLevelUp = IsValidLevelUpCount(slot);
        if (isValidLevelUp)
        {
            var cost = GetUnionReqireCount(slot);
            slot.PayUnion(cost);
            slot.LevelUp();
            slot.inGameData.LevelUpReqirementCount = GetUnionReqireCount(slot);
            slot.inGameData.damage = GetUnionDamage(slot);
            slot.inGameData.passiveDamage = GetUnionPassiveDamage(slot);
            slot.RelodUISet();
        }
        else
        {
            Debug.Log($"레벨업에 필요한 유니온이 부족합니다. {slot.unionData.name} _ {slot.inGameData.unionCount}");
        }
        return isValidLevelUp;
    }

}
