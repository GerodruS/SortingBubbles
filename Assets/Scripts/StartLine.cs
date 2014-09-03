using UnityEngine;
using System.Collections;

public class StartLine : MonoBehaviour
{
    public float startTime = 5.0f;


    private Cooldown _cooldown;
    private int _secondsLeft;
    private bool _enable;


    void Start()
    {
        _cooldown = new Cooldown();
        _cooldown.StartTimer(startTime);
        _secondsLeft = (int)startTime;
        _enable = true;
    }


    void Update()
    {
        if (_enable)
        {
            if (_cooldown.isReady())
            {
                GameObject[] cars = GameObject.FindGameObjectsWithTag("Cars");
                foreach (GameObject car in cars)
                {
                    car.rigidbody2D.isKinematic = false;
                }
                _enable = false;
            }
            else
            {
                _cooldown.Step(Time.deltaTime);
                if (_cooldown.Value < _secondsLeft)
                {
                    _secondsLeft = (int)_cooldown.Value;
                    if (0.0f <= _cooldown.Value)
                    {
                        Debug.Log(_secondsLeft + 1);
                    }
                    else
                    {
                        Debug.Log("Start!");
                    }
                }
            }
        }
    }

}
