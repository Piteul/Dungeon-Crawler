using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour {

    [Range(0f,2f)]
    public float intensity;
    public Transform target;
    Vector3 initialPos;
    float pendingShakeDuration = 0f;
    bool isShacking = false;

    // Use this for initialization
    void Start() {
        target = GetComponent<Transform>();
        initialPos = target.localPosition;
    }

    // Update is called once per frame
    void Update() {
        if (pendingShakeDuration > 0 && !isShacking) {
            StartCoroutine(DoShake());
        }
    }

    public void Shake(float duration) {
        if (duration > 0) {
            pendingShakeDuration += duration;
        }
    }

    IEnumerator DoShake() {
        isShacking = true;
        var startTime = Time.realtimeSinceStartup;
        while(Time.realtimeSinceStartup < startTime + pendingShakeDuration) {
            //Do shack stuff
            var randomPoint = new Vector3(initialPos.x, initialPos.y, Random.Range(-0.2f, 0.2f)*intensity);
            target.localPosition += randomPoint;
            yield return null;
        }

        pendingShakeDuration = 0f;
        target.localPosition = initialPos;
        isShacking = false;



    }
}
