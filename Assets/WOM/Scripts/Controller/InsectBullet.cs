using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

/// <summary>
/// ���� ���� �̵� �ϴ� �߻�ü ( ���� )
/// </summary>
public class InsectBullet : MonoBehaviour
{
    public EnumDefinition.InsectType insectType;
    public AnimData animData;
         
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("monster"))
        {
            // monster hit!
            
            // �Ҹ�

        }
    }



}
