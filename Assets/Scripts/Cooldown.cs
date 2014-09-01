using UnityEngine;
using System.Collections;


public class Cooldown
{
    private float _time = 0.0f;

    public void StartTimer(float time)
    {
        _time = time;
    }

    public void Step(float additionalRate = 1.0f)
    {
        if (!isReady())
        {
            _time -= Time.deltaTime * additionalRate;
        }
    }

    public bool isReady()
    {
        return _time <= 0.0f;
    }
}
