using System.Collections;

public class SmoothlyVaryingValue
{
    public float valueStep = 1.0f;

    public float CurrentValue
    {
        get
        {
            return _valueCurrent;
        }
    }


    private float _valueTarget;
    private float _valueCurrent;


    public SmoothlyVaryingValue()
    {
        _valueTarget = 0.0f;
        _valueCurrent = 0.0f;
    }


    public SmoothlyVaryingValue(float valueStart)
    {
        _valueTarget = valueStart;
        _valueCurrent = valueStart;
    }


    public SmoothlyVaryingValue(float valueStart, float valueTarget)
    {
        _valueTarget = valueTarget;
        _valueCurrent = valueStart;
    }


    public void Step(float deltaTime)
    {
        if (_valueCurrent < _valueTarget)
        {
            _valueCurrent += valueStep * deltaTime;
            if (_valueTarget < _valueCurrent)
            {
                _valueCurrent = _valueTarget;
            }
        }
        else if (_valueTarget < _valueCurrent)
        {
            _valueCurrent -= valueStep * deltaTime;
            if (_valueCurrent < _valueTarget)
            {
                _valueCurrent = _valueTarget;
            }
        }
    }
    
    public void SetValue(float value)
    {
        _valueTarget = value;
    }

}
