using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���� �̵� �ϴ� �߻�ü ( ���� )
/// </summary>
public class InsectBullet : MonoBehaviour
{
    public EnumDefinition.InsectType insectType;
        

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    public void SetInsectType(EnumDefinition.InsectType insectType)
    {
        this.insectType = insectType;
    } 

    IEnumerator Attack()
    {
        yield return null;
    }



}
