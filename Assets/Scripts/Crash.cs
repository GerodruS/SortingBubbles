using UnityEngine;
using System.Collections;


public class Crash : MonoBehaviour
{
    public float strength = 1.0f;
    public float cooldownTime = 0.1f;
    public float penalty = 0.3f;
    public Bubble generatedBubble;
    public float startForce = 10.0f;

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
                Bubble bubbleOther = coll.gameObject.GetComponent<Bubble>();
                if (null == bubbleOther || Mathf.Abs(_bubble.GetRadius(true) - bubbleOther.GetRadius(true)) < 0.1f)
                {
                    // crash
                    _cooldown.StartTimer(cooldownTime);

                    generateBubbles(penalty);

                    r *= (1.0f - penalty);
                    _bubble.SetRadius(r);
                }
                else if (bubbleOther != null && bubbleOther.GetRadius(true) < _bubble.GetRadius(true))
                {
                    // absorption
                    _bubble.ChangeRadius(bubbleOther.GetRadius(false));
                    Destroy(bubbleOther.gameObject);
                }
            }
        }
    }

    private void Update()
    {
        _cooldown.Step();
    }

    private void generateBubbles(float value)
    {
        while (0.1f < value)
        {
            Bubble bubbleNew = (Bubble)Instantiate(generatedBubble, transform.position, Quaternion.identity);
            bubbleNew.radiusStart = 0.0f;
            float r = (0.1f + Random.value * 0.8f) * value;
            bubbleNew.SetRadius(r);
            Rigidbody2D body = bubbleNew.GetComponent<Rigidbody2D>();
            body.AddForce(Random.insideUnitCircle * startForce);

            value -= r;
        }
    }

}