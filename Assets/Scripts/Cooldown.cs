﻿using System.Collections;


public class Cooldown
{
    public float Value
    {
        get
        {
            return _time;
        }
    }

    private float _time = 0.0f;

    public void StartTimer(float time)
    {
        _time = time;
    }

    public void Step(float deltaTime, float additionalRate = 1.0f)
    {
        if (!isReady())
        {
            _time -= deltaTime * additionalRate;
        }
    }

    public bool isReady()
    {
        return _time <= 0.0f;
    }
}
