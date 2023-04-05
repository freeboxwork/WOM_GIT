using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinePopup : CastlePopupBase
{
    public TextMeshProUGUI productionCountText;//���귮
    public TextMeshProUGUI maxSupplyText;//�ִ����差
    public TextMeshProUGUI productionTimeText;//����ð�
    public TextMeshProUGUI levelText;//����
    public TextMeshProUGUI priceText;//���׷��̵� ��ź ���
    public TextMeshProUGUI totlaMiningValue; //�� ä����
    public Button btnGetGold;
    public Button btnUpgrade;

    //���� Ŭ���� TextMeshProUGUI Ÿ���� �ɹ������� ���� text ���� ���� �����ϴ� ���� �Լ�
    public void SetTextTotalMiningValue(string text)
    {
        totlaMiningValue.text = text;
    }
    public void SetTextProductionCount(string text)
    {
        productionCountText.text = text;
    }
    public void SetTextMaxSupply(string text)
    {
        maxSupplyText.text = text;
    }
    public void SetTextProductionTime(string text)
    {
        productionTimeText.text = text;
    }
    public void SetTextLevel(string text)
    {
        levelText.text = text;
    }
    public void SetTextPrice(string text)
    {
        priceText.text = text;
    }

  
    private void Start()
    {
        SetButtonEvents();
    }

    void SetButtonEvents()
    {
        btnGetGold.onClick.AddListener(() =>
        {
            GlobalData.instance.castleManager.WithdrawGold();
        });

        btnUpgrade.onClick.AddListener(() =>
        {
            GlobalData.instance.castleManager.UpGradeCastle(popupType);
        });
    }
     


    //CastleBuildingData ��ü�� ���ڷ� �޾Ƽ� ������ �ɹ������� text ���� �����ϴ� �Լ�
    /*
    public void SetUpGradeText(CastleBuildingData data, CastleBuildingData nextLevelData =null)
    {

        // ���귮 = ���� ������ ���귮 - ���� ������ ���귮
        var productionCount = data.productionCount - nextLevelData.productionCount;
        // �ִ� ���差 = ���� ������ ���差 - ���� ������ ���差
        var maxSupply = data.maxSupplyAmount - nextLevelData.maxSupplyAmount;
        // ����ð� = ���� ������ ����ð� - ���� ������ ����ð�
        var productionTime = nextLevelData.productionTime - data.productionTime;

        string _productionCountText = string.Empty;
        string _maxSupplyText = string.Empty; ;
        string _productionTimeText = string.Empty; ;
        string _levelText = string.Empty; 
        string _priceText = string.Empty; 
      
        if(nextLevelData != null)
        {
           _productionCountText = $"{data.productionCount} / {productionCount}";
           _maxSupplyText = $"{data.maxSupplyAmount} / {maxSupply}";
           _productionTimeText = $"{data.productionTime} / {productionTime}";
           _levelText = $"{data.level} > {nextLevelData.level}";
           _priceText = nextLevelData.price.ToString();
        }
        else
        {
            _productionCountText = $"{data.productionCount}";
            _maxSupplyText = $"{data.maxSupplyAmount}";
            _productionTimeText = $"{data.productionTime}";
            _levelText = $"{data.level}";
            _priceText = data.price.ToString(); 
        }

        SetTextProductionCount(_productionCountText);
        SetTextMaxSupply(_maxSupplyText);
        SetTextProductionTime(_productionTimeText);
        SetTextLevel(_levelText);
        SetTextPrice(data.price.ToString());
       
    }
    */

    //CastleBuildingData ��ü�� ���ڷ� �޾Ƽ� ������ �ɹ������� text ���� �����ϴ� �Լ�
    public void SetUpGradeText(CastleBuildingData data, CastleBuildingData nextLevelData = null)
    {
        // ���귮, �ִ� ���差, ���� �ð� ���
        var productionCount = (nextLevelData != null) ? data.productionCount - nextLevelData.productionCount : 0;
        var maxSupply = (nextLevelData != null) ? data.maxSupplyAmount - nextLevelData.maxSupplyAmount : 0;
        var productionTime = (nextLevelData != null) ? nextLevelData.productionTime - data.productionTime : 0;

        // ���ڿ� �ʱⰪ ����
        string _productionCountText = $"{data.productionCount}";
        string _maxSupplyText = $"{data.maxSupplyAmount}";
        string _productionTimeText = $"{data.productionTime}";
        string _levelText = $"{data.level}";
        string _priceText = data.price.ToString();

        // ���� ���� ������ �����ϴ� ��� ���ڿ��� ������Ʈ
        if (nextLevelData != null)
        {
            _productionCountText += $" / {productionCount}";
            _maxSupplyText += $" / {maxSupply}";
            _productionTimeText += $" / {productionTime}";
            _levelText += $" > {nextLevelData.level}";
            _priceText = nextLevelData.price.ToString();
        }

        // UI�� ���� ����
        SetTextProductionCount(_productionCountText);
        SetTextMaxSupply(_maxSupplyText);
        SetTextProductionTime(_productionTimeText);
        SetTextLevel(_levelText);
        SetTextPrice(_priceText);
    }

    //CastleBuildingData ��ü�� ���ڷ� �޾Ƽ� ������ �ɹ������� text ���� �����ϴ� �Լ�
    public void InitUIText(CastleBuildingData data)
    {
        // ���귮, �ִ� ���差, ���� �ð� ���
        var productionCount = data.productionCount;
        var maxSupply = data.maxSupplyAmount;
        var productionTime = data.productionTime;

        // ���ڿ� �ʱⰪ ����
        string _productionCountText = $"{data.productionCount}";
        string _maxSupplyText = $"{data.maxSupplyAmount}";
        string _productionTimeText = $"{data.productionTime}";
        string _levelText = $"{data.level}";
        string _priceText = data.price.ToString();

        // UI�� ���� ����
        SetTextProductionCount(_productionCountText);
        SetTextMaxSupply(_maxSupplyText);
        SetTextProductionTime(_productionTimeText);
        SetTextLevel(_levelText);
        SetTextPrice(_priceText);

        //data �� ��� ������ ���
        Debug.Log($"data.productionCount : {data.productionCount}");
        Debug.Log($"data.maxSupplyAmount : {data.maxSupplyAmount}");
        Debug.Log($"data.productionTime : {data.productionTime}");
        Debug.Log($"data.level : {data.level}");
        Debug.Log($"data.price : {data.price}");


    }


}
