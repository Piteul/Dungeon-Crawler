using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeTester : MonoBehaviour {

    public Shaker shaker;
    public float duration = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)){
            shaker.Shake(duration);
        }
		
	}
}
