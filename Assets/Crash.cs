using UnityEngine;
using System.Collections;

public class Crash : MonoBehaviour
{
    public float additionalStrength = 1.0f;
    public float cooldown = 1.0f;

    private CarScript car;
    private float cooldownCurrent = 0.0f;


    // Use this for initialization
    void Start()
    {
        car = GetComponent<CarScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (0.0f < cooldownCurrent)
        {
            cooldownCurrent -= Time.deltaTime;
        }
    }

    void generateBubbles(float value)
    {
        Debug.Log("BOOM " + value);
        int count = 1 + (int)(Random.value * value * 2.0f);

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (cooldownCurrent <= 0.0f && additionalStrength * car.Strength < coll.relativeVelocity.magnitude)
        {
            float value = coll.relativeVelocity.magnitude - additionalStrength * car.Strength;
            generateBubbles(value);
            cooldownCurrent = cooldown;
        }
        else
        {
            Debug.Log("no");
        }
    }
}
