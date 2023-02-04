using System.Collections;
using UnityEngine;

/// <summary>
/// 몬스터 향해 이동 하는 발사체 ( 공격 )
/// </summary>
public class InsectBullet : MonoBehaviour
{
    public EnumDefinition.InsectType insectType;
    public AnimData animData;
    public ParticleSystem effDisable;
    public SpriteRenderer spriteRenderer;    
    
    float speed = 1;
    Vector3 lookDir;

    // UNION
    public UnionInGameData inGameData;

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

    float GetSpeed()
    {
        if(insectType == EnumDefinition.InsectType.union)
        {
            return inGameData.moveSpeed;
        }
        else
        {
            return GlobalData.instance.insectManager.GetInsect(insectType).speed;
        }
    }

    IEnumerator AttackAnim()
    {
        animData.ResetAnimData();
        var animPoints = GetAnimPoints();
        speed = GetSpeed();
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
        points.Item2 = GetRandomMonsterPoint();
        return points;
    }
    Vector3 GetRandomMonsterPoint()
    {
        var monPos = GlobalData.instance.player.currentMonster.transform.position;
        var x = Random.Range(monPos.x - 1, monPos.x + 1);
        return new Vector3(x, monPos.y, monPos.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("monster"))
        {
            // Attack Effect Enable
            GlobalData.instance.effectManager.EnableAttackEffectByInsectType(insectType, this.transform);

            // 소멸
            gameObject.SetActive(false);
            // monster hit event!
            if(insectType == EnumDefinition.InsectType.union)
                EventManager.instance.RunEvent(CallBackEventType.TYPES.OnMonsterHit, insectType, inGameData.unionIndex);
            else
                EventManager.instance.RunEvent(CallBackEventType.TYPES.OnMonsterHit, insectType);
        }
    }

    // 어떠한 이유로 곤충 갑자기 사라져야 할 때
    public void DisableInsect()
    {
        // 파티클 이펙트 추가
        GlobalData.instance.effectManager.EnableInsectDisableEffect(this.transform);
        gameObject.SetActive(false);
    }

    public void SetInsectFace(Sprite insectFace)
    {
        spriteRenderer.sprite = insectFace; 
    }

    
}


