using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoppingText : MonoBehaviour {

    float destroyTime = 0.5f;
    Vector3 offset = new Vector3(0, 1f, 0);
    Vector3 randomPosition = new Vector3(0.5f, 0.2f, 0);

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, destroyTime);
        transform.localPosition += offset;
        transform.localPosition += new Vector3(Random.Range(-randomPosition.x, randomPosition.x), Random.Range(-randomPosition.y, randomPosition.y), Random.Range(-randomPosition.z, randomPosition.z));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
