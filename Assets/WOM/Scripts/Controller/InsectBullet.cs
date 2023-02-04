using System.Collections;
using UnityEngine;

/// <summary>
/// ���� ���� �̵� �ϴ� �߻�ü ( ���� )
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
        // ���� ���� ���¿����� �ִϸ��̼� ����
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
            // TODO : DATA �� SPEED ����
            animData.animTime = ((Time.time - animData.animStartTime) / animData.animDuration);  //* speed;
            animData.animValue = EaseValues.instance.GetAnimCurve(animData.animCurveType, animData.animTime);

            // �����̵�
            transform.position = Vector3.Lerp(animPoints.startPoint, animPoints.targetPoint, animData.animValue);

            // ȸ��
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

            // �Ҹ�
            gameObject.SetActive(false);
            // monster hit event!
            if(insectType == EnumDefinition.InsectType.union)
                EventManager.instance.RunEvent(CallBackEventType.TYPES.OnMonsterHit, insectType, inGameData.unionIndex);
            else
                EventManager.instance.RunEvent(CallBackEventType.TYPES.OnMonsterHit, insectType);
        }
    }

    // ��� ������ ���� ���ڱ� ������� �� ��
    public void DisableInsect()
    {
        // ��ƼŬ ����Ʈ �߰�
        GlobalData.instance.effectManager.EnableInsectDisableEffect(this.transform);
        gameObject.SetActive(false);
    }

    public void SetInsectFace(Sprite insectFace)
    {
        spriteRenderer.sprite = insectFace; 
    }

    
}


