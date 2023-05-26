using UnityEngine;
using System;

public class QuestResetTimer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private const string CURRENT_TIME_KEY = "current_time";
    private const string MIDNIGHT_TIME_KEY = "midnight_time";

    void SaveCurrentTime()
    {
        // 현재 시간을 가져온다.
        DateTime now = DateTime.Now;

        // PlayerPrefs에 현재 시간을 문자열로 저장한다.
        PlayerPrefs.SetString(CURRENT_TIME_KEY, now.ToString());

        //PlayerPrefs.Save();
    }

    void SaveMidnightTime()
    {
        // 오늘 자정 시간을 계산한다.
        DateTime midnight = DateTime.Today.AddDays(1);

        // PlayerPrefs에 오늘 자정 시간을 문자열로 저장한다.
        PlayerPrefs.SetString(MIDNIGHT_TIME_KEY, midnight.ToString());

        //PlayerPrefs.Save();
    }

    DateTime LoadCurrentTime()
    {
        // PlayerPrefs에서 저장된 시간 정보를 불러온다.
        string currentTimeStr = PlayerPrefs.GetString(CURRENT_TIME_KEY);

        // 불러온 문자열 시간 정보를 DateTime 형식으로 변환한다.
        DateTime currentTime = DateTime.Parse(currentTimeStr);

        return currentTime;
    }

    bool HasCrossedMidnight()
    {
        // 현재 시간과 저장된 오늘 자정 시간을 비교한다.
        DateTime currentTime = LoadCurrentTime();
        DateTime midnight = DateTime.Parse(PlayerPrefs.GetString(MIDNIGHT_TIME_KEY));

        if (currentTime > midnight)
        {
            // 저장된 오늘 자정 시간 이후라면 true를 반환한다.
            return true;
        }
        else
        {
            // 저장된 오늘 자정 시간 이전이라면 false를 반환한다.
            return false;
        }
    }
}
