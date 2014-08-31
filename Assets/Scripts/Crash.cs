using UnityEngine;
using System.Collections;


public class Crash : MonoBehaviour
{
    public float strength = 1.0f;
    public float cooldownTime = 0.0f;
    public float penalty = 0.3f;

    private Bubble _bubble;
    private Cooldown _cooldown;


    private void Start()
    {
        _bubble = GetComponent<Bubble>();
        _cooldown = new Cooldown();
    }


    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (_cooldown.isReady())
        {
            float r = _bubble.GetRadius(false);
            if (strength / (r * (1.0f + _bubble.rate)) < coll.relativeVelocity.magnitude)
            {
                _cooldown.StartTimer(cooldownTime);

                //generateBubbles();

                r *= (1.0f - penalty);
                _bubble.SetRadius(r);
            }
        }
    }

    private void Update()
    {
        _cooldown.Step();
    }

}