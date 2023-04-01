using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using static EnumDefinition;

public class CastleManager : MonoBehaviour
{
    public List<CastlePopupBase> castlePopupList = new List<CastlePopupBase >();
    
    public CastleBuildingData BuildDataMine;
    public CastleBuildingData BuildDataFactory;

    public int mineLevel = 0;
    public int factoryLevel = 0;


    void Start()
    {
        SetBtnEvents();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Init()
    {
        // GET SAVE DATA
        SetCastleData();
        yield return null;
        
        // 골드 채굴 시작
        StartCoroutine(MiningGold(BuildDataMine));
        // 뼈조각 채굴 시작 
        StartCoroutine(MiningBone(BuildDataFactory));
    }


    void SetCastleData()
    {
        var refBuildDataMine = GlobalData.instance.dataManager.GetBuildDataMineByLevel(mineLevel);
        var refBuildDataFactory = GlobalData.instance.dataManager.GetBuildDataFactoryByLevel(factoryLevel);

        BuildDataMine = new CastleBuildingData().Create().SetGoodsType(GoodsType.gold).Clone(refBuildDataMine);
        BuildDataFactory = new CastleBuildingData().Create().SetGoodsType(GoodsType.bone).Clone(refBuildDataFactory);
    }


    void SetBtnEvents() 
    {
        UtilityMethod.SetBtnEventCustomTypeByID(51,()=>{
            OpenCastlePopup(EnumDefinition.CastlePopupType.mine);            
        });
        UtilityMethod.SetBtnEventCustomTypeByID(52,()=>{
            OpenCastlePopup(EnumDefinition.CastlePopupType.factory);            
        });
        UtilityMethod.SetBtnEventCustomTypeByID(53,()=>{
            OpenCastlePopup(EnumDefinition.CastlePopupType.camp);            
        });
        UtilityMethod.SetBtnEventCustomTypeByID(54,()=>{
            OpenCastlePopup(EnumDefinition.CastlePopupType.lab);            
        });
    }


    CastlePopupBase GetCastlePopupByType(EnumDefinition.CastlePopupType popupType) 
    {
       return castlePopupList.FirstOrDefault(x => x.popupType == popupType );
    }


    public void OpenCastlePopup(EnumDefinition.CastlePopupType popup) 
    {
        GetCastlePopupByType(popup).gameObject.SetActive(true);
    }

    public void UpGradeCastle(CastlePopupType type)
    {
        switch (type)
        {
            case CastlePopupType.mine:
                UpgradeMine((isSuccess) => {
                    if (isSuccess)
                    {
                         var popup = (MinePopup)GetCastlePopupByType(type);
                    }
                    else
                    {
                        // 골드 부족 POPUP
                    }
                });
                break;
            case CastlePopupType.factory:
                UpgradeFactory((isSuccess) => {
                    if (isSuccess)
                    {
                        var popup = (FactoryPopup)GetCastlePopupByType(type);
                    }
                    else
                    {
                        // 뼈조각 부족 POPUP
                    }
                });
                break;
            case CastlePopupType.camp:
                break;
            case CastlePopupType.lab:
                break;
            default:
                break;
        }
    }
    


    public void UpgradeMine(UnityAction<bool> completeCallback)
    {
        if (GlobalData.instance.player.coal >= BuildDataMine.price)
        {
            GlobalData.instance.player.PayCoal(BuildDataMine.price);
            mineLevel++;
            var refBuildDataMine = GlobalData.instance.dataManager.GetBuildDataMineByLevel(mineLevel);
            BuildDataMine = new CastleBuildingData().Create().SetGoodsType(GoodsType.gold).Clone(refBuildDataMine);
            completeCallback(true);
        }
        else
        {
            completeCallback(false);
        }
    }

    public void UpgradeFactory(UnityAction<bool> completeCallback)
    {
        if (GlobalData.instance.player.coal >= BuildDataFactory.price)
        {
            GlobalData.instance.player.PayCoal(BuildDataFactory.price);
            factoryLevel++;
            var refBuildDataFactory = GlobalData.instance.dataManager.GetBuildDataFactoryByLevel(factoryLevel);
            BuildDataFactory = new CastleBuildingData().Create().SetGoodsType(GoodsType.bone).Clone(refBuildDataFactory);
            completeCallback(true);
        }
        else
        {
            completeCallback(false);
        }
    }



    // 골드 채굴
    // CastleBuildingData 클래스를 기준으로 코루틴을 사용하여 productionTime 한번씩 productionCount을 totlaValue에 더해주고 maxSupplyAmount을 넘어가면 더이상 totlaValue에 더하지 않는다
    IEnumerator MiningGold(CastleBuildingData building)
    {
        while (true)
        {
            if(GlobalData.instance.player.coal >= building.price)
            {
                // 석탄 사용
                GlobalData.instance.player.PayCoal(building.price);
                // productionCount 만큼 totlaValue에 더하되, maxSupplyAmount를 넘지 않도록 한다.
                building.totlaMiningValue = Mathf.Min(building.totlaMiningValue + building.productionCount, building.maxSupplyAmount);      
                // set ui  
                 var popup = (MinePopup)GetCastlePopupByType(CastlePopupType.mine);
                 popup.SetTextTotalMiningValue(building.totlaMiningValue.ToString());
            }
            yield return new WaitForSeconds(building.productionTime);
        }
    }

    /// <summary> 골드 인출 </summary>
    void WithdrawGold()
    {
        int withdrawnGold = BuildDataMine.totlaMiningValue;
        Debug.Log("인출된 골드: " + withdrawnGold);
        GlobalData.instance.player.AddGold(withdrawnGold);
        BuildDataMine.totlaMiningValue = 0;
    }

    // 뼈조각 채굴
    IEnumerator MiningBone(CastleBuildingData building)
    {
        while (true)
        {
            if (GlobalData.instance.player.coal >= building.price)
            {
                GlobalData.instance.player.PayCoal(building.price);
                building.totlaMiningValue = Mathf.Min(building.totlaMiningValue + building.productionCount, building.maxSupplyAmount);
                // set ui  
                 var popup = (FactoryPopup)GetCastlePopupByType(CastlePopupType.factory);
                 popup.SetTextTotalMiningValue(building.totlaMiningValue.ToString());
            }
            yield return new WaitForSeconds(building.productionTime);
        }
    }

    /// <summary> 뼈조각 인출 </summary>
    void WithdrawBone()
    {
        int withdrawnBone = BuildDataFactory.totlaMiningValue;
        Debug.Log("인출된 뼈조각: " + withdrawnBone);
        GlobalData.instance.player.AddBone(withdrawnBone);
        BuildDataMine.totlaMiningValue = 0;
    }



}

[System.Serializable]
public class CastleBuildingData
{
    // 레벨
    public int level;
    // 골드 생산량
    public int productionCount;
    // 골드 최대 저장량 
    public int maxSupplyAmount;
    // 생산 시간
    public int productionTime;
    // 석탄 필요량
    public int price;
    public string currencyType;
    // 총 생산량
    public int totlaMiningValue;
     
     
    // 생산되는 재화 타입    
    EnumDefinition.GoodsType goodsType;

   public CastleBuildingData data;

    public CastleBuildingData  Create(){
        data = new CastleBuildingData();   
        return this; 
    }
    public CastleBuildingData SetGoodsType(EnumDefinition.GoodsType goodsType)
    {
        data.goodsType = goodsType;
        return this;
    }

    public CastleBuildingData Clone(MineAndFactoryBuildingData mineAndFactoryBuildingData)
    {
        data.level = mineAndFactoryBuildingData.level;
        data.productionCount = mineAndFactoryBuildingData.productionCount;
        data.maxSupplyAmount = mineAndFactoryBuildingData.maxSupplyAmount;
        data.productionTime = mineAndFactoryBuildingData.productionTime;
        data.price = mineAndFactoryBuildingData.price;
        return data;
    }
}