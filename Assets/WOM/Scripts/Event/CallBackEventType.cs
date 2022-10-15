

public class CallBackEventType 
{
    public enum TYPES
    {
        OnMonsterHit,            /// <summary> 곤충이 몬스터를 타격 했을때  </summary>
        OnBossMonsterChallenge,  /// <summary> 보스 몬스터 도전 버튼을 눌렀을때  </summary>
        OnBossMonsterChallengeTimeOut, /// <summary> 보스 몬스터 도전 타이머 종료 되었을때  </summary>

        None,
    }
}
 