using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bubble : MonoBehaviour
{
    public float radiusStart = 0.0f;
    public float radiusMax = 5.0f;
    public float radiusMin = 0.1f;
    public float radiusChangeStep = 1.0f;

    public float rate
    {
        get
        {
            return _rateValue.CurrentValue;
        }

        set
        {
            _rateValue.SetValue(value);
        }
    }

    SmoothlyVaryingValue _rateValue;

    public void SetRadius(float value)
    {
        if (value < radiusMin)
        {
            _radiusTarget = radiusMin;
        }
        else if (radiusMax < value)
        {
            _radiusTarget = radiusMax;
        }
        else
        {
            _radiusTarget = value;
        }
    }


    public void ChangeRadius(float delta)
    {
        float value = _radiusTarget + delta;
        SetRadius(value);
    }


    public float GetRadius(bool withRate)
    {
        if (withRate)
        {
            return _radiusCurrent * rate;
        }
        else
        {
            return _radiusCurrent;
        }
    }


    public float GetRadiusTarget()
    {
        return _radiusTarget;
    }


    private float _radiusCurrent;
    private float _radiusTarget = 0.0f;
    

    public float Size
    {
        get
        {
            float r = GetRadius(true);
            return Mathf.PI * Mathf.Pow(r, 2.0f);
        }
    }


    private void Start()
    {
        _rateValue = new SmoothlyVaryingValue(1.0f);

        float value = Mathf.Max(radiusMin, radiusStart);
        _radiusCurrent = value;
        if (_radiusTarget <= 0.0f)
        {
            _radiusTarget = value;
        }

        Update();
    }


    private void Update()
    {
        if (_radiusCurrent < _radiusTarget)
        {
            _radiusCurrent += radiusChangeStep * Time.deltaTime;
            if (_radiusTarget < _radiusCurrent)
            {
                _radiusCurrent = _radiusTarget;
            }
        }
        else if (_radiusTarget < _radiusCurrent)
        {
            _radiusCurrent -= radiusChangeStep * Time.deltaTime;
            if (_radiusCurrent < _radiusTarget)
            {
                _radiusCurrent = _radiusTarget;
            }
        }

        float radius = GetRadius(true);
        float diameter = 2.0f * radius;
        transform.localScale = new Vector3(diameter, diameter, diameter);

        Camera camera = GetComponentInChildren<Camera>();
        if (camera != null)
        {
            float sizeMin = 39.0f * radiusMin;
            float sizeMax = 21.0f * radiusMax;
            /*
            float center = (sizeMax + sizeMin) / 2.0f;

            float value = center + 2.0f * (radius * 5.0f - center);
            */

            float value = radius * 30.0f;
            
            if (value < sizeMin)
            {
                camera.orthographicSize = sizeMin;
            }
            else if (sizeMax < value)
            {
                camera.orthographicSize = sizeMax;
            }
            else
            {
                camera.orthographicSize = value;
            }
        }

        _rateValue.Step(Time.deltaTime);
    }


    public void IgnoreCollision(Bubble otherBubble)
    {
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), otherBubble.GetComponent<Collider2D>());
    }

}
