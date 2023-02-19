using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    bool timerActive = false;
    float currTimeInSeconds;

    void Update()
    {
        if (timerActive)
            currTimeInSeconds += Time.deltaTime;
    }

    public void StartTimer()
    {
        currTimeInSeconds = 0;
        timerActive = true;
    }

    public float EndTimer()
    {
        timerActive = false;
        return currTimeInSeconds;
    }
}
