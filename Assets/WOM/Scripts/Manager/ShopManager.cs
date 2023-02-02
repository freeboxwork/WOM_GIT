using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public List<ShopSlot> shopSlots = new List<ShopSlot>();

    void Start()
    {

    }



    public IEnumerator Init()
    {
        SetButtonEvents();

        // Set Shop Slot UI ( 필요시 사용 )
        // SetShopSlots(); 
        yield return null;
    }

    void SetShopSlots()
    {
        for (int i = 0; i < shopSlots.Count; i++)
        {
            var slot = shopSlots[i];
            
            // set ui
        }
    }

    private void SetButtonEvents()
    {
        // UNION 1 ( 34 )
        UtilityMethod.SetBtnEventCustomTypeByID(34, () =>
        {
            // disable menu ui
            GlobalData.instance.uiController.AllDisableMenuPanels();
            GlobalData.instance.evolutionManager.UnionLotteryGameStart(1);
        });

        // UNION 11
        UtilityMethod.SetBtnEventCustomTypeByID(35, () =>
        {
            GlobalData.instance.uiController.AllDisableMenuPanels();
            GlobalData.instance.evolutionManager.UnionLotteryGameStart(11);
        });

        // DNA 1
        UtilityMethod.SetBtnEventCustomTypeByID(36, () => { });

        // DNA 11
        UtilityMethod.SetBtnEventCustomTypeByID(37, () => { });

        // FREE GEM 1
        UtilityMethod.SetBtnEventCustomTypeByID(38, () => { });

        // FREE GEM 10
        UtilityMethod.SetBtnEventCustomTypeByID(39, () => { });

        // FREE UNION 1
        UtilityMethod.SetBtnEventCustomTypeByID(40, () => { });

        // FREE UNION 10
        UtilityMethod.SetBtnEventCustomTypeByID(41, () => { });

        // FREE DNA 1
        UtilityMethod.SetBtnEventCustomTypeByID(42, () => { });

        // FREE DNA 10
        UtilityMethod.SetBtnEventCustomTypeByID(43, () => { });

        // 유니온 뽑기 닫기 버튼
        UtilityMethod.SetBtnEventCustomTypeByID(44, () => {

            GlobalData.instance.uiController.EnableMenuPanel(EnumDefinition.MenuPanelType.shop);

        });


    }

    ShopSlot GetShopSlotByType(EnumDefinition.ShopSlotType type)
    {
        return shopSlots.FirstOrDefault(f=> f.shopSlotType == type);
    }

}
