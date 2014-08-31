using UnityEngine;
using System.Collections;


public class CarScript : MonoBehaviour
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

    public Vector2 PositionCurrent
    {
        get
        {
            return transform.position;
        }
    }

    private Vector2 _positionCurrent;
    private Vector2 _positionPrevious;

    public float MovementRate
    {
        get
        {
            return _bubble.Size;
        }
    }



    private Bubble _bubble;

    private void Start()
    {
        _bubble = GetComponent<Bubble>();

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
        _bubble.rate = 1.0f + radiusMaxDelta * rate;
    }

}