using UnityEngine;
using System.Collections;

public class Source : MonoBehaviour
{
    public float CooldownStart = 0.0f;
    public float Cooldown = 0.0f;
    public float Value = 1.0f;

    private float CooldownCurrent = 0.0f;

    // Use this for initialization
    void Start()
    {
        resetCooldown(CooldownStart);
    }

    // Update is called once per frame
    void Update()
    {
        if (isCooldown())
        {
            CooldownCurrent -= Time.deltaTime;
            if (!isCooldown())
            {
                ParticleSystem particles = GetComponent<ParticleSystem>();
                particles.enableEmission = true;
            }
        }
    }

    bool isCooldown()
    {
        return 0.0f < CooldownCurrent;
    }

    void resetCooldown(float time)
    {
        CooldownCurrent = time;
        ParticleSystem particles = GetComponent<ParticleSystem>();
        particles.enableEmission = false;
    }

    public float getAir()
    {
        if (!isCooldown())
        {
            resetCooldown(Cooldown);
            return Value;
        }
        else
        {
            return 0.0f;
        }
    }

}
