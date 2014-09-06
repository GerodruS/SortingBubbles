using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Crash : MonoBehaviour
{
    public float strength = 1.0f;
    public float cooldownTimeCollision = 0.1f;
    public float cooldownTimeIgnore = 0.1f;
    public float penalty = 0.3f;
    public Bubble generatedBubble;
    public float startForce = 10.0f;
    public int maxCount = 5;
    public float absorbationStep = 0.1f;

    private Bubble _bubble;
    private Cooldown _cooldownCollision;
    private bool init = false;

    private void Start()
    {
        _bubble = GetComponent<Bubble>();
        _objectsToIgnoreTemp = new List<Crash>();
        _cooldownCollision = new Cooldown();

        init = true;
    }


    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (!init)
        {
            return;
        }

        if (_cooldownCollision.isReady())
        {
            float r = _bubble.GetRadius(false);
            if (strength / (r * (1.0f + _bubble.rate)) < coll.relativeVelocity.magnitude)
            {
                Bubble bubbleOther = coll.gameObject.GetComponent<Bubble>();
                if (null == bubbleOther || Mathf.Abs(_bubble.GetRadius(true) - bubbleOther.GetRadius(true)) < 0.1f)
                {
                    // simple crash
                    _cooldownCollision.StartTimer(cooldownTimeCollision);

                    r = _bubble.GetRadiusTarget() * (1.0f - penalty);
                    float oldSize = _bubble.GetRadiusTarget();
                    _bubble.SetRadius(r);

                    r = oldSize - _bubble.GetRadiusTarget();
                    float summ = generateBubbles(r, coll.relativeVelocity.magnitude);
                }
                else if (bubbleOther != null)
                {
                    Car carOther = bubbleOther.GetComponent<Car>();
                    Car carThis = GetComponent<Car>();
                    if (null == carOther)
                    {
                        if (null == carThis)
                        {
                            // other is simple bubble
                            //  this is simple bubble
                            omnomnom(bubbleOther, false);
                        }
                        else
                        {
                            // other is simple bubble
                            //  this is car
                            omnomnom(bubbleOther, true);
                        }
                    }
                    else
                    {
                        if (null == carThis)
                        {
                            // other is car
                            //  this is simple bubble
                            //  do nothing
                        }
                        else
                        {
                            // other is car
                            //  this is car
                            // union
                            BubbleController controllerThis = carThis.GetComponent<BubbleController>();
                            controllerThis.Absorbtion(bubbleOther);
                        }
                    }
                }
            }
        }
    }


    private void omnomnom(Bubble bubbleOther, bool forceAbsorbation)
    {
        if (forceAbsorbation || bubbleOther.GetRadius(true) < _bubble.GetRadius(true))
        {
            float radius = bubbleOther.GetRadiusTarget();
            if (radius <= absorbationStep)
            {
                _bubble.ChangeRadius(radius);
                Destroy(bubbleOther.gameObject);
            }
            else
            {
                bubbleOther.ChangeRadius(-absorbationStep);
                float delta = radius - bubbleOther.GetRadiusTarget();
                if (delta < absorbationStep)
                {
                    _bubble.ChangeRadius(radius);
                    Destroy(bubbleOther.gameObject);
                }
                else
                {
                    _bubble.ChangeRadius(delta);
                }
            }
        }
    }


    private void Update()
    {
        _cooldownCollision.Step(Time.deltaTime);
        for (int i = 0; i < _cooldownIgnore.Count; ++i)
        {
            _cooldownIgnore[i].Item1.Step(Time.deltaTime);
        }
    }


    private void FixedUpdate()
    {
        for (int i = 0; i < _cooldownIgnore.Count; ++i)
        {
            if (_cooldownIgnore[i].Item1.isReady())
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), _cooldownIgnore[i].Item2, false);
                _cooldownIgnore.RemoveAt(i);
                --i;
            }
        }
    }


    private List<Crash> _objectsToIgnoreTemp = new List<Crash>();

    private float generateBubbles(float value, float velocity)
    {
        float result = 0.0f;

        float min = 0.1f;

        _objectsToIgnoreTemp.Add(this);

        for (int i = 0; i < maxCount && min < value; ++i)
        {
            Bubble bubbleNew = (Bubble)Instantiate(generatedBubble, transform.position, Quaternion.identity);
            _objectsToIgnoreTemp.Add(bubbleNew.GetComponent<Crash>());

            bubbleNew.radiusStart = 0.0f;

            float r = (i == maxCount - 1 ?
                       1.0f :
                       0.1f + Random.value * 0.8f) * value;
            if (r < min)
            {
                r = min;
            }

            bubbleNew.SetRadius(r);
            result += bubbleNew.GetRadiusTarget();
            Rigidbody2D body = bubbleNew.GetComponent<Rigidbody2D>();
            body.AddForce(Random.insideUnitCircle * startForce * velocity);

            value -= r;
        }

        int count = _objectsToIgnoreTemp.Count;
        for (int i = 0; i < count; ++i)
        {
            Crash a = _objectsToIgnoreTemp[i];
            for (int j = i + 1; j < count; ++j)
            {
                Crash b = _objectsToIgnoreTemp[j];
                a.IgnoreCollision(b.GetComponent<Collider2D>());
            }
        }

        _objectsToIgnoreTemp.Clear();

        return result;
    }

    List<Eppy.Tuple<Cooldown, Collider2D>> _cooldownIgnore = new List<Eppy.Tuple<Cooldown, Collider2D>>();

    public void IgnoreCollision(Collider2D other)
    {
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), other);

        Cooldown cooldown = new Cooldown();
        cooldown.StartTimer(cooldownTimeIgnore);
        _cooldownIgnore.Add(new Eppy.Tuple<Cooldown, Collider2D>(cooldown, other));
        
    }

}