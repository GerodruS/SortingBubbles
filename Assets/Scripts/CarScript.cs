using UnityEngine;
using System.Collections;

public class CarScript : MonoBehaviour
{
    public Vector2 PreviousPosition
    {
        get
        {
            return _previousPosition;
        }
    }

    public Vector2 CurrentPosition
    {
        get
        {
            float x = transform.position.x;
            float y = transform.position.y;
            return new Vector2(x, y);
        }
    }

    public Vector2 DeltaPosition
    {
        get
        {
            return CurrentPosition - PreviousPosition;
        }
    }

    private Vector2 _previousPosition;
    private Vector2 _currentPosition;

    // Use this for initialization
    void Start()
    {
        _currentPosition = CurrentPosition;
        _previousPosition = _currentPosition;
    }

    // Update is called once per frame
    void Update()
    {
        _previousPosition = _currentPosition;
        _currentPosition = CurrentPosition;
    }
}