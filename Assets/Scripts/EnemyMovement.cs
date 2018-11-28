using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    bool bMoving, bRotating;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*IEnumerator StepForward()
    {
        bMoving = true;
        for (int g = 0; g < iMovementInterval; g++)
        {
            transform.position += transform.forward * fMovementIncrement;
            yield return new WaitForSeconds(fWaitForSecondsInterval);
        }
        yield return new WaitForSeconds(fWaitForSecondsInterval);
        bMoving = false;
    }

    IEnumerator RotateRight()
    {
        bRotating = true;

        for (int g = 0; g < iRotateInterval; g++)
        {
            //transform.position = new Vector3(Mathf.Round(transform.position.x), fYLockPosition, Mathf.Round(transform.position.z));
            transform.localEulerAngles += new Vector3(0, fRotateIncrement, 0);
            currentYRotation += fRotateIncrement;
            yield return new WaitForSeconds(fWaitForSecondsInterval);
        }
        yield return new WaitForSeconds(fWaitForSecondsInterval);
        bRotating = false;
        var wait = new WaitForSeconds(fWaitForSecondsInterval);

    }

    IEnumerator RotateLeft()
    {
        bRotating = true;

        for (int g = 0; g < iRotateInterval; g++)
        {
            //transform.position = new Vector3(Mathf.Round(transform.position.x), fYLockPosition, Mathf.Round(transform.position.z));
            transform.localEulerAngles += new Vector3(0, -fRotateIncrement, 0);
            currentYRotation += -fRotateIncrement;
            yield return new WaitForSeconds(fWaitForSecondsInterval);
        }
        yield return new WaitForSeconds(fWaitForSecondsInterval);
        bRotating = false;
        var wait = new WaitForSeconds(fWaitForSecondsInterval);

    }*/
}
