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
        
        // 골드 채굴 시작
        StartCoroutine(MiningGold());
        // 뼈조각 채굴 시작 
        StartCoroutine(MiningBone(BuildDataFactory));

          yield return null;
    }


    void SetCastleData()
    {
        var refBuildDataMine = GlobalData.instance.dataManager.GetBuildDataMineByLevel(mineLevel);
        var refBuildDataFactory = GlobalData.instance.dataManager.GetBuildDataFactoryByLevel(factoryLevel);

        BuildDataMine = new CastleBuildingData().Create().SetGoodsType(GoodsType.gold).Clone(refBuildDataMine);
        BuildDataFactory = new CastleBuildingData().Create().SetGoodsType(GoodsType.bone).Clone(refBuildDataFactory);

        // SET UI , TODO: 저장된 데이터에서 불러와야 함.
        // 초기 UI 설정
        var minePopup = (MinePopup)GetCastlePopupByType(CastlePopupType.mine);
        minePopup.InitUIText(BuildDataMine);
        var factoryPopup =(MinePopup)GetCastlePopupByType(CastlePopupType.factory);
        factoryPopup.InitUIText(BuildDataFactory);
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
            case CastlePopupType.factory:
                UpgradeMine((isSuccess, upgradeData) =>
                {
                    if (isSuccess)
                    {
                        var popup = (MinePopup)GetCastlePopupByType(type);
                        var nextLevelData = GlobalData.instance.dataManager.GetBuildDataMineByLevel(mineLevel + 1);
                        CastleBuildingData nextBuildData = null;
                        if (nextLevelData != null)
                        {
                            nextBuildData = new CastleBuildingData().Create().SetGoodsType(GoodsType.gold).Clone(nextLevelData);
                        }
                        popup.SetUpGradeText(upgradeData, nextBuildData);

                        // 성공 로그
                        Debug.Log("Upgrade Success " + type);
                    }
                    else
                    {
                        // 석탄 부족 POPUP
                        // 실패 로그
                        Debug.Log("Upgrade Fail");
                    }
                });
                break;
            // case CastlePopupType.factory:
            //     UpgradeFactory((isSuccess) =>
            //     {
            //         if (isSuccess)
            //         {
            //             // 팩토리 업그레이드 성공 처리
            //         }
            //         else
            //         {
            //             // 뼈조각 부족 POPUP
            //         }
            //     });
            //     break;
            case CastlePopupType.camp:
            case CastlePopupType.lab:
            default:
                break;
        }
    }



/*
 * UpgradeMine - 광산 업그레이드 메소드
 * 
 * @param completeCallback: UnityAction<bool, CastleBuildingData> 타입의 콜백 함수. 업그레이드 성공 여부와 다음 레벨 정보를 전달합니다.
 */
public void UpgradeMine(UnityAction<bool,CastleBuildingData > completeCallback)
{
    // 플레이어가 가진 coal(resource)이 광산의 가격보다 많을 때 업그레이드 진행
    if (GlobalData.instance.player.coal >= BuildDataMine.price)
    {
        // 가격만큼 resource 차감 후 레벨 업그레이드 진행
        GlobalData.instance.player.PayCoal(BuildDataMine.price);
        mineLevel++;

        // 다음 레벨의 광산 정보 가져오기
        var refBuildDataMine = GlobalData.instance.dataManager.GetBuildDataMineByLevel(mineLevel);

        // Clone 메소드를 이용하여 BuildDataMine 객체의 데이터 갱신
        BuildDataMine = new CastleBuildingData().Create().SetGoodsType(GoodsType.gold).Clone(refBuildDataMine);

        // 업그레이드 성공 처리를 위해 completeCallback 호출
        completeCallback(true, BuildDataMine);
    }
    else
    {
        // Coal(resource) 부족으로 업그레이드 실패 시 completeCallback 호출
        completeCallback(false,null);
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
    // 이 함수는 CastleBuildingData를 인자로 받아 골드 채굴을 하는 IEnumerator입니다.
    IEnumerator MiningGold()
    {
        while (true)
        {
            // 이 조건문은 player의 coal이 충분한지 검사합니다.
            if (GlobalData.instance.player.coal >= BuildDataMine.price && BuildDataMine.level > 0)  // level이 0이면 채굴 불가
            {
                // 아래 두 줄은 player의 coal을 사용하여 채굴하고, productionCount만큼 totlaMiningValue를 업데이트합니다. 단, maxSupplyAmount를 넘지 않도록 합니다.
                GlobalData.instance.player.PayCoal(BuildDataMine.price);
                BuildDataMine.totlaMiningValue = Mathf.Min(BuildDataMine.totlaMiningValue + BuildDataMine.productionCount, BuildDataMine.maxSupplyAmount);

                // MinePopup UI를 설정하고 현재의 totalMiningValue를 팝업에 표시합니다. 
                var popup = (MinePopup)GetCastlePopupByType(CastlePopupType.mine);
                popup.SetTextTotalMiningValue(BuildDataMine.totlaMiningValue.ToString());

                Debug.Log("채굴된 골드: " + BuildDataMine.totlaMiningValue + " 남은 시간: " + BuildDataMine.productionTime);
            }

            // 해당 시간만큼 대기 후 다시 while문을 반복합니다.
            //yield return new WaitForSeconds(building.productionTime);
            yield return new WaitForSeconds(1f);
        }
    }


    /// <summary> 골드 인출 </summary>
    // 이 함수는 골드 인출 버튼을 눌렀을 때 호출됩니다.
    public void WithdrawGold()
    {
        // BuildDataMine이 가지고 있는 총 채굴량을 withdrawnGold 변수에 저장합니다.
        int withdrawnGold = BuildDataMine.totlaMiningValue;

        // Debug.Log를 사용하여 인출된 골드 양을 디버그 창에 출력합니다.
        Debug.Log("인출된 골드: " + withdrawnGold);

        // GlobalData의 instance에서 player를 가져와, AddGold() 함수를 사용하여 player의 소지금에 withdrawnGold만큼 추가합니다.
        GlobalData.instance.player.AddGold(withdrawnGold);

        // 모든 금 채굴량을 인출했으므로 BuildDataMine 객체의 totlaMiningValue를 0으로 설정합니다. 
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
                 var popup = (MinePopup)GetCastlePopupByType(CastlePopupType.factory);
                 popup.SetTextTotalMiningValue(building.totlaMiningValue.ToString());
            }
            yield return new WaitForSeconds(building.productionTime);
        }
    }

    /// <summary> 뼈조각 인출 </summary>
    public void WithdrawBone()
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