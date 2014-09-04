using UnityEngine;
using System.Collections;


public class BubbleSource : MonoBehaviour
{
    public Bubble bubble;
    public float cooldownMin = 0.1f;
    public float cooldownMax = 1.0f;
    public float bubbleRadiusMax = 1.0f;
    public float startForce = 1.0f;


    private Cooldown cooldown;


    private void Start()
    {
        cooldown = new Cooldown();
        resetCooldown();
    }


    private void FixedUpdate()
    {
        if (cooldown.isReady())
        {
            Bubble bubbleNew = (Bubble)Instantiate(bubble, transform.position, Quaternion.identity);
            bubbleNew.radiusStart = 0.0f;
            float r = bubbleRadiusMax * Random.value;
            bubbleNew.SetRadius(r);
            Rigidbody2D body = bubbleNew.GetComponent<Rigidbody2D>();
            body.AddForce(Random.insideUnitCircle * startForce);

            resetCooldown();
        }

        cooldown.Step(Time.deltaTime);
    }


    private void resetCooldown()
    {
        if (0.0f < cooldownMax)
        {
            float time = cooldownMin + (cooldownMax - cooldownMin) * Random.value;
            cooldown.StartTimer(time);
        }
    }

}
