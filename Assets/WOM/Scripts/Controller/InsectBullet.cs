using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���� �̵� �ϴ� �߻�ü ( ���� )
/// </summary>
public class InsectBullet : MonoBehaviour
{
    public EnumDefinetion.InsectType insectType;
        

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    public void SetInsectType(EnumDefinetion.InsectType insectType)
    {
        this.insectType = insectType;
    } 

    IEnumerator Attack()
    {
        yield return null;
    }



}
