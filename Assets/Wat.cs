using UnityEngine;
using System.Collections;

public class Wat : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        rigidbody.AddForce(1, 10, 1);
    }
}
