using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public static class UtilityMethod 
{

    public static T CustomGetComponet<T>() where T : UnityEngine.Object
    {
        var returnValue = GameObject.FindObjectOfType<T>();
        if (returnValue == null) Debug.Log($"씬에 존재하지 않는 타입 입니다.");
        return returnValue;
    }

    public static T CustomGetComponetMy<T>(GameObject gameObject) where T : UnityEngine.Object
    {
        if (gameObject.TryGetComponent(out T value))
        {
            return value;
        }
        else
        {
            //Debug.LogError($"{gameObject.name} 게임 오브젝트에 컴포넌트 타입이 없습니다.");
            return null;
        }
    }

    public static float GetRandomRangeValue_X(Vector2 point, float rv)
    {
        return Random.Range(point.x - rv, point.x + rv);
    }
    public static float GetRandomRangeValue_Y(Vector2 point, float rv)
    {
        return Random.Range(point.y - rv, point.y + rv);
    }

    public static Vector2 GetTargetRandomRangeValue_V2(Vector2 x_min, Vector2 x_max, Vector2 y_min, Vector2 y_max)
    {
        var x = Random.Range(x_min.x, x_max.x);
        var y = Random.Range(y_min.y, y_max.y);
        return new Vector2(x, y);
    }


    public static void SetTxtCustomTypeByID(int id, string value)
    {
        var txtObj = GlobalData.instance.customTypeDataManager.GetCustomTypeData_Text(id);
        if (txtObj != null)
        {
            txtObj.text = value;
        }
        else
        {
            Debug.LogError($"Custom Type Object - Text - {id} 오브젝트가 없습니다.");
        }
    }

    public static void SetBtnEventCustomTypeByID(int id , UnityAction action)
    {
        var btn = GlobalData.instance.customTypeDataManager.GetCustomTypeData_Button(id);
        if(btn != null)
        {
            btn.onClick.AddListener(action);
        }
        else
        {
            Debug.LogError($"Custom Type Object - Button - {id} 오브젝트가 없습니다.");
        }
    }

    public static Button GetCustomTypeBtnByID(int id)
    {
        return GlobalData.instance.customTypeDataManager.GetCustomTypeData_Button(id);  
    }
}
