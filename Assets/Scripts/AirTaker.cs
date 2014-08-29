using UnityEngine;
using System.Collections;

public class AirTaker : MonoBehaviour
{
    public float speed = 1.0f;
    public float cooldown = 1.0f;
    public float cooldownSpeed = 0.1f;

    private float target = 0.0f;
    public float cooldownCurrent = 0.0f;

    void Update()
    {
        if (0.0f < target)
        {
            CarScript car = GetComponent<CarScript>();
            float value = speed * Time.deltaTime;
            if (target < value)
            {
                car.toChangeAirValue(target);
                target = 0.0f;
            }
            else
            {
                car.toChangeAirValue(value);
                target -= value;
            }
        }

        if (0.0f < cooldownCurrent)
        {
            cooldownCurrent -= cooldownSpeed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ("Source" == other.tag)
        {
            if (cooldownCurrent <= 0.0f)
            {
                Source source = other.GetComponent<Source>();
                target += source.CollectAir();
                cooldownCurrent = cooldown;
            }
        }
    }

}
