using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CustomTypeDataManager : MonoBehaviour
{
    

    public List<CustomTypeData> customTypeDatas = new List<CustomTypeData>();

    // TODO : 타입별로 셋팅 진행
    public List<CustomTypeData> customTypeDatas_animCont = new List<CustomTypeData>();
    public List<CustomTypeData> customTypeDatas_uiButton = new List<CustomTypeData>();
    public List<CustomTypeData> customTypeDatas_uiText = new List<CustomTypeData>();
    public List<CustomTypeData> customTypeDatas_uiImage = new List<CustomTypeData>();

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

    public CustomTypeData GetCustomTypeData_Button(int index)
    {
        var data = customTypeDatas_uiButton?.FirstOrDefault(f=> f.index == index);
        if (data != null)
            return data;
        else
            Debug.LogError($"{EnumDefinition.CustomDataType.button}_{index} custom type data 가 없습니다.");
        return null;
    }

    public CustomTypeData GetCustomTypeData_Text(int index)
    {
        var data = customTypeDatas_uiText?.FirstOrDefault(f => f.index == index);
        if (data != null)
            return data;
        else
            Debug.LogError($"{EnumDefinition.CustomDataType.button}_{index} custom type data 가 없습니다.");
        return null;
    }

    public CustomTypeData GetCustomTypeData_Image(int index)
    {
        var data = customTypeDatas_uiImage?.FirstOrDefault(f => f.index == index);
        if (data != null)
            return data;
        else
            Debug.LogError($"{EnumDefinition.CustomDataType.button}_{index} custom type data 가 없습니다.");
        return null;
    }

    public CustomTypeData GetCustomTypeData_AnimCont(int index)
    {
        var data = customTypeDatas_animCont?.FirstOrDefault(f => f.index == index);
        if (data != null)
            return data;
        else
            Debug.LogError($"{EnumDefinition.CustomDataType.image}_{index}  custom type data 가 없습니다.");
        return null;
    }
   
}
