using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoppingText : MonoBehaviour {

    float DestroyTime = 5f;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, DestroyTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
