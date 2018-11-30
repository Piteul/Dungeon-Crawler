using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyMovement : MonoBehaviour {

    EnemyVisibility enemVis;
    public bool isMove = false;
    private bool dejaVu = false;
    private bool inRangeforAttack = false;
    private int stepSize = 4;
    public float fRotateIncrement = 4.5f;
    public int iRotateInterval = 20;
    void Start()
    {
        enemVis = GetComponent<EnemyVisibility>();

    }

    void Update()
    {
        RaycastHit hit;
        inRangeforAttack = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, enemVis.attackDistance, 1);
        if (enemVis.CheckVisibilty() && !dejaVu)
        {
            
            dejaVu = true;
            if (!inRangeforAttack)
            {
                
                int boxes = countBoxes() - enemVis.attackDistanceByCase;
                StartCoroutine(move(boxes));
                inRangeforAttack = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, enemVis.attackDistance, 1);
            }
            else
                Attack();
        }
        else if (!enemVis.CheckVisibilty() && dejaVu)
        {
            dejaVu = false;
            int boxes = countBoxes();
            StartCoroutine(move(boxes));
            /*if (enemVis.plOrientation == playerLastOrientation.Left)
            {
                StartCoroutine(RotateLeft());
            }
            else if (enemVis.plOrientation == playerLastOrientation.Right)
            {
                StartCoroutine(RotateRight());
            }*/
        }
    }

    private void Attack()
    {
        Debug.Log("Attacking");
    }

    int countBoxes()
    {
        switch (enemVis.orientation)
        {
            // 4 est le step size
            case (Orientation.Forward):
                Debug.Log("forward");
                return Mathf.Abs((int)(((int)transform.position.x - (int)enemVis.playerLastPosition.x) / stepSize));
            case (Orientation.Backward):
                Debug.Log("backward");
                return Mathf.Abs((int)(((int)transform.position.x - (int)enemVis.playerLastPosition.x) / stepSize));
            case (Orientation.Right):
                Debug.Log("right");
                return Mathf.Abs((int)(((int)transform.position.z - (int)enemVis.playerLastPosition.z) / stepSize));
            case (Orientation.Left):
                Debug.Log("left");
                return Mathf.Abs((int)(((int)transform.position.z - (int)enemVis.playerLastPosition.z) / stepSize));

        }
        return -1;
    }

    IEnumerator move(int boxes)
    {
        Debug.Log(boxes);
        for(int i =0; i < boxes; i++) { 
            switch (enemVis.orientation)
            {
                case (Orientation.Forward):
                    for (int g = 0; g < 20; g++)
                    {
                        transform.position += Vector3.right * 0.2f;
                        yield return new WaitForSeconds(0.01f);
                    }
                    Debug.Log("moving");
                    break;
                case (Orientation.Backward):
                    for (int g = 0; g < 20; g++)
                    {
                        transform.position -= Vector3.right * 0.2f;
                        yield return new WaitForSeconds(0.01f);
                    }
                    break;
                case (Orientation.Right):
                    for (int g = 0; g < 20; g++)
                    {
                        transform.position += Vector3.forward * 0.2f;
                        yield return new WaitForSeconds(0.01f);
                    }
                    break;
                case (Orientation.Left):
                    for (int g = 0; g < 20; g++)
                    {
                        transform.position -= Vector3.forward * 0.2f;
                        yield return new WaitForSeconds(0.01f);
                    }
                    break;
            }
            yield return new WaitForSeconds(0.5f);
        }

        isMove = false;
    }

    IEnumerator RotateRight()
    {
        

        for (int g = 0; g < iRotateInterval; g++)
        {
            //transform.position = new Vector3(Mathf.Round(transform.position.x), fYLockPosition, Mathf.Round(transform.position.z));
            transform.localEulerAngles += new Vector3(0, fRotateIncrement, 0);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        
        var wait = new WaitForSeconds(0.5f);

    }

    IEnumerator RotateLeft()
    {
        

        for (int g = 0; g < iRotateInterval; g++)
        {
            //transform.position = new Vector3(Mathf.Round(transform.position.x), fYLockPosition, Mathf.Round(transform.position.z));
            transform.localEulerAngles += new Vector3(0, -fRotateIncrement, 0);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);

        var wait = new WaitForSeconds(0.5f);

    }


}

















/*
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
                Debug.Log("got here");

                // calcul des cases à marcher sur
                int cases = Mathf.Abs((int)(((int)transform.position.x - (int)enemVis.playerLastPosition.x) / (int)enemVis.viewDistance));

                for(int i = 0; i < cases; i++) {
                    Debug.Log(cases);
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
        Debug.Log("attack");
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

    }*/
