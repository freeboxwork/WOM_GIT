

public class CallBackEventType
{
    public enum TYPES
    {
        OnMonsterHit,                   /// <summary> ������ ���͸� Ÿ�� ������  </summary>
        OnBossMonsterChallenge,         /// <summary> ���� ���� ���� ��ư�� ��������  </summary>
        OnBossMonsterChallengeTimeOut,  /// <summary> ���� ���� ���� Ÿ�̸� ���� �Ǿ�����  </summary>
        OnEvolutionMonsterChallenge,    /// <summary> ��ȭ�� ��ư ��������  </summary>
        OnReloaUITranuing,              /// <summary> �Ʒ� UI �ݾ׿� ���� ��ư Ȱ�� , ��Ȱ��ȭ ��ų��  </summary>
        OnReloaUISkill,                 /// <summary> ��ų UI �ݾ׿� ���� ��ư Ȱ�� , ��Ȱ��ȭ ��ų��  </summary>
        OnDungeonMonsterChallenge,      /// <summary> ���� ���� ���� ��ư�� ��������  </summary>
        OnDungeonMonsterHit,            /// <summary> ������ ���� ���͸� Ÿ�� ������  </summary>
        OnQusetClearOneDayCounting,     /// <summary> ���� ����Ʈ �Ϸ� �Ͽ� ī��Ʈ �Ҷ�  </summary>
        OnQusetUsingRewardOneDay,       /// <summary> ���� ����Ʈ ���� �޾�����  </summary>
        OnQusetClearRepeatCounting,     /// <summary> �ݺ� ����Ʈ �Ϸ� ������  </summary>

        None,
    }
}
