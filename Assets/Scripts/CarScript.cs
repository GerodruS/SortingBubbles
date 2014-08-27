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

    public float MovementRate
    {
        get
        {
            return Size / SizeOrigin;
        }
    }

    public float Strength
    {
        get
        {
            return SizeOrigin / Size;
        }
    }

    public float SizeOrigin = 2.0f;
    public float Size = 2.0f;
    public float SizeChangingSpeed = 0.1f;
    public float SizeMaxDelta = 1.0f;

    private Vector2 _previousPosition;
    private Vector2 _currentPosition;

    // Use this for initialization
    private void Start()
    {
        Size = SizeOrigin;
        _currentPosition = CurrentPosition;
        _previousPosition = _currentPosition;
    }

    // Update is called once per frame
    private void Update()
    {
        _previousPosition = _currentPosition;
        _currentPosition = CurrentPosition;

        transform.localScale = new Vector3(Size, Size, Size);
    }

    public void ChangeSizeTo(float rate)
    {
        float sizeOld = Size;
        float sizeTarget = SizeOrigin + SizeMaxDelta * rate;
        if (sizeTarget < SizeOrigin - SizeMaxDelta)
        {
            sizeTarget = SizeOrigin - SizeMaxDelta;
        }
        else if (SizeOrigin + SizeMaxDelta < sizeTarget)
        {
            sizeTarget = SizeOrigin + SizeMaxDelta;
        }
        float sizeNew = sizeOld + (sizeTarget - sizeOld) * SizeChangingSpeed * Time.deltaTime;
        //Debug.Log(string.Format("sizeOld={0} sizeTarget={1} sizeNew={2}", sizeOld, sizeTarget, sizeNew));

        Size = sizeNew;
    }

    public void toChangeAirValue(float delta)
    {
        Size += delta;
        SizeOrigin += delta;
    }

}