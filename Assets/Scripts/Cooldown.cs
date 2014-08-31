using UnityEngine;
using System.Collections;


public class Cooldown
{
    private float _time = 0.0f;

    public void StartTimer(float time)
    {
        _time = time;
    }

    public void Step()
    {
        if (!isReady())
        {
            _time -= Time.deltaTime;
        }
    }

    public bool isReady()
    {
        return _time <= 0.0f;
    }
}
