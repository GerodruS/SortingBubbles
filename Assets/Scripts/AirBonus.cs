using UnityEngine;
using System.Collections;

public class AirBonus : MonoBehaviour
{
    public float startCooldown = 1.0f;

    private float cooldownCurrent = 0.0f;

    // Use this for initialization
    void Start()
    {
        cooldownCurrent = startCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (0.0f < cooldownCurrent)
        {
            cooldownCurrent -= Time.deltaTime;
        }
    }

    public bool isReady()
    {
        return cooldownCurrent <= 0.0f;
    }

}
