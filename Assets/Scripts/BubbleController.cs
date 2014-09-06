using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Eppy;
using System.Linq;


public class BubbleController : MonoBehaviour
{
    public float horizontalSpeed = 10.0f;
    public List<Tuple<string, float>> suffixes = new List<Tuple<string, float>>();


    private Bubble _bubble;
    private Car _car;


    private void Start()
    {
        _bubble = GetComponent<Bubble>();
        _car = GetComponent<Car>();
    }


    void FixedUpdate()
    {
        float horizontalValue = 0;
        float verticalValue = 0;
        for (int i = 0, count = suffixes.Count; i < count; ++i)
        {
            horizontalValue += Input.GetAxis(suffixes[i].Item1 + "Horizontal");
            verticalValue += Input.GetAxis(suffixes[i].Item1 + "Vertical");
        }
        Control(horizontalValue, verticalValue);
    }


    private void Control(float horizontalValue, float verticalValue)
    {
        if (0.0f < Mathf.Abs(horizontalValue))
        {
            horizontalValue *= horizontalSpeed;
            horizontalValue *= _bubble.Size;
            Quaternion q = Quaternion.EulerAngles(0.0f, 0.0f, -90.0f);
            rigidbody2D.AddForce(q * _bubble.CurrentDirection * horizontalValue);
        }
        
        _car.ChangeRadiusTo(verticalValue);
    }


    public void Reset()
    {
        Control(0.0f, 0.0f);
    }

    public void Absorbtion(Bubble bubbleOther)
    {
        Bubble bubbleThis = _bubble;

        // controll
        BubbleController controllerThis = this;
        BubbleController controllerOther = bubbleOther.GetComponent<BubbleController>();

        // suffixes & sizes
        float[] sizesThis = bubbleThis.getSizes();
        float[] sizesOther = bubbleOther.getSizes();
        float sizeAll = bubbleThis.GetRadiusTarget() + bubbleOther.GetRadiusTarget();
        for (int i = 0, count = sizesThis.Length; i < count; ++i)
        {
            controllerThis.suffixes[i] = new Tuple<string, float>(controllerThis.suffixes[i].Item1, sizesThis[i] / sizeAll);
        }
        for (int i = 0, count = sizesOther.Length; i < count; ++i)
        {
            controllerOther.suffixes[i] = new Tuple<string, float>(controllerOther.suffixes[i].Item1, sizesOther[i] / sizeAll);
        }
        controllerThis.suffixes.AddRange(controllerOther.suffixes);
        //controllerThis.suffixes = controllerThis.suffixes.Distinct().ToList();

        // size
        _bubble.ChangeRadius(bubbleOther.GetRadiusTarget());
        //

        // change color
        //GameObject managers = GameObject.Find("Managers");
        //GameManager gameManager = managers.GetComponent<GameManager>();
        var meshRendererThis = GetComponentInChildren<MeshRenderer>().material;
        var meshRendererOther = bubbleOther.GetComponentInChildren<MeshRenderer>().material;
        float r = (meshRendererThis.color.r + meshRendererOther.color.r) / 2.0f;
        float g = (meshRendererThis.color.g + meshRendererOther.color.g) / 2.0f;
        float b = (meshRendererThis.color.b + meshRendererOther.color.b) / 2.0f;
        meshRendererThis.color = new Color(r, g, b);
        // 

        // camera
        var camerasOther = bubbleOther.GetComponentsInChildren<Camera>();
        for (int i = 0, count = camerasOther.Length; i < count; ++i)
        {
            camerasOther[i].transform.parent = gameObject.transform;
        }
        //

        Destroy(bubbleOther.gameObject);
    }

}