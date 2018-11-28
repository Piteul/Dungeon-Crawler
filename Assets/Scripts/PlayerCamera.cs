
using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {
    [Tooltip("The 'animation' loop for movement to reach your desired movement units. The defaults move 1 Unity unit. Adjust this to suit your level design.")]
    public float fMovementIncrement = 0.05f;
    [Tooltip("The 'animation' loop for movement to reach your desired movement units. The defaults move 1 Unity unit. Adjust this to suit your level design.")]
    public float iMovementInterval = 20;
    [Tooltip("Locked Y position of the camera, adjust to suit your level design.")]
    public float fYLockPosition = 0f;
    [Tooltip("Public only for debugging, these values are overridden at runtime.")]
    public bool bMoving = false;
    [Tooltip("Public only for debugging, these values are overridden at runtime.")]
    public bool bRotating = false;
    [Tooltip("fRotateIncrement * iRotateInterval must equal 90 for full grid movement. For faster rotating, lower the interval and raise the increment.")]
    public float fRotateIncrement = 4.5f;
    [Tooltip("fRotateIncrement * iRotateInterval must equal 90 for full grid movement. For faster rotating, lower the interval and raise the increment.")]
    public int iRotateInterval = 20;
    [Tooltip("The WaitForSeconds value returned for each return yeild of the Coroutines.")]
    public float fWaitForSecondsInterval = 0.01f;

    public enum Compass { N, S, E, W }
    Compass currentCompass = Compass.W;

    void Start() {
        bMoving = false;
        bRotating = false;
    }

    void Update() {

        if (!bRotating && !bMoving) {
            transform.position = new Vector3(Mathf.Round(transform.position.x), fYLockPosition, Mathf.Round(transform.position.z));

        }
        if (Input.GetKey(KeyCode.Z)) {
            if (!bRotating && !bMoving) {
                StartCoroutine(StepForward());
            }
        }
        if (Input.GetKey(KeyCode.S)) {
            if (!bRotating && !bMoving) {
                StartCoroutine(StepBackward());
            }
        }
        if (Input.GetKey(KeyCode.Q)) {
            if (!bRotating && !bMoving) {
                StartCoroutine(StepLeft());
            }
        }
        if (Input.GetKey(KeyCode.D)) {
            if (!bRotating && !bMoving) {
                StartCoroutine(StepRight());
            }
        }
        if (Input.GetKey(KeyCode.E)) {
            if (!bRotating && !bMoving) {
                StartCoroutine(RotateRight());
            }
        }

        if (Input.GetKey(KeyCode.A)) {
            if (!bRotating && !bMoving) {
                StartCoroutine(RotateLeft());
            }
        }

    }

    IEnumerator StepForward() {
        Vector3 newpos;
        if (currentCompass == Compass.N) {
            bMoving = true;
            for (int g = 0; g < iMovementInterval; g++) {
                newpos = new Vector3(0, 0, fMovementIncrement);
                transform.position += newpos;
                yield return new WaitForSeconds(fWaitForSecondsInterval);
            }
            yield return new WaitForSeconds(fWaitForSecondsInterval);
            bMoving = false;
        }
        if (currentCompass == Compass.S) {
            bMoving = true;
            for (int g = 0; g < iMovementInterval; g++) {
                newpos = new Vector3(0, 0, -fMovementIncrement);
                transform.position += newpos;
                yield return new WaitForSeconds(fWaitForSecondsInterval);
            }
            yield return new WaitForSeconds(fWaitForSecondsInterval);
            bMoving = false;
        }
        if (currentCompass == Compass.E) {
            bMoving = true;
            for (int g = 0; g < iMovementInterval; g++) {
                newpos = new Vector3(fMovementIncrement, 0, 0);
                transform.position += newpos;
                yield return new WaitForSeconds(fWaitForSecondsInterval);
            }
            yield return new WaitForSeconds(fWaitForSecondsInterval);
            bMoving = false;
        }
        if (currentCompass == Compass.W) {
            bMoving = true;
            for (int g = 0; g < iMovementInterval; g++) {
                newpos = new Vector3(-fMovementIncrement, 0, 0);
                transform.position += newpos;
                yield return new WaitForSeconds(fWaitForSecondsInterval);
            }
            yield return new WaitForSeconds(fWaitForSecondsInterval);
            bMoving = false;
        }


    }

    IEnumerator StepBackward() {
        Vector3 newpos;
        if (currentCompass == Compass.N) {
            bMoving = true;
            for (int g = 0; g < iMovementInterval; g++) {
                newpos = new Vector3(0, 0, -.05f);
                transform.position += newpos;
                yield return new WaitForSeconds(fWaitForSecondsInterval);
            }
            yield return new WaitForSeconds(fWaitForSecondsInterval);
            bMoving = false;
        }
        if (currentCompass == Compass.S) {
            bMoving = true;
            for (int g = 0; g < iMovementInterval; g++) {
                newpos = new Vector3(0, 0, fMovementIncrement);
                transform.position += newpos;
                yield return new WaitForSeconds(fWaitForSecondsInterval);
            }
            yield return new WaitForSeconds(fWaitForSecondsInterval);
            bMoving = false;
        }
        if (currentCompass == Compass.E) {
            bMoving = true;
            for (int g = 0; g < iMovementInterval; g++) {
                newpos = new Vector3(-fMovementIncrement, 0, 0);
                transform.position += newpos;
                yield return new WaitForSeconds(fWaitForSecondsInterval);
            }
            yield return new WaitForSeconds(fWaitForSecondsInterval);
            bMoving = false;
        }
        if (currentCompass == Compass.W) {
            bMoving = true;
            for (int g = 0; g < iMovementInterval; g++) {
                newpos = new Vector3(fMovementIncrement, 0, 0);
                transform.position += newpos;
                yield return new WaitForSeconds(fWaitForSecondsInterval);
            }
            yield return new WaitForSeconds(fWaitForSecondsInterval);
            bMoving = false;
        }

    }

    IEnumerator StepLeft() {
        Vector3 newpos;
        if (currentCompass == Compass.N) {
            bMoving = true;
            for (int g = 0; g < iMovementInterval; g++) {
                newpos = new Vector3(-fMovementIncrement, 0, 0);
                transform.position += newpos;
                yield return new WaitForSeconds(fWaitForSecondsInterval);
            }
            yield return new WaitForSeconds(fWaitForSecondsInterval);
            bMoving = false;
        }
        if (currentCompass == Compass.S) {
            bMoving = true;
            for (int g = 0; g < iMovementInterval; g++) {
                newpos = new Vector3(fMovementIncrement, 0, 0);
                transform.position += newpos;
                yield return new WaitForSeconds(fWaitForSecondsInterval);
            }
            yield return new WaitForSeconds(fWaitForSecondsInterval);
            bMoving = false;
        }
        if (currentCompass == Compass.E) {
            bMoving = true;
            for (int g = 0; g < iMovementInterval; g++) {
                newpos = new Vector3(0, 0, fMovementIncrement);
                transform.position += newpos;
                yield return new WaitForSeconds(fWaitForSecondsInterval);
            }
            yield return new WaitForSeconds(fWaitForSecondsInterval);
            bMoving = false;
        }
        if (currentCompass == Compass.W) {
            bMoving = true;
            for (int g = 0; g < iMovementInterval; g++) {
                newpos = new Vector3(0, 0, -fMovementIncrement);
                transform.position += newpos;
                yield return new WaitForSeconds(fWaitForSecondsInterval);
            }
            yield return new WaitForSeconds(fWaitForSecondsInterval);
            bMoving = false;
        }

        var wait = new WaitForSeconds(0.1f);


    }

    IEnumerator StepRight() {
        Vector3 newpos;
        if (currentCompass == Compass.N) {
            bMoving = true;
            for (int g = 0; g < iMovementInterval; g++) {
                newpos = new Vector3(fMovementIncrement, 0, 0);
                transform.position += newpos;
                yield return new WaitForSeconds(fWaitForSecondsInterval);
            }
            yield return new WaitForSeconds(fWaitForSecondsInterval);
            bMoving = false;
        }
        if (currentCompass == Compass.S) {
            bMoving = true;
            for (int g = 0; g < iMovementInterval; g++) {
                newpos = new Vector3(-fMovementIncrement, 0, 0);
                transform.position += newpos;
                yield return new WaitForSeconds(fWaitForSecondsInterval);
            }
            yield return new WaitForSeconds(fWaitForSecondsInterval);
            bMoving = false;
        }
        if (currentCompass == Compass.E) {
            bMoving = true;
            for (int g = 0; g < iMovementInterval; g++) {
                newpos = new Vector3(0, 0, -fMovementIncrement);
                transform.position += newpos;
                yield return new WaitForSeconds(fWaitForSecondsInterval);
            }
            yield return new WaitForSeconds(fWaitForSecondsInterval);
            bMoving = false;
        }
        if (currentCompass == Compass.W) {
            bMoving = true;
            for (int g = 0; g < iMovementInterval; g++) {
                newpos = new Vector3(0, 0, fMovementIncrement);
                transform.position += newpos;
                yield return new WaitForSeconds(fWaitForSecondsInterval);
            }
            yield return new WaitForSeconds(fWaitForSecondsInterval);
            bMoving = false;
        }

        var wait = new WaitForSeconds(0.1f);

    }

    IEnumerator RotateRight() {
        bRotating = true;

        for (int g = 0; g < iRotateInterval; g++) {
            transform.position = new Vector3(Mathf.Round(transform.position.x), fYLockPosition, Mathf.Round(transform.position.z));
            transform.eulerAngles += new Vector3(0, fRotateIncrement, 0);
            yield return new WaitForSeconds(fWaitForSecondsInterval);
        }

        switch (currentCompass) {
            case (Compass.W):
                currentCompass = Compass.N;
                Debug.Log("Compass is now N");
                //text.text ="N";
                break;
            case (Compass.E):
                currentCompass = Compass.S;
                Debug.Log("Compass is now S");
                //text.text ="S";
                break;
            case (Compass.S):
                currentCompass = Compass.W;
                Debug.Log("Compass is now W");
                //text.text ="W";
                break;
            case (Compass.N):
                currentCompass = Compass.E;
                Debug.Log("Compass is now E");
                //text.text ="E";
                break;
        }
        yield return new WaitForSeconds(fWaitForSecondsInterval);
        bRotating = false;

        var wait = new WaitForSeconds(fWaitForSecondsInterval);


    }

    IEnumerator RotateLeft() {
        bRotating = true;
        for (int g = 0; g < iRotateInterval; g++) {
            transform.position = new Vector3(Mathf.Round(transform.position.x), fYLockPosition, Mathf.Round(transform.position.z));
            transform.eulerAngles += new Vector3(0, -fRotateIncrement, 0);
            yield return new WaitForSeconds(fWaitForSecondsInterval);
        }

        switch (currentCompass) {
            case (Compass.W):
                currentCompass = Compass.S;
                break;
            case (Compass.E):
                currentCompass = Compass.N;
                break;
            case (Compass.S):
                currentCompass = Compass.E;
                break;
            case (Compass.N):
                currentCompass = Compass.W;
                break;
        }
        yield return new WaitForSeconds(fWaitForSecondsInterval);
        bRotating = false;

        var wait = new WaitForSeconds(fWaitForSecondsInterval);

    }
}
