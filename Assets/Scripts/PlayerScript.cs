using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

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

    int stepSize = 4;
    int rotateSize = 90;
    public LayerMask mur;
    float rotationX = 0;
    float rotationY = -90;
    float currentYRotation = -90;
    public float minAngleX = -90;
    public RayChecker rayCheck;
    public float maxAngleX = 90;
    public float minAngleY = -180;
    public float maxAngleY = 0;

    Animator animator;

    // Use this for initialization
    void Start() {

        bMoving = false;
        bRotating = false;
        rayCheck = GetComponent<RayChecker>();

        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update() {
        playerMovement();
        Attack();
    }


    public void Attack() {
        if (Input.GetMouseButtonDown(0)) {
            animator.SetTrigger("Attack");
        }
    }

    void RotateCamera(bool goBack) {
        // after each rotation applied in coroutines rotateLeft(), right,etc.. we change the value of currentYRotation in order to go back to this exact rotation on mouse release.

        if (goBack) {
            // lorsqu'on lache la souris, on revient à la rotation initiale
            transform.rotation = Quaternion.Euler(0, -90, 0);

            rotationX = 0;
            rotationY = currentYRotation;
            //cam.localRotation = Quaternion.Euler(0, 0, 0);
            return;
        }


        // change the rotation according to mouse axis 
        rotationX -= Input.GetAxis("Mouse X") * 4f;
        rotationY += Input.GetAxis("Mouse Y") * 4f;

        // constraint the rotation on both axis
        rotationX = Mathf.Clamp(rotationX, -90, 90);
        rotationY = Mathf.Clamp(rotationY, -180, 0);

        // apply the new rotation.
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        //cam.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }


    public void playerMovement() {

        Vector3 position = transform.position;
        Vector3 rotation = transform.localEulerAngles;

        if (!bRotating && !bMoving) {
            transform.position = new Vector3(Mathf.Round(transform.position.x), fYLockPosition, Mathf.Round(transform.position.z));

            if (Input.GetMouseButton(1)) {
                // go back is false this means apply rotation change, no going back!!
                RotateCamera(false);
            }
            if (Input.GetMouseButtonUp(1)) {
                // go back is true, go back to initial roation.
                RotateCamera(true);

            }
        }
        //up
        if (Input.GetKey(KeyCode.Z)) {
            if (!bRotating && !bMoving && rayCheck.wForward) {
                StartCoroutine(StepForward());
            }
        }
        //down
        if (Input.GetKey(KeyCode.S)) {
            if (!bRotating && !bMoving && rayCheck.wBackward) {
                StartCoroutine(StepBackward());
            }
        }
        //left
        if (Input.GetKey(KeyCode.Q) && rayCheck.wLeft) {
            if (!bRotating && !bMoving) {
                StartCoroutine(StepLeft());
            }
        }
        //right
        if (Input.GetKey(KeyCode.D) && rayCheck.wRight) {
            if (!bRotating && !bMoving) {
                StartCoroutine(StepRight());
            }
        }
        //rotate right
        if (Input.GetKey(KeyCode.E)) {
            if (!bRotating && !bMoving) {
                StartCoroutine(RotateRight());
            }
        }
        //rotate left
        if (Input.GetKey(KeyCode.A)) {
            if (!bRotating && !bMoving) {
                StartCoroutine(RotateLeft());
            }
        }
    }

    IEnumerator StepForward() {
        bMoving = true;
        for (int g = 0; g < iMovementInterval; g++) {
            transform.position += transform.forward * fMovementIncrement;
            yield return new WaitForSeconds(fWaitForSecondsInterval);
        }
        yield return new WaitForSeconds(fWaitForSecondsInterval);
        bMoving = false;
    }

    IEnumerator StepBackward() {
        bMoving = true;
        for (int g = 0; g < iMovementInterval; g++) {
            transform.position -= transform.forward * fMovementIncrement;
            yield return new WaitForSeconds(fWaitForSecondsInterval);
        }
        yield return new WaitForSeconds(fWaitForSecondsInterval);
        bMoving = false;
    }

    IEnumerator StepLeft() {
        bMoving = true;
        for (int g = 0; g < iMovementInterval; g++) {
            transform.position -= transform.right * fMovementIncrement;
            yield return new WaitForSeconds(fWaitForSecondsInterval);
        }
        yield return new WaitForSeconds(fWaitForSecondsInterval);
        bMoving = false;
    }

    IEnumerator StepRight() {
        bMoving = true;
        for (int g = 0; g < iMovementInterval; g++) {
            transform.position += transform.right * fMovementIncrement;
            yield return new WaitForSeconds(fWaitForSecondsInterval);
        }
        yield return new WaitForSeconds(fWaitForSecondsInterval);
        bMoving = false;
    }

    IEnumerator RotateRight() {
        bRotating = true;

        for (int g = 0; g < iRotateInterval; g++) {
            //transform.position = new Vector3(Mathf.Round(transform.position.x), fYLockPosition, Mathf.Round(transform.position.z));
            transform.localEulerAngles += new Vector3(0, fRotateIncrement, 0);
            currentYRotation += fRotateIncrement;
            yield return new WaitForSeconds(fWaitForSecondsInterval);
        }
        yield return new WaitForSeconds(fWaitForSecondsInterval);
        bRotating = false;
        var wait = new WaitForSeconds(fWaitForSecondsInterval);

    }

    IEnumerator RotateLeft() {
        bRotating = true;

        for (int g = 0; g < iRotateInterval; g++) {
            //transform.position = new Vector3(Mathf.Round(transform.position.x), fYLockPosition, Mathf.Round(transform.position.z));
            transform.localEulerAngles += new Vector3(0, -fRotateIncrement, 0);
            currentYRotation += -fRotateIncrement;
            yield return new WaitForSeconds(fWaitForSecondsInterval);
        }
        yield return new WaitForSeconds(fWaitForSecondsInterval);
        bRotating = false;
        var wait = new WaitForSeconds(fWaitForSecondsInterval);

    }

}
