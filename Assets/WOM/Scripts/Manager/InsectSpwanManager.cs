using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectSpwanManager : MonoBehaviour
{
    /// <summary> 0: y , 1 : x min , 2 : x max </summary>
    public Transform[] randomPoint;
    public List<InsectSpwanTimer> spwanTimers = new List<InsectSpwanTimer>();


    void Start()
    {
        
    }

    IEnumerator Init()
    {
        yield return null;
    }

    public void StartInsectSpwan()
    {
        foreach (var timer in spwanTimers)
            timer.TimerStart();
    }


    #region UTILITY METHOD
    public Vector3 GetRandomPos()
    {
        return new Vector3(GetPosX(), GetPosY(), 0);
    }
    float GetPosY()
    {
        return randomPoint[0].position.y;
    }

    float GetPosX()
    {
        var xMinx = randomPoint[1].position.x;
        var xMax = randomPoint[2].position.x;
        return Random.Range(xMinx, xMax);
    }
    #endregion


}
