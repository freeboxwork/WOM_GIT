using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnionEquipSlot : MonoBehaviour
{
    public Image imgFrame;
    public Image imgUnionFace;
    public Image imgUnlock;
    public Button btnSlot;
    public bool isUnLock = false;
    public UnionSlot unionSlot;
    void Start()
    {
        SetBtnEvent();
        SetBtnEnableState(false);
    }

    void SetBtnEvent()
    {
        btnSlot.onClick.AddListener(() =>
        {
            GlobalData.instance.unionManager.EquipSlot(this);
        });
    }

    public void SetBtnEnableState(bool value)
    {
        btnSlot.enabled = value;
    }

    public void UnLockSlot()
    {
        isUnLock = true;
        imgUnlock.gameObject.SetActive(false);
    }

    public void SetUI()
    {
        imgUnionFace.sprite = unionSlot.imgUnionFace.sprite;
    }

    public void EquipUnion(UnionSlot _unionSlot)
    {
        unionSlot = _unionSlot;
        SetUI();
    }
    
}
