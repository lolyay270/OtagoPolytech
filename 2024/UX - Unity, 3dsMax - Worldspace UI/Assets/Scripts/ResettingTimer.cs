using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResettingTimer : MonoBehaviour
{
    public bool isRunning;

    float _secs;
    Coroutine _timer;

    /// <summary>
    /// Timer that resets if called again
    /// </summary>
    /// <param name="secs">length of timer in seconds</param>
    public void StartTimer(float secs)
    {
        _secs = secs;

        if (_timer != null) StopCoroutine(_timer);
        _timer = StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        isRunning = true;
        yield return new WaitForSeconds(_secs);
        isRunning = false;
        _timer = null;
    }
}
