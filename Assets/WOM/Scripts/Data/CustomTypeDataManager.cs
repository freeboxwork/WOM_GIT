using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class CustomTypeDataManager : MonoBehaviour
{
    

    public List<CustomTypeData> customTypeDatas = new List<CustomTypeData>();

    // TODO : 타입별로 셋팅 진행
    public List<CustomTypeData> customTypeDatas_animCont = new List<CustomTypeData>();
    public List<CustomTypeData> customTypeDatas_uiButton = new List<CustomTypeData>();
    public List<CustomTypeData> customTypeDatas_uiText = new List<CustomTypeData>();
    public List<CustomTypeData> customTypeDatas_uiImage = new List<CustomTypeData>();
    public List<CustomTypeData> customTypeDatas_gameObject = new List<CustomTypeData>();

    void Start()
    {
        
    }


    public CustomTypeData GetCustomTypeData(EnumDefinition.CustomDataType customType, int index)
    {
        var data = customTypeDatas?.FirstOrDefault(f => f.customType == customType && f.index == index);
        if (data != null)
            return data;
        else
            Debug.LogError($"{customType}_{index} custom type data 가 없습니다..");
        return null;
    }

    public Button GetCustomTypeData_Button(int index)
    {
        var data = customTypeDatas_uiButton?.FirstOrDefault(f=> f.index == index).components.button;
        if (data != null)
            return data;
        else
            Debug.LogError($"{EnumDefinition.CustomDataType.button}_{index} custom type data 가 없습니다.");
        return null;
    }

    public TextMeshProUGUI GetCustomTypeData_Text(int index)
    {
        var data = customTypeDatas_uiText?.FirstOrDefault(f => f.index == index).components.text;
        if (data != null)
            return data;
        else
            Debug.LogError($"{EnumDefinition.CustomDataType.text}_{index} custom type data 가 없습니다.");
        return null;
    }

    public Image GetCustomTypeData_Image(int index)
    {
        var data = customTypeDatas_uiImage?.FirstOrDefault(f => f.index == index).components.image;
        if (data != null)
            return data;
        else
            Debug.LogError($"{EnumDefinition.CustomDataType.image}_{index} custom type data 가 없습니다.");
        return null;
    }

    public GameObject GetCustomTypeData_GameOnject(int index)
    {
        var data = customTypeDatas_gameObject?.FirstOrDefault(f => f.index == index).components.gm;
        if (data != null)
            return data;
        else
            Debug.LogError($"{EnumDefinition.CustomDataType.gm}_{index} custom type data 가 없습니다.");
        return null;
    }


    public AnimationController GetCustomTypeData_AnimCont(int index)
    {
        var data = customTypeDatas_animCont?.FirstOrDefault(f => f.index == index).components.animController;
        if (data != null)
            return data;
        else
            Debug.LogError($"{EnumDefinition.CustomDataType.animCont}_{index}  custom type data 가 없습니다.");
        return null;
    }
   
}
