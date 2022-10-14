

public class CallBackEventType 
{
    public enum TYPES
    {
        OnMonsterHit,            /// <summary> 곤충이 몬스터에 닿았을때  </summary>
        OnMonsterKill,           /// <summary> 몬스터가 사망 했을때  </summary>
        OnMonsterUiReset,        /// <summary> 몬스터관련 UI 가 리셋 될때  </summary>
        OnBossMonsterChallenge,  /// <summary> 보스 몬스터 도전 버튼을 눌렀을때  </summary>
        OnBossMonsterChallengeTimerEnd, /// <summary> 보스 몬스터 도전 타이머 종료 되었을때  </summary>

        None,
    }
}
 