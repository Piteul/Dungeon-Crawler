using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyMovement : MonoBehaviour {

    EnemyVisibility enemVis;
    public bool isMove = false;
    private bool dejaVu = false;
    public bool inRangeforAttack = false;
    private int stepSize = 4;
    private bool boxesCalculated = false;
    public float fRotateIncrement = 4.5f;
    public int iRotateInterval = 20;
    void Start()
    {
        enemVis = GetComponent<EnemyVisibility>();

    }

    void Update()
    {
        
        Debug.Log(enemVis.orientation);
        enemVis.playerOrientation();
        RaycastHit hit;
        
        //Debug.Log("cv"+enemVis.CheckVisibilty());
        inRangeforAttack = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, enemVis.attackDistance, 1);
        if (enemVis.CheckVisibilty() && !dejaVu) //
        {
            
            //Debug.Log("i see you ");
            enemVis.playerOrientation();
            dejaVu = true;
            if (!inRangeforAttack && !boxesCalculated) //
            {
                
                int boxes = countBoxes(); //- enemVis.attackDistanceByCase;
                boxesCalculated = true;
                StartCoroutine(move(boxes));   
            }
            else
                Attack();
        }
       
    }
    private void rotateEnemy()
    {
        

        enemVis.playerOrientation();
        if (enemVis.plOrientation == playerLastOrientation.Left)
        {
            StartCoroutine(RotateLeft());
        }
        else if (enemVis.plOrientation == playerLastOrientation.Right)
        {
            StartCoroutine(RotateRight());
        }
    }
    private void Attack()
    {
        //Debug.Log("Attacking");
    }

    int countBoxes()
    {
        switch (enemVis.orientation)
        {
            // 4 est le step size
            case (Orientation.North):
                //Debug.Log("forward");
                return Mathf.Abs((int)(((int)transform.position.x - (int)enemVis.playerLastPosition.x) / stepSize));
            case (Orientation.South):
               // Debug.Log("backward");
                return Mathf.Abs((int)(((int)transform.position.x - (int)enemVis.playerLastPosition.x) / stepSize));
            case (Orientation.East):
                //Debug.Log("right");
                return Mathf.Abs((int)(((int)transform.position.z - (int)enemVis.playerLastPosition.z) / stepSize));
            case (Orientation.West):
                //Debug.Log("left");
                return Mathf.Abs((int)(((int)transform.position.z - (int)enemVis.playerLastPosition.z) / stepSize));

        }
        return -1;
    }

    IEnumerator move(int boxes)
    {
        //Debug.Log(boxes);
        int i = -1;
        for (i = 0; i < boxes; i++) {
            RaycastHit hit;
            inRangeforAttack = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, enemVis.attackDistance, 1);
            if (inRangeforAttack)
                break;
            switch (enemVis.orientation)
            {
                case (Orientation.North):
                    for (int g = 0; g < 20; g++)
                    {
                        transform.position += Vector3.right * 0.2f;
                        yield return new WaitForSeconds(0.01f);
                    }
                    //Debug.Log("moving");
                    break;
                case (Orientation.South):
                    for (int g = 0; g < 20; g++)
                    {
                        transform.position -= Vector3.right * 0.2f;
                        yield return new WaitForSeconds(0.01f);
                    }
                    break;
                case (Orientation.East):
                    for (int g = 0; g < 20; g++)
                    {
                        transform.position += Vector3.forward * 0.2f;
                        yield return new WaitForSeconds(0.01f);
                    }
                    break;
                case (Orientation.West):
                    for (int g = 0; g < 20; g++)
                    {
                        transform.position -= Vector3.forward * 0.2f;
                        yield return new WaitForSeconds(0.01f);
                    }
                    break;
            }
            
            yield return new WaitForSeconds(0.5f);
        }
        //if (i == 0) i = -1;
        if(i == boxes) { 
            rotateEnemy();
            
        }
        isMove = false;
    }

    IEnumerator RotateRight()
    {
        Debug.Log("Rotating Right");
        for (int g = 0; g < iRotateInterval; g++)
        {
            //transform.position = new Vector3(Mathf.Round(transform.position.x), fYLockPosition, Mathf.Round(transform.position.z));
            transform.localEulerAngles += new Vector3(0, fRotateIncrement, 0);
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(0.5f);
        // reset key boolean for recalculation, c'est ça le problème avec les coroutines en fait si tu les reset pas ici
        // ça executera en parallèle et ça sera le bordel, alors il faut faire gaffe
        enemVis.calculateRotation();
        dejaVu = false;
        boxesCalculated = false;
        //var wait = new WaitForSeconds(0.5f);

    }

    IEnumerator RotateLeft()
    {
        Debug.Log("Rotating Left");
        for (int g = 0; g < iRotateInterval; g++)
        {
            //transform.position = new Vector3(Mathf.Round(transform.position.x), fYLockPosition, Mathf.Round(transform.position.z));
            transform.localEulerAngles += new Vector3(0, -fRotateIncrement, 0);
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(0.5f);
        // reset key boolean for recalculation, c'est ça le problème avec les coroutines en fait si tu les reset pas ici 
        // ça executera en parallèle et ça sera le bordel, alors il faut faire gaffe
        enemVis.calculateRotation();
        dejaVu = false;
        boxesCalculated = false;
        //var wait = new WaitForSeconds(0.5f);

    }

}