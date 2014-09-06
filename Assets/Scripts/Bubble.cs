using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bubble : MonoBehaviour
{
    public float verticalSpeed = 2.0f;
    public float radiusStart = 0.0f;
    public float radiusMax = 5.0f;
    public float radiusMin = 0.1f;
    public float radiusChangeStep = 1.0f;

    public float rate
    {
        get
        {
            if (null == _rateValue)
            {
                _rateValue = new SmoothlyVaryingValue(1.0f);
            }
            return _rateValue.CurrentValue;
        }

        set
        {
            if (null == _rateValue)
            {
                _rateValue = new SmoothlyVaryingValue(1.0f);
            }
            _rateValue.SetValue(value);
        }
    }

    public Vector2 CurrentDirection
    {
        get
        {
            return _currentDirection;
        }
    }


    private SmoothlyVaryingValue _rateValue;
    private Vector2 _currentDirection = Vector2.zero;
    private List<Vector2> directions = new List<Vector2>();


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
        if (null == _rateValue)
        {
            _rateValue = new SmoothlyVaryingValue(1.0f);
        }

        float value = Mathf.Max(radiusMin, radiusStart);
        _radiusCurrent = value;
        if (_radiusTarget <= 0.0f)
        {
            _radiusTarget = value;
        }

        FixedUpdate();
    }


    private void FixedUpdate()
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

        {
            Vector2 v = Vector2.zero;

            for (int i = 0, count = directions.Count; i < count; ++i)
            {
                v += directions[i];
            }
            v.Normalize();

            rigidbody2D.AddForce(v * verticalSpeed * Size);
            _currentDirection = v;

            directions.Clear();
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (9 == other.gameObject.layer)
        {
            Vector2 v = other.gameObject.transform.rotation * Vector2.up;
            directions.Add(v * Size);
        }
    }

    public float[] getSizes()
    {
        BubbleController controller = GetComponent<BubbleController>();
        if (controller != null)
        {
            int count = controller.suffixes.Count;
            float[] sizes = new float[count];
            float sizeAll = _radiusTarget;
            for (int i = 0; i < count; ++i)
            {
                sizes[i] = controller.suffixes[i].Item2 * sizeAll;
            }
            return sizes;
        }
        else
        {
            return null;
        }
    }

}
