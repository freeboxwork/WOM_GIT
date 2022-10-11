using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

/// <summary>
/// 몬스터 향해 이동 하는 발사체 ( 공격 )
/// </summary>
public class InsectBullet : MonoBehaviour
{
    public EnumDefinition.InsectType insectType;
    public AnimData animData;
    
    float speed = 1;
    Vector3 lookDir;
    void Start()
    {
        
    }
    
    public void SetInsectType(EnumDefinition.InsectType insectType)
    {
        this.insectType = insectType;
    }

    private void OnEnable()
    {
        // 공격 가능 상태에서만 애니메이션 진행
        if (GlobalData.instance.attackController.GetAttackableState() == true)
            StartCoroutine(AttackAnim());
    }


    IEnumerator AttackAnim()
    {
        animData.ResetAnimData();
        var animPoints = GetAnimPoints();
        speed = GlobalData.instance.insectManager.GetInsect(insectType).speed;
        while (animData.animValue < 0.99f)
        {
            // TODO : DATA 의 SPEED 적용
            animData.animTime = ((Time.time - animData.animStartTime) / animData.animDuration);  //* speed;
            animData.animValue = EaseValues.instance.GetAnimCurve(animData.animCurveType, animData.animTime);

            // 직선이동
            transform.position = Vector3.Lerp(animPoints.startPoint, animPoints.targetPoint, animData.animValue);

            // 회전
            lookDir = (animPoints.targetPoint - transform.position);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f));
            yield return null;

        }
        gameObject.SetActive(false);
    }

    (Vector3 startPoint, Vector3 targetPoint) GetAnimPoints()
    {
        (Vector3, Vector3) points;
        points.Item1 = transform.position;
        points.Item2 = GlobalData.instance.player.currentMonster.transform.position;
        return points;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("monster"))
        {
            // monster hit event!
            EventManager.instance.RunEvent(CallBackEventType.TYPES.OnMonsterHit, insectType);

            // 소멸
            gameObject.SetActive(false);

        }
    }



}
