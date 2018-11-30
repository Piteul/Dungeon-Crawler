using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [Tooltip("The 'animation' loop for movement to reach your desired movement units. The defaults move 1 Unity unit. Adjust this to suit your level design.")]
    public float fMovementIncrement = 0.1f;
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


    private bool visibility = false;
    private EnemyVisibility enemVis;
    private bool inRangeforAttack = false; 
    // Use this for initialization
    void Start () {
        enemVis = GetComponent<EnemyVisibility>();
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        inRangeforAttack = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, enemVis.attackDistance, 1);

        if (enemVis.CheckVisibilty())
        {
            visibility = true;
            if (!inRangeforAttack )
            {
                int cases = (int) (((int)transform.position.x - (int)enemVis.playerLastPosition.x) / (int)enemVis.viewDistance);
                
                //StartCoroutine(StepForward());
            }
                else
                Attack();
        }
        else
        {
            // "random searching behaviour"
            if (visibility)
            {
                int cases = (int)(((int)transform.position.x - (int)enemVis.playerLastPosition.x) / (int)enemVis.viewDistance);
                for(int i = 0; i < cases; i++) { 
                    StartCoroutine(StepForward());
                }
                // Quand il arrive à la dernière case où il a vu le joueur il tourne vers la dernière orientation du joueur
                if (enemVis.distanceToPlayer <= 2)
                {
                    if(!bRotating && !bMoving)
                    {
                        if(enemVis.plOrientation == playerLastOrientation.Left)
                        {
                            StartCoroutine(RotateLeft());
                        }
                        else if (enemVis.plOrientation == playerLastOrientation.Right)
                        {
                            StartCoroutine(RotateRight());
                        }
                    }
                }
            }
        }
    }

    private void Attack()
    {
        Debug.Log("shit");
    }

    IEnumerator StepForward()
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
            
            yield return new WaitForSeconds(fWaitForSecondsInterval);
        }
        yield return new WaitForSeconds(fWaitForSecondsInterval);
        bRotating = false;
        var wait = new WaitForSeconds(fWaitForSecondsInterval);

    }
}
