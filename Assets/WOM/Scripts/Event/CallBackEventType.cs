

public class CallBackEventType 
{
    public enum TYPES
    {
        OnMonsterHit,            /// <summary> ������ ���Ϳ� �������  </summary>
        OnMonsterKill,           /// <summary> ���Ͱ� ��� ������  </summary>
        OnMonsterUiReset,        /// <summary> ���Ͱ��� UI �� ���� �ɶ�  </summary>
        OnBossMonsterChallenge,  /// <summary> ���� ���� ���� ��ư�� ��������  </summary>
        OnBossMonsterChallengeTimerEnd, /// <summary> ���� ���� ���� Ÿ�̸� ���� �Ǿ�����  </summary>

        None,
    }
}
 