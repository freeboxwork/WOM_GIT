using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityMethod 
{

    public static T CustomGetComponet<T>() where T : UnityEngine.Object
    {
        var returnValue = GameObject.FindObjectOfType<T>();
        if (returnValue == null) Debug.Log($"���� �������� �ʴ� Ÿ�� �Դϴ�.");
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
            //Debug.LogError($"{gameObject.name} ���� ������Ʈ�� ������Ʈ Ÿ���� �����ϴ�.");
            return null;
        }
    }

}
