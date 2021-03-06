﻿using UnityEngine;
using System.Collections;


public class Car : MonoBehaviour
{
    public float radiusMaxDelta = 0.5f;

    public Vector2 PositionDelta
    {
        get
        {
            return PositionCurrent - PositionPrevious;
        }
    }

    public Vector2 PositionPrevious
    {
        get
        {
            return _positionPrevious;
        }
    }

    public Vector2 CurrentDirection
    {
        get
        {
            return null == _bubble ?
                   Vector2.zero :
                   _bubble.CurrentDirection;
        }
    }

    public Vector2 PositionCurrent
    {
        get
        {
            return transform.position;
        }
    }

    private Vector2 _positionCurrent;
    private Vector2 _positionPrevious;

    private Bubble _bubble;

    private void Start()
    {
        if (null == _bubble)
        {
            _bubble = GetComponent<Bubble>();
        }

        _positionCurrent = PositionCurrent;
        _positionPrevious = _positionCurrent;
    }

    private void Update()
    {
        _positionPrevious = _positionCurrent;
        _positionCurrent = PositionCurrent;
    }
    
    public void ChangeRadiusTo(float rate)
    {
        if (null == _bubble)
        {
            _bubble = GetComponent<Bubble>();
        }
        _bubble.rate = 1.0f + radiusMaxDelta * rate;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        FinishLine finishLine = other.GetComponent<FinishLine>();
        if (finishLine != null)
        {
            bool complete = finishLine.CompleteLap(this);
            if (complete)
            {
                BubbleController controller = GetComponent<BubbleController>();
                controller.Reset();
                controller.enabled = false;
            }
            else
            {
            }
        }
    }

}