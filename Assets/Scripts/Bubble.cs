using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour
{
    public float radiusStart = 1.0f;
    public float radiusMax = 5.0f;
    public float radiusMin = 0.5f;
    public float radiusChangeStep = 1.0f;
    public float rate = 1.0f;


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


    private float _radiusCurrent;
    private float _radiusTarget;
    

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
        _radiusCurrent = radiusStart;
        _radiusTarget = radiusStart;
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

        float diameter = GetRadius(true) * 2.0f;
        transform.localScale = new Vector3(diameter, diameter, diameter);
    }

}
